using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Tools.Threading
{
    /// <summary>
    /// Set of routines for threading handling.
    /// </summary>
    public static class ThreadingHelper
    {
        /// <summary>
        /// Asynchronously waits for a condition to be fulfilled returning True in case of success and False otherwise or canceled.
        /// </summary>
        /// <param name="condition">The condition delegate.</param>
        /// <param name="timeout">Maximum waiting time for <paramref name="condition"/> to be fulfilled.<br>
        /// If timeout is reached, result is false.</br><br>
        /// Leaving a default value means <paramref name="condition"/> will be iterated indefinitely.</br></param>
        /// <param name="pollingTimeout">Polling timeout performed on each <paramref name="condition"/> inspect iteration.<br>
        /// Leaving a default value means 100 ms.</br></param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if <paramref name="condition"/>is fulfilled and False otherwise or canceled.</returns>
        public static async Task<bool> WaitFor(Func<bool> condition, TimeSpan timeout = default, TimeSpan pollingTimeout = default, CancellationToken cancellationToken = default)
        {
            if (timeout == default) timeout = Timeout.InfiniteTimeSpan;
            if (pollingTimeout == default) pollingTimeout = TimeSpan.FromMilliseconds(100);

            Task loopTask = Task.Run(async () =>
            {
                while (!condition() && !cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(pollingTimeout, cancellationToken);
                }
            }, cancellationToken);

            if (await Task.WhenAny(loopTask, Task.Delay(timeout, cancellationToken)) == loopTask)
            {
                await loopTask;
                return true;
            }
            else return false;
        }
    }
}
