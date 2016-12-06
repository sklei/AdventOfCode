using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015.Day03
{
    class Program_2015_03
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            //input = "^v^v^v^v^v"; //Testing

            Dictionary<Point, int> grid = new Dictionary<Point, int>();
            Point position = new Point() { X = 0, Y = 0 };
            grid.Add(position, 1);

            //Testing
            Point position2 = new Point() { X = 1, Y = 0 };

            int presents = 0;
            input.ToList().ForEach(x =>
                {
                    if (!grid.TryGetValue(position2, out presents))
                        grid.Add(position, presents);

                    switch (x)
                    {
                        case '^': position.Y++; break;
                        case '>': position.X++; break;
                        case 'v': position.Y--; break;
                        case '<': position.X--; break;
                    }
                    
                });

           

            //input
            //    .ToList()
            //    .ForEach(x => 
            //    {
            //        switch(x)
            //        {
            //            case '^': Y++; break;
            //            case '>': X++; break;
            //            case 'v': Y--; break;
            //            case '<': X--; break;
            //        }


            //        grid[X, Y]++;
            //    });

            var houses = grid.Cast<int>().Where(x => x > 0).Count();

            Console.WriteLine($"3.1 - Houses with at least one present: {houses}");

            Console.ReadKey();
        }
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
