using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016.Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            //input = File.ReadAllLines("input.example.txt"); //#1 easter

            var hiddenMsg = input
                .SelectMany(s => s
                    .Select((c,i) => 
                        new { C = c, I = i } 
                    )
                )
                .GroupBy(grp => grp.I)
                .Select(grp => grp.Select(x => x.C).ToArray())
                .Select(v => v.GroupBy(c => c).OrderByDescending(c => c.Count()).First().Key)
                .Aggregate("", (was, add) => was += add);

            Console.WriteLine($"#1 Hidden message: {hiddenMsg}");
            Console.ReadKey();
        }
    }
}
