using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeSnippetsAndUtils
{
    public static class Retry
    {
        public static void InvokeAndRetry(this Action action, int attempts, TimeSpan retryInterval, IEnumerable<Exception> fatalExceptions = null)
        {
            InvokeAndRetry<object>(() =>
            {
                action();
                return null;
            }, attempts, retryInterval, fatalExceptions);
        }

        public static T InvokeAndRetry<T>(this Func<T> action, int attempts, TimeSpan retryInterval, IEnumerable<Exception> fatalExceptions = null)
        {
            GenericExtensions.ThrowIfNull(action, nameof(action));
            if (attempts < 1) throw new ArgumentOutOfRangeException(nameof(attempts), "Must make at least one attempt.");

            int maxAttempts = attempts;

            ICollection<Exception> exceptions = new List<Exception>();

            while (attempts > 0)
            {
                try
                {
                    if (attempts < maxAttempts) Thread.Sleep(retryInterval);

                    return action();
                }
                catch (Exception ex) when (!fatalExceptions?.Contains(ex) ?? true)
                {
                    exceptions.Add(ex);
                }

                attempts--;
            }

            throw new AggregateException(exceptions);
        }
        
        public static void InvokeAndRetryIf<TExc>(this Action action, int attempts, TimeSpan retryInterval) where TExc : Exception
        {
            InvokeAndRetryIf<object, TExc>(() =>
            {
                action();
                return null;
            }, attempts, retryInterval);
        }

        public static T InvokeAndRetryIf<T, TExc>(this Func<T> action, int attempts, TimeSpan retryInterval) where TExc : Exception
        {
            GenericExtensions.ThrowIfNull(action, nameof(action));
            if (attempts < 1) throw new ArgumentOutOfRangeException(nameof(attempts), "Must make at least one attempt.");

            Exception firstExc = null;

            while (attempts-- > 0)
            {
                try
                {
                    return action();
                }
                catch (TExc ex)
                {
                    firstExc ??= ex;
                    Thread.Sleep(retryInterval);
                }
            }

            throw firstExc;
        }
        
        public static async Task InvokeAndRetryAsync(this Func<Task> action, int attempts, TimeSpan retryInterval, IEnumerable<Exception> fatalExceptions = null)
        {
            await InvokeAndRetry<Task<object>>(() =>
            {
                action();
                return null;
            }, attempts, retryInterval, fatalExceptions);
        }

        public static async Task<T> InvokeAndRetryAsync<T>(this Func<Task<T>> action, int attempts, TimeSpan retryInterval, IEnumerable<Exception> fatalExceptions = null)
        {
            GenericExtensions.ThrowIfNull(action, nameof(action));
            if (attempts < 1) throw new ArgumentOutOfRangeException(nameof(attempts), "Must make at least one attempt.");

            int maxAttempts = attempts;

            ICollection<Exception> exceptions = new List<Exception>();

            while (attempts > 0)
            {
                try
                {
                    if (attempts < maxAttempts) await Task.Delay(retryInterval);

                    return await action();
                }
                catch (Exception ex) when (!fatalExceptions?.Contains(ex) ?? true)
                {
                    exceptions.Add(ex);
                }

                attempts--;
            }

            throw new AggregateException(exceptions);
        }
        
        public static async Task InvokeAndRetryIfAsync<TExc>(this Func<Task> action, int attempts, TimeSpan retryInterval) where TExc : Exception
        {
            await InvokeAndRetryIf<Task<object>, TExc>(() =>
            {
                action();
                return null;
            }, attempts, retryInterval);
        }

        public static async Task<T> InvokeAndRetryIfAsync<T, TExc>(this Func<Task<T>> action, int attempts, TimeSpan retryInterval) where TExc : Exception
        {
            GenericExtensions.ThrowIfNull(action, nameof(action));
            if (attempts < 1) throw new ArgumentOutOfRangeException(nameof(attempts), "Must make at least one attempt.");

            Exception firstExc = null;

            while (attempts-- > 0)
            {
                try
                {
                    return await action();
                }
                catch (TExc ex)
                {
                    firstExc ??= ex;
                    await Task.Delay(retryInterval);
                }
            }

            throw firstExc;
        }
    }
}
