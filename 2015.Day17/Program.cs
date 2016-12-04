using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015.Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sw = new StreamReader("input.txt"))
            {
                var list = sw.ReadToEnd().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                /*var result = Enumerable
                  .Range(1, (1 << list.Count) - 1)
                  .Select(index => list.Where((item, idx) => ((1 << idx) & index) != 0).ToList());
                //PART 1
                var combinationsSatysfying = result.Where(comb => comb.Sum() == 150);

                //PART 2
                var minCount = combinationsSatysfying.Min(comb => comb.Count());
                var minCombinations = combinationsSatysfying.Where(comb => comb.Count() == minCount);

                System.Console.WriteLine($"Number of combinations(Part 1): {combinationsSatysfying.Count()}");
                System.Console.WriteLine($"Different ways of minimal (Part 2): {minCombinations.Count()}");*/

                ////

                var counts = new int[151, 21];
                counts[0, 0] = 1;

                foreach (int size in list)
                {
                    for (int v = 150 - size; v >= 0; v--)
                    {
                        for (int n = 20; n > 0; n--)
                        {
                            counts[v + size, n] += counts[v, n - 1];
                        }
                    }
                }

                int totalCombinations = Enumerable.Range(0, 21).Sum(n => counts[150, n]);

                Console.WriteLine($"Combinations that sum to 150: {totalCombinations}");

                int minCount2 = Enumerable.Range(0, 20)
                    .Where(n => counts[150, n] > 0)
                    .Min();

                Console.WriteLine($"Minimum number of containers: {minCount2}");

                int minCountCombinations = counts[150, minCount2];
                Console.WriteLine($"Combinations of {minCount2} that sum to 150: {minCountCombinations}");
            }

            Console.ReadKey();
        }
    }
}
