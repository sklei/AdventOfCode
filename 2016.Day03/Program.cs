using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016.Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            //input = "5 10 25"; //#1 should be invalid

            string[] lines = input.Split('\n');

            int validTriangles = lines.Count(l => l
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => Int32.Parse(n))
                .ToArray()
                .IsValidTriangle());

            Console.WriteLine($"Valid triangles: {validTriangles}");
            Console.ReadKey();
        }
    }

    public static class TriangleExtension
    {
        public static bool IsValidTriangle(this int[] values)
        {
            return Enumerable.Range(0, 3)
                .All(i => values[i % 3] + values[(i + 1) % 3] > values[(i + 2) % 3]);
        }
    }
}
