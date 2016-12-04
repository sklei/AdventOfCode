using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class Program
    {
        enum Direction { N, E, S, W }

        static void Main(string[] args)
        {
            Direction facing = Direction.N;
            Point position = new Point();

            string input = File.ReadAllText("input.txt");
            //input = "R5, L5, R5, R3"; //TEST, should return 12
            //input = "R2, R2, R2"; //TEST, should return 5

            var steps = input
                .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => new
                {
                    Direction = s[0],
                    Steps = int.Parse(s.Substring(1)) //Important, some numbers are > 9.
                });

            foreach (var step in steps)
            {
                facing = (Direction)((((int)facing) + (step.Direction == 'L' ? -1 : 1) + 4) % 4);

                switch (facing)
                {
                    case Direction.N:
                        position.Y += step.Steps;
                        break;
                    case Direction.E:
                        position.X += step.Steps;
                        break;
                    case Direction.S:
                        position.Y -= step.Steps;
                        break;
                    case Direction.W:
                        position.X -= step.Steps;
                        break;
                }
            }

            Console.WriteLine($"Steps to take: {position.X + position.Y}");
            Console.ReadKey();
        }
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
