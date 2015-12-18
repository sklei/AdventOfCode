using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            //lines = new string[] { "1x1x10", "2x3x4" }; //Testing
            //A line is lxwxh and it's sum: 2*l*w + 2*w*h + 2*h*l + 1*smallest

            var res = lines
                .Select(l => l.Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(ln => int.Parse(ln)).ToArray()
                )
                .Select(n => new { l = n[0], w = n[1], h = n[2] })
                .Select(d => new int[] { (d.l * d.w), (d.w * d.h), (d.h * d.l) }
                    .OrderBy(x => x)
                    .ToArray()
                )
                .Sum(d => (2 * d[0]) + (2 * d[1]) + (2 * d[2]) + d[0]); 

            Console.WriteLine($"2.1 - Packaging needed: {res}");
            Console.ReadKey(); 
        }
    }
}
