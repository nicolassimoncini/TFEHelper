using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TFEHelper.Backend.Tools.Threading
{
    /// <summary>
    /// Thread safe pool of <see cref="SemaphoreSlim"/> instances.
    /// </summary>
    public sealed class MutexPool : IDisposable
    {
        private readonly ConcurrentDictionary<string, SemaphoreSlim> _pool;
        private bool _disposed = false;

        /// <summary></summary>
        public MutexPool()
        {
            _pool = new ConcurrentDictionary<string, SemaphoreSlim>();
        }

        /// <summary>
        /// Gets a <see cref="SemaphoreSlim"/> instance related to who invoked the method in a thread safe manner.
        /// </summary>
        /// <param name="invoker">Name of the invoker method.  Since this parameter implements <see cref="CallerMemberNameAttribute"/>, leaving this 
        /// value default will allow compiler to resolve the invoker name by itself.</param>
        /// <returns></returns>
        public SemaphoreSlim GetMutex([CallerMemberName] string invoker = "")
        {
            return _pool.GetOrAdd(invoker, new SemaphoreSlim(1, 1));
        }

        /// <summary></summary>
        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                foreach (var poolItem in _pool)
                {
                    poolItem.Value.Dispose();
                }
                _pool.Clear();
            }
            _disposed = true;
        }

        /// <summary>Releases all the <see cref="SemaphoreSlim"/> instances held by the pool.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary></summary>
        ~MutexPool()
        {
            Dispose(false);
        }
    }
}
