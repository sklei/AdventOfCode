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
            //input = File.ReadAllLines("input.example.txt"); //#1 easter #2 advent

            var hiddenMsg1 = input
                .SelectMany(s => s
                    .Select((c,i) => 
                        new { C = c, I = i } 
                    )
                )
                .GroupBy(grp => grp.I)
                .Select(grp => grp.Select(x => x.C))
                .Select(v => v.GroupBy(c => c).OrderByDescending(c => c.Count()).First().Key)
                .Aggregate("", (was, add) => was += add);

            var hiddenMsg2 = input
                .SelectMany(s => s
                    .Select((c, i) =>
                        new { C = c, I = i }
                    )
                )
                .GroupBy(grp => grp.I)
                .Select(grp => grp.Select(x => x.C))
                .Select(v => v.GroupBy(c => c).OrderBy(c => c.Count()).First().Key)
                .Aggregate("", (was, add) => was += add);

            Console.WriteLine($"#1 Hidden message: {hiddenMsg1}");
            Console.WriteLine($"#2 Hidden message: {hiddenMsg2}");
            Console.ReadKey();
        }
    }
}
