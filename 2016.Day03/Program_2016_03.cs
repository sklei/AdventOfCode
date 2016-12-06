using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016.Day03
{
    class Program_2016_03
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            //input = "5 10 25"; //#1 should be invalid

            int validTriangles = lines.Count(l => l
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => Int32.Parse(n))
                .ToArray()
                .IsValidTriangle());

            var validTrianglesAsColumns = lines
                .Select(l => l.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .Select((s, i) => new {
                    Index = i / 3,
                    Numbers = s.Select(n => Int32.Parse(n)).ToArray()
                })
                .GroupBy(ian => ian.Index)
                .Select(iang => iang.Select(ian => ian.Numbers).ToArray())
                .SelectMany(npg => Enumerable.Range(0, 9).Select(n => npg[n % 3][n / 3])) //Each section is now 3x3
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 3)
                .Select(x => x.Select(y => y.Value).ToArray())
                .Count(x => x.IsValidTriangle());

            Console.WriteLine($"#1 Valid triangles: {validTriangles}");
            Console.WriteLine($"#2 Valid triangles as colums: {validTrianglesAsColumns}");
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
