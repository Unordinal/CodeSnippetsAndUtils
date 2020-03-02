using CodeSnippetsAndUtils.ASCIITypes;
using CodeSnippetsAndUtils.Collections;
using CodeSnippetsAndUtils.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;
using static CodeSnippetsAndUtils.UnitTests.Benchmarking;

namespace CodeSnippetsAndUtils.UnitTests
{
    [TestClass]
    public class MiscTests
    {
        private const int Iterations = 1000;
        private Action<Action, int> Benchmark { get; } = CompensatedBenchmark;

        private enum TestEnum { Now = 11, When = -5, Getting = -1, The, Count, Of, This, Enum, Equals, Eight }

        [TestMethod]
        public void Misc_Tests()
        {
            const int arrLen = 100000;
            string str0 = "abcdefghijklmnopqrstuvwxyz";
            string str1 = "0123456789";
            string str2 = "[|]";
            ASCIIString aStr0 = "abcdefghijklmnopqrstuvwxyz";
            ASCIIString aStr1 = "0123456789";
            ASCIIString aStr2 = "[|]";

            string res = string.Empty;
            ASCIIString aRes = ASCIIString.Empty;

            Console.WriteLine(str0);
            Console.WriteLine(aStr0);

            Console.WriteLine("string insert");
            Benchmark(() =>
            {
                res = str0.Substring(5, 10);
            }, Iterations);

            Console.WriteLine(res);
            
            Console.WriteLine("ASCIIString insert");
            Benchmark(() =>
            {
                aRes = aStr0.Substring(5, 10);
            }, Iterations);

            Console.WriteLine(aRes);
        }

        [TestMethod]
        public void SizeOf_Tests()
        {
            Console.WriteLine("Int: " + sizeof(int));                                               // Int: 4
            //Console.WriteLine("TestStruct: " + sizeof(TestStruct)); // error
            Console.WriteLine("Dyn Int: " + MiscUtils.DynSizeOf<int>());                            // Dyn Int: 4
            Console.WriteLine("Dyn TestStruct: " + MiscUtils.DynSizeOf<TestStruct>());              // Dyn TestStruct: 8
            Console.WriteLine("Dyn TestStructNoPad: " + MiscUtils.DynSizeOf<TestStructNoPad>());    // Dyn TestStructNoPad: 5
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TestStruct
        {
            private readonly int testInt;
            private readonly byte testByte;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TestStructNoPad
        {
            private readonly int testInt;
            private readonly byte testByte;
        }
    }
}
