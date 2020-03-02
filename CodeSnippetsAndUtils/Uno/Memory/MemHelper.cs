using CodeSnippetsAndUtils.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static CodeSnippetsAndUtils.Uno.Memory.MemHelper.Pinvoke;

namespace CodeSnippetsAndUtils.Uno.Memory
{
    // Instance Members
    public partial class MemHelper
    {
        private IntPtr hProcess;
        private IntPtr? MainModuleAddr => OpenedProcess?.MainModule.BaseAddress;

        public Process? OpenedProcess { get; protected set; }

        public void OpenProcess(string name)
        {
            OpenedProcess = Process.GetProcessesByName(name).FirstOrDefault();
            hProcess = Pinvoke.OpenProcess(ProcessAccessFlags.All, false, OpenedProcess.Id);
        }
        public void OpenProcess(Process process)
        {
            OpenedProcess = process;
            hProcess = Pinvoke.OpenProcess(ProcessAccessFlags.All, false, process.Id);
        }

        public T Read<T>(int offset) where T : unmanaged
        {
            if (OpenedProcess is null) throw new InvalidOperationException("There is current no opened process to read from.");

            IntPtr realAddr = IntPtr.Add(MainModuleAddr!.Value, offset);
            return Read<T>(realAddr);
        }
        public T[] Read<T>(int offset, int count) where T : unmanaged
        {
            if (OpenedProcess is null) throw new InvalidOperationException("There is current no opened process to read from.");

            IntPtr realAddr = IntPtr.Add(MainModuleAddr!.Value, offset);
            return Read<T>(realAddr, count);
        }
        public unsafe T Read<T>(IntPtr address) where T : unmanaged
        {
            if (OpenedProcess is null) throw new InvalidOperationException("There is current no opened process to read from.");

            byte[] serialized = new byte[sizeof(T)];
            ReadProcessMemory(hProcess, address, serialized, sizeof(T), out _);

            return Serialization.Deserialize<T>(serialized);
        }
        public unsafe T[] Read<T>(IntPtr address, int count) where T : unmanaged
        {
            if (OpenedProcess is null) throw new InvalidOperationException("There is current no opened process to read from.");
            if (count < 1) throw new ArgumentOutOfRangeException(nameof(count));

            int size = sizeof(T) * count;
            byte[] serialized = new byte[size];
            ReadProcessMemory(hProcess, address, serialized, size, out _);

            return Serialization.Deserialize<T>(serialized, count);
        }

        public void Write<T>(int offset, T value) where T : unmanaged
        {
            if (OpenedProcess is null) throw new InvalidOperationException("There is current no opened process to read from.");

            IntPtr realAddr = IntPtr.Add(MainModuleAddr!.Value, offset);
            Write(realAddr, value);
        }
        public void Write<T>(int offset, T[] values) where T : unmanaged
        {
            if (OpenedProcess is null) throw new InvalidOperationException("There is current no opened process to read from.");

            IntPtr realAddr = IntPtr.Add(MainModuleAddr!.Value, offset);
            Write(realAddr, values);
        }
        public void Write<T>(IntPtr address, T value) where T : unmanaged
        {
            byte[] serialized = Serialization.Serialize(value);
            WriteProcessMemory(hProcess, address, serialized, serialized.Length, out _);
        }
        public void Write<T>(IntPtr address, T[] values) where T : unmanaged
        {
            if (values is null) throw new ArgumentNullException(nameof(values));

            byte[] serialized = Serialization.Serialize(values);
            WriteProcessMemory(hProcess, address, serialized, serialized.Length, out _);
        }
    }

    // Static Members
    public partial class MemHelper
    {
        public static IntPtr OpenProcess(Process proc, ProcessAccessFlags flags)
        {
            return Pinvoke.OpenProcess(flags, false, proc.Id);
        }

        public static IntPtr GetModuleBaseAddress(Process process, string moduleName)
        {
            IntPtr addr = IntPtr.Zero;
            foreach (ProcessModule? module in process.Modules)
            {
                if (module?.ModuleName == moduleName)
                {
                    addr = module.BaseAddress;
                    break;
                }
            }
            return addr;
        }
        public static IntPtr GetModuleBaseAddress(int processID, string moduleName)
        {
            const int InvalidHandleValue = -1;

            IntPtr addr = IntPtr.Zero;
            IntPtr hSnap = CreateToolhelp32Snapshot(SnapshotFlags.Module | SnapshotFlags.Module32, processID);

            if (hSnap.ToInt64() != InvalidHandleValue)
            {
                MODULEENTRY32 modEntry = new MODULEENTRY32();
                modEntry.dwSize = (uint)Marshal.SizeOf(typeof(MODULEENTRY32));

                if (Module32First(hSnap, ref modEntry))
                {
                    do
                    {
                        if (modEntry.szModule == moduleName)
                        {
                            addr = modEntry.modBaseAddr;
                            break;
                        }
                    } while (Module32Next(hSnap, ref modEntry));
                }
            }

            CloseHandle(hSnap);
            return addr;
        }

        public static IntPtr FindDMAAddress(IntPtr hProcess, IntPtr ptr, int[] offsets)
        {
            byte[] buffer = new byte[IntPtr.Size];
            foreach (int i in offsets)
            {
                ReadProcessMemory(hProcess, ptr, buffer, buffer.Length, out IntPtr readBytes);
                ptr = (IntPtr.Size == sizeof(int))
                    ? IntPtr.Add(new IntPtr(BitConverter.ToInt32(buffer, 0)), i)
                    : IntPtr.Add(new IntPtr(BitConverter.ToInt64(buffer, 0)), i);
            }
            return ptr;
        }
    }

    // Pinvoke
    public partial class MemHelper
    {
        public class Pinvoke
        {
            [Flags]
            public enum ProcessAccessFlags : uint
            {
                All = 0x001F0FFF,
                Terminate = 0x01,
                CreateThread = 0x02,
                VirtualMemoryOperation = 0x08,
                VirtualMemoryRead = 0x10,
                VirtualMemoryWrite = 0x20,
                DuplicateHandle = 0x40,
                CreateProcess = 0x80,
                SetQuota = 0x100,
                SetInformation = 0x200,
                QueryInformation = 0x400,
                QueryLimitedInformation = 0x1000,
                Synchronize = 0x100000
            }
            [Flags]
            public enum SnapshotFlags : uint
            {
                HeapList = 0x01,
                Process = 0x02,
                Thread = 0x04,
                Module = 0x08,
                Module32 = 0x10,
                Inherit = 0x80000000,
                All = 0x1F,
                NoHeaps = 0x40000000
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct PROCESSENTRY32
            {
                public uint dwSize;
                public uint cntUsage;
                public uint th32ProcessID;
                public IntPtr th32DefaultHeapID;
                public uint th32ModuleID;
                public uint cntThreads;
                public uint th32ParentProcessID;
                public int pcPriClassBase;
                public uint dwFlags;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string szExeFile;
            }
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct MODULEENTRY32
            {
                internal uint dwSize;
                internal uint th32ModuleID;
                internal uint th32ProcessID;
                internal uint GlblcntUsage;
                internal uint ProccntUsage;
                internal IntPtr modBaseAddr;
                internal uint modBaseSize;
                internal IntPtr hModule;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
                internal string szModule;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                internal string szExePath;
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesRead);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);
            [DllImport("kernel32.dll")]
            public static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);
            [DllImport("kernel32.dll")]
            public static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);
            [DllImport("kernel32.dll")]
            public static extern bool Module32First(IntPtr hSnapshot, ref MODULEENTRY32 lpme);
            [DllImport("kernel32.dll")]
            public static extern bool Module32Next(IntPtr hSnapshot, ref MODULEENTRY32 lpme);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool CloseHandle(IntPtr hHandle);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr CreateToolhelp32Snapshot(SnapshotFlags dwFlags, int th32ProcessID);
        }
    }
}
