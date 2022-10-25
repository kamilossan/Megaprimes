using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaprimes.Services
{
    public class MegaPrimesService
    {
        private readonly ISieveService _sieveService;
        private readonly uint[] nonPrimes = { 0, 1, 4, 6, 8, 9 };
        public MegaPrimesService(ISieveService sieveService)
        {
            _sieveService = sieveService;
        }

        public uint[] MegaPrimes(uint max) => _sieveService.GetPrimes(max).Where(x => isMega(x)).ToArray();

        private bool isMega(uint param)
        {
            while (param > 0)
            {
                var mod = param % 10;
                if (nonPrimes.Contains(mod)) return false;
                param /= 10;
            }
            return true;
        }
    }
}
