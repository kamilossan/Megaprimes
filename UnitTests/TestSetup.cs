using Megaprimes.Services;

namespace UnitTests
{
    [SetUpFixture]
    public class TestsSetup
    {
        protected MegaPrimesService _megaPrimesService;
        protected ISieveService _sieveService;

        [OneTimeSetUp]
        public void GivenPrimeCalculatingServices()
        {
            _sieveService = new SieveService();
            _megaPrimesService = new MegaPrimesService(_sieveService);
        }

    }
}