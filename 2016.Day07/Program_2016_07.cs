using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2016.Day07
{
    static class Program_2016_07
    {
        /* Regex explanation:
         * \w           = word character
         * (?!\1)       = negative lookahead to matching group #1, as in: it can't match group #1
         * (?=RX1)RX2   = positive lookahead, find RX1 where RX2 follows
         */

        private static Regex IsAbbaRegex = new Regex(@"(\w)(\w)(?!\1)\2\1");
        private static Regex IsAbaRegex = new Regex(@"(?=((\w)(\w)(?!\3)\2))\w");

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            //input = new string[] { "abba[mnop]qrst" }; //#1 true
            //input = new string[] { "abcd[bddb]xyyx" }; //#1 false
            //input = new string[] { "aaaa[qwer]tyui" }; //#1 false
            //input = new string[] { "ioxxoj[asdfgh]zxcvbn" }; //#4 true

            //input = new string[] { "aba[bab]xyz" }; //#2 true
            //input = new string[] { "xyx[xyx]xyx" }; //#2 false
            //input = new string[] { "aaa[kek]eke" }; //#2 true
            //input = new string[] { "zazbz[bzb]cdb" }; //#2 true
            
            var tlsCount = input
                .Select(l => new EBIP(l))
                .Count(ebip => ebip.SupportsTLS());

            var sslCount = input
                .Select(l => new EBIP(l))
                .Count(ebip => ebip.SupportsSSL());

            Console.WriteLine($"#1 EBIP's found Supporting TLS: {tlsCount}");
            Console.WriteLine($"#2 EBIP's found Supporting SSL: {sslCount}");
            Console.ReadKey();
        }

        private class EBIP
        {
            public string Input { get; set; }

            public EBIP(string input)
            {
                Input = input;
            }

            public bool SupportsTLS()
            {
                bool result = false;
                bool inHypernet = true; //First part is never a hypernet part, but as it gets reversed this should be true.

                foreach (string part in Input.Split(new char[] { '[', ']' }))
                {
                    inHypernet = !inHypernet;
                    bool isAbbaPart = IsAbbaRegex.IsMatch(part);

                    if (inHypernet && isAbbaPart)
                    {
                        result = false;
                        break;
                    }
                    else if (!inHypernet && isAbbaPart)
                    {
                        result = true;
                    }
                }

                return result;
            }

            public bool SupportsSSL(bool hypernetOnly = false, string hypernetValue = null)
            {
                bool inHypernet = true; //First part is never a hypernet part, but as it gets reversed this should be true.

                foreach (string part in Input.Split(new char[] { '[', ']' }))
                {
                    inHypernet = !inHypernet;

                    if (hypernetOnly && !inHypernet)
                        continue;
                    else if (!hypernetOnly && inHypernet)
                        continue;

                    MatchCollection mc = IsAbaRegex.Matches(part);

                    foreach (Match m in mc)
                    {
                        string value = m.Groups[1].Value;
                        string matchReversed = "" + value[1] + value[0] + value[1];

                        if (!inHypernet && SupportsSSL(!hypernetOnly, matchReversed))
                            return true;
                        else if (hypernetOnly && value == hypernetValue)
                            return true;
                    }
                }

                return false;
            }
        }
    }
}
