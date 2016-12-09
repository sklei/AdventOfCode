using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
 * You are faced with a security door designed by Easter Bunny engineers that seem to have 
 * acquired most of their security knowledge by watching hacking movies.

The eight-character password for the door is generated one character at a time by finding 
the MD5 hash of some Door ID (your puzzle input) and an increasing integer index(starting with 0).

A hash indicates the next character in the password if its hexadecimal representation starts with 
five zeroes.If it does, the sixth character in the hash is the next character of the password.

For example, if the Door ID is abc:

-   The first index which produces a hash that starts with five zeroes is 3231929, which we find by 
    hashing abc3231929; the sixth character of the hash, and thus the first character of the password, is 1.
-   5017308 produces the next interesting hash, which starts with 000008f82..., 
    so the second character of the password is 8.
-   The third time a hash starts with five zeroes is for abc5278568, discovering the character f.
    
In this example, after continuing this search a total of eight times, the password is 18f47a30.

Given the actual Door ID, what is the password?

Your puzzle input is wtnhxymk.
*/

namespace _2016.Day05
{
    class Program_2016_05
    {
        static void Main(string[] args)
        {
            string input = "wtnhxymk";
            //input = "abc"; //TEST #1 - 18f47a30
            string password = "";

            for(int i = 0; i < Int32.MaxValue; i++)
            {
                string md5 = CalculateMD5Hash(input + i.ToString());

                if(md5.StartsWith("00000"))
                {
                    password += md5[5];

                    if (password.Length == 8)
                        break;
                }
            }

            Console.WriteLine($"#1 - Password: {password}");
            Console.ReadLine();
        }

        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
