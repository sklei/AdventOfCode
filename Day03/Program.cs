using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            //input = "^v^v^v^v^v"; //Testing
            
            int[,] grid = new int[input.Length << 1, input.Length << 1];
            int X = input.Length;
            int Y = input.Length;

            grid[X, Y]++;

            input
                .ToList()
                .ForEach(x => 
                {
                    switch(x)
                    {
                        case '^': Y++; break;
                        case '>': X++; break;
                        case 'v': Y--; break;
                        case '<': X--; break;
                    }
                    grid[X, Y]++;
                });

            var houses = grid.Cast<int>().Where(x => x > 0).Count();

            Console.WriteLine($"3.1 - Houses with at least one present: {houses}");

            Console.ReadKey();
        }
    }
}
