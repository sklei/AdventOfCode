using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2016.Day07
{
    class Program_2016_07
    {
        private static Regex IsAbbaRegex = new Regex(@"(\w)(\w)(?!\1)\2\1");

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            //input = new string[] { "abba[mnop]qrst" }; //#1 true
            //input = new string[] { "abcd[bddb]xyyx" }; //#2 false
            //input = new string[] { "aaaa[qwer]tyui" }; //#3 false
            //input = new string[] { "ioxxoj[asdfgh]zxcvbn" }; //#4 true

            var ebipCount = input
                .Select(l => new EBIP(l))
                .Count(ebip => ebip.IsAbba);

            Console.WriteLine($"#1 EBIP's found: {ebipCount}");
            Console.ReadKey();
        }

        private class EBIP
        {
            public string Input { get; set; }

            public EBIP(string input)
            {
                Input = input;
            }

            public bool IsAbba
            {
                get
                {
                    bool result = false;
                    bool inHypernet = true; //First part is never a hypernet part, but as it gets reversed this should be true.

                    foreach(string part in Input.Split(new char[] { '[', ']' }))
                    {
                        inHypernet = !inHypernet;
                        bool isAbbaPart = IsAbbaRegex.IsMatch(part);

                        if(inHypernet && isAbbaPart)
                        {
                            result = false;
                            break;
                        }
                        else if(!inHypernet && isAbbaPart)
                        {
                            result = true;
                        }
                    }

                    return result;
                }
            }
        }
    }
}
