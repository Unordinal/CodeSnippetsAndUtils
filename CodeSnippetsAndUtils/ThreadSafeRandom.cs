using System;
using System.Security.Cryptography;
using System.Threading;

namespace CodeSnippetsAndUtils
{
    /// <summary>
    /// Provides a way to get a single <see cref="Random"/> instance that is unique for each thread.
    /// </summary>
    /// <remarks><see cref="ThreadStaticAttribute"/> uses my exact wording, huh.</remarks>
    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random instance;

        public static Random Instance => instance ??= new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId));
    }
}
