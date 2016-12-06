using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016.Day01
{
    class Program
    {
        enum Direction { N, E, S, W }

        static Dictionary<string, bool> visitedPositions = new Dictionary<string, bool>();
        static Point entrance = null;

        static void Main(string[] args)
        {
            Direction facing = Direction.N;
            Point position = new Point();

            string input = File.ReadAllText("input.txt");
            //input = "R5, L5, R5, R3"; //TEST #1, should return 12
            //input = "R2, R2, R2"; //TEST #1, should return 5
            //input = "R8, R4, R4, R8"; //TEST #2, should return 4

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
                TakeAllSteps(step.Steps, facing, position);
            }

            Console.WriteLine($"#1 - Steps to take: {Math.Abs(position.X) + Math.Abs(position.Y)}");
            Console.WriteLine($"#2 - Entrance: {(entrance != null ? (Math.Abs(entrance.X) + Math.Abs(entrance.Y)) : -1)}");

            Console.ReadKey();
        }

        private static void TakeAllSteps(int stepsToTake, Direction inDirection, Point position)
        {
            Enumerable.Range(0, stepsToTake)
                .ToList()
                .ForEach(x =>
                {
                    switch (inDirection)
                    {
                        case Direction.N:
                            position.Y += 1;
                            break;
                        case Direction.E:
                            position.X += 1;
                            break;
                        case Direction.S:
                            position.Y -= 1;
                            break;
                        case Direction.W:
                            position.X -= 1;
                            break;
                    }

                    //#2 First positon visited twice is the entrance.
                    if (!visitedPositions.ContainsKey(position.Key))
                        visitedPositions[position.Key] = true;
                    else if (entrance == null)
                        entrance = new Point(position.X, position.Y);
                });
        }
    }

    public class Point
    {
        public Point()
        {

        }

        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public string Key
        {
            get
            {
                return X.ToString() + Y.ToString();
            }
        }
    }
}
