using System.Collections.Concurrent;
using System.Diagnostics;

namespace Megaprimes.Services
{
    public interface ISieveService
    {
        public uint[] GetPrimes(uint n);
    }
    //concurrent Sieve of Erasthotenes
    public class SieveService : ISieveService
    {
        private ConcurrentDictionary<uint, bool>? _primeMatrix;

        public uint[] GetPrimes(uint n)
        {
            if (n < 2) return new uint[0];
            _primeMatrix = new ConcurrentDictionary<uint, bool>();
            _primeMatrix[1] = false;
            for (uint i = 2; i <= n; i++)
            {
                _primeMatrix[i] = true;
            }
            //likely to be faster if thread pool is divided into constant pool batches - and able to handle UINT_MAX without memory overflow.
            //if first n thread pool is resolved first, n+1 thread pool is more likely to contain skippable values already marked as composite, creating something akin to wheel sieve
            Task[] taskPool = new Task[(uint)Math.Sqrt(n)];
            for (uint i = 0; i < taskPool.Length; i++)
            {
                //copies value casue by the time task is scheduled to run *i* may have iterated asynchronously and delegates compile values by reference
                uint threadVar = i;
                taskPool[threadVar] = Task.Run(() => SieveMultiples(threadVar + 2));
            }
            Console.WriteLine($"Started {taskPool.Length} tasks on {Process.GetCurrentProcess().Threads.Count} threads.");
            Task.WaitAll(taskPool);
            return _primeMatrix.Where(x => x.Value).Select(x => x.Key).ToArray();
        }

        private async Task SieveMultiples(uint n)
        {
            Console.WriteLine("Sifting multiples of " + n);
            await Task.Run(() =>
            {
                if (_primeMatrix![n])
                {
                    for (uint i = n * 2; i <= _primeMatrix.Count; i += n)
                    {
                        _primeMatrix[i] = false;
                    }
                }
            });
            Console.WriteLine("Finished sifting multiples of " + n);
            return;
        }

    }

}
