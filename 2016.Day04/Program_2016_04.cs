using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//--- Day 4: Security Through Obscurity ---

//Finally, you come across an information kiosk with a list of rooms.Of course, the list is encrypted 
//and full of decoy data, but the instructions to decode the list are barely hidden nearby.Better 
//remove the decoy data first.

//Each room consists of an encrypted name (lowercase letters separated by dashes) followed by a dash, 
//a sector ID, and a checksum in square brackets.

//A room is real (not a decoy) if the checksum is the five most common letters in the encrypted name, 
//in order, with ties broken by alphabetization.For example:

//aaaaa-bbb-z-y-x-123[abxyz] is a real room because the most common letters are a(5), b(3), and then 
//a tie between x, y, and z, which are listed alphabetically.
//a-b-c-d-e-f-g-h-987[abcde] is a real room because although the letters are all tied (1 of each), 
//the first five are listed alphabetically.
//not-a-real-room-404[oarel] is a real room.
//totally-real-room-200[decoy] is not.
//Of the real rooms from the list above, the sum of their sector IDs is 1514.

//What is the sum of the sector IDs of the real rooms?

namespace _2016.Day04
{
    class Program_2016_04
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            //input =  new string[] { "aaaaa-bbb-z-y-x-123[abxyz]" }; //#1 real room
            //input =  new string[] { "a-b-c-d-e-f-g-h-987[abcde]" }; //#1 real room
            //input =  new string[] { "totally-real-room-200[decoy]" }; //#1 fake room
            //input =  new string[] { "qzmt-zixmtkozy-ivhz-343[doesn]" }; //#2 very encrypted name

            var rooms = input.Select(l => new Room()
            {
                Name = l.Substring(0, l.LastIndexOf('-')),
                SectorID = Int32.Parse(l.Substring(l.LastIndexOf('-') + 1, 3)),
                Checksum = l.Substring(l.IndexOf('[') + 1, 5)
            });

            var realRoomSectorSum = rooms.Where(r => r.IsRealRoom).Sum(r => r.SectorID);
            var northPoleSectorID = rooms.FirstOrDefault(r => r.DecryptedValue.ToLower().StartsWith("north"))?.SectorID;

            Console.WriteLine($"#1 Real room sector sum: {realRoomSectorSum}"); //278221
            Console.WriteLine($"#2 North Pole sector ID: {northPoleSectorID}");
            Console.ReadKey();
        }
    }

    internal class Room
    {
        public string Name { get; set; }
        public int SectorID { get; set; }
        public string Checksum { get; set; }

        public bool IsRealRoom
        {
            get
            {
                var letterGroupsOrdered = Name
                    .Replace("-", "")
                    .GroupBy(letter => letter)
                    .OrderByDescending(letterGroup => letterGroup.Count())
                    .ThenBy(letterGroup => letterGroup.Key)
                    .Take(5)
                    .Select(letterGroup => letterGroup.Key)
                    .Aggregate("", (a, b) => a + b);

                var result = letterGroupsOrdered == Checksum;

                return result;
            }
        }

        internal String DecryptedValue
        {
            get
            {
                //Steps, transforms q to v:
                //var cur = 'q'; //113
                //var inc = 343 % 26; //5
                //var cor = ((cur + inc) - 'a') % 26; //21
                //var nex = 'a' + cor; //118

                string decryptedValue = Name
                    .Select(c => c == '-' ? ' ' : (char)('a' + ((c + (SectorID % 26)) - 'a') % 26))
                    .Aggregate("", (a, b) => a + b);

                return decryptedValue;
            }
        }

        public override string ToString()
        {
            return $"Name: {Name} SectorID: {SectorID} CheckSum: {Checksum} Real: {IsRealRoom} DecryptedValue {DecryptedValue}";
        }
    }
}
