using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeSnippetsAndUtils.ASCIITypes.SystemInternal
{
    public static class Memory
    {
        public static unsafe void Copy<T>(IntPtr pSrc, IntPtr pDest, uint count) where T : unmanaged
        {
            uint elemSize = (uint)sizeof(T);
            CopyMemory(pDest, pSrc, count * elemSize);
        }

        public static unsafe void Copy<T>(IntPtr pSrc, T[] dest, uint count) where T : unmanaged
        {
            uint elemSize = (uint)sizeof(T);
            fixed (T* pDest = &dest[0])
            {
                CopyMemory((IntPtr)pDest, pSrc, count * elemSize);
            }
        }

        public static unsafe void Copy<T>(T[] src, IntPtr pDest, uint count) where T : unmanaged
        {
            uint elemSize = (uint)sizeof(T);
            fixed (T* pSrc = &src[0])
            {
                CopyMemory(pDest, (IntPtr)pSrc, count * elemSize);
            }
        }

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        private static extern void CopyMemory(IntPtr dest, IntPtr src, uint length);
    }
}
