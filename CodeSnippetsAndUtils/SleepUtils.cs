using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace CodeSnippetsAndUtils
{
    public static class SleepUtils
    {
        public static void Wait(TimeSpan duration)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                if (stopwatch.ElapsedMilliseconds >= duration.TotalMilliseconds) return;
                Thread.Sleep(1);
            }
        }
    }
}
