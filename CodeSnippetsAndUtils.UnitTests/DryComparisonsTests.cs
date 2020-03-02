using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using CodeSnippetsAndUtils;
using static CodeSnippetsAndUtils.DryComparisons;
using static CodeSnippetsAndUtils.UnitTests.Benchmarking;

namespace CodeSnippetsAndUtils.UnitTests
{
    [TestClass]
    public class DryComparisonsTests
    {
        private const int Iterations = 100000;
        private Action<Action, int> Benchmark { get; } = CompensatedBenchmark;

        [TestMethod]
        public void IDryObject_Variant_Tests()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            bool result = false;

            Console.WriteLine("Benchmarking Comparisons");
            Console.WriteLine($"Iterations: {Iterations}");
            Console.WriteLine("Set Size: 3");
            Console.WriteLine();

            Console.WriteLine("Comparing ints: ==");
            Console.WriteLine("Standard '||':");
            Benchmark(() =>
            {
                result = (1 == 1 || 2 == 1 || 3 == 1);
            }, Iterations);
            Console.WriteLine(result);
            Benchmark(() =>
            {
                result = (1 == 3 || 2 == 3 || 3 == 3);
            }, Iterations);
            Console.WriteLine(result);

            Console.WriteLine("FAnyOf<T>:");
            Benchmark(() =>
            {
                result = FAnyOf<int>(1, AsDry(RandomIntMethod), 3) == 1;
            }, Iterations);
            Console.WriteLine(result);
            Benchmark(() =>
            {
                result = FAnyOf<int>(1, AsDry(RandomIntMethod), 3) == 3;
            }, Iterations);
            Console.WriteLine(result);

            Console.WriteLine("AnyOf<T>:");
            Benchmark(() =>
            {
                result = AnyOf(1, 2) == 1;
            }, Iterations);
            Console.WriteLine(result);
            Benchmark(() =>
            {
                result = AnyOf(1, 2, 3) == 3;
            }, Iterations);
            Console.WriteLine(result);
        }

        public static int RandomIntMethod()
        {
            return 2;
        }
    }
}
