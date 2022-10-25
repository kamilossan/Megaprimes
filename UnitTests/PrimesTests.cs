using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests {

    [TestFixture]
    public class PrimesTests : TestsSetup
    {
        [TestCase((uint)1, ExpectedResult=new uint[0])]
        [TestCase((uint)0, ExpectedResult = new uint[0])]
        [TestCase((uint)10, ExpectedResult = new uint[] { 2, 3, 5, 7 })]
        [TestCase((uint)37, ExpectedResult = new uint[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 })]
        public uint[] ThenPrimesAndMegaPrimesAreCorrectlyDetermined(uint n)
        {
            return _sieveService.GetPrimes(n);
        }

    }
}
