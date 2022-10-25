using Megaprimes.Services;

var megaPrimeService = new MegaPrimesService(new SieveService());
uint param;
do { Console.WriteLine("Specify max int range"); }
while (!uint.TryParse(Console.ReadLine(), out param));
var result = megaPrimeService.MegaPrimes(param);

Console.WriteLine(result.Length>0?result.Select(x => $"{x}").Aggregate((x, y) => $"{x}, {y}"):"No results.");
