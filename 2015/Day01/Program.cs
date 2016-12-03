using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Move Santa up or down
            Func<char, int> upOrDown = (c) => { return c == '(' ? 1 : -1; };

            String input = File.ReadAllText("input.txt");

            //1.1 - Find the final destination
            int finalFloor = input.Sum(c => upOrDown(c));
            Console.WriteLine($"1.1: Final floor: {finalFloor}");

            //1.2 - How many steps to the basement?
            int steps = 0;
            var stepsToBasement = input
                .Select((c, i) => { steps += upOrDown(c); return new { steps, i };})
                .First(x => x.steps < 0)
                .i + 1;

            Console.WriteLine($"1.2: Steps to basement: {stepsToBasement}");
            Console.ReadKey();
        }
    }
}
