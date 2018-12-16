using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day10
{
    class Program
    {
        static int printNb = 0;
        static void Main(string[] args)
        {
            string line;
            Regex r = new Regex(@"position=< (-?\d+),  (-?\d+)> velocity=< (-?\d+), (-?\d+)>");
            List<Point> points = new List<Point>();
            using (var file = new System.IO.StreamReader(@"input.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    var chunks = line.Split(',', '<', '>');

                    points.Add(new Point(int.Parse(chunks[1]), int.Parse(chunks[2]), int.Parse(chunks[4]), int.Parse(chunks[5])));
                }

            }
            var i = 1;
            while (PrintPoints(points, i) || printNb < 3)
            {
                foreach (var p in points)
                {
                    p.Move();
                }


                i++;
            }
        }

        private static bool PrintPoints(List<Point> points, int seconds)
        {
            var (maxX, maxY) = points.Aggregate((0, 0), (acc, next) => (Math.Max(acc.Item1, next.X), Math.Max(acc.Item2, next.Y)));
            var (minX, minY) = points.Aggregate((0, 0), (acc, next) => (Math.Min(acc.Item1, next.X), Math.Min(acc.Item2, next.Y)));
            if ((maxX - minX + 1) * (maxY - minY + 1) < 0 || (maxY - minY + 1) > 150)
            {
                //Console.WriteLine("ignored");
                return false;
            }
            var output = new char[maxY - minY + 1][];

            for (int i = 0; i < maxY - minY + 1; i++)
            {
                output[i] = new char[maxX - minX + 1];
                for (int j = 0; j < maxX - minX + 1; j++)
                {
                    output[i][j] = '.';
                }
            }
            foreach (var p in points)
            {
                output[p.Y - minY][p.X - minX] = '#';
            }

            for (int i = 0; i < output.Length; i++)
            {
                for (int j = 0; j < output[i].Length; j++)
                {
                    Console.Write(output[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("waited " + seconds + "s");
            printNb++;
            return true;
        }

        public class Point
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public int SpeedX { get; private set; }
            public int SpeedY { get; private set; }

            public Point(int initialX, int initialY, int speedX, int speedY)
            {
                X = initialX;
                Y = initialY;
                SpeedX = speedX;
                SpeedY = speedY;
            }

            public void Move()
            {
                X += SpeedX;
                Y += SpeedY;
            }

            public override string ToString()
            {
                return $"({X}, {Y})";
            }
        }
    }
}
