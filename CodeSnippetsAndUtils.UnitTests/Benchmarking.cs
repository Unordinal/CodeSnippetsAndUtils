using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodeSnippetsAndUtils.UnitTests
{
    public static class Benchmarking
    {
        public static void CompensatedBenchmark(Action action, int iterations = 10000)
        {
            Clock.BenchmarkTime(action, iterations);
        }

        public static void SimpleBenchmark(Action action, int iterations = 10000)
        {
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                action();
            }
            sw.Stop();
            Console.WriteLine($"\tTime: {sw.Elapsed.ToString(@"mm\:ss:\.fff")}");
        }
    }
}
