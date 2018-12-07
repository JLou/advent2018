using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            List<Rectangle> inputs = new List<Rectangle>();
            using (var file = new System.IO.StreamReader(@"input.txt"))
            {
                Regex r = new Regex(@"#(?<Id>\d+) @ (?<X>\d+),(?<Y>\d+): (?<Width>\d+)x(?<Height>\d+)");
                while ((line = file.ReadLine()) != null)
                {
                    Match match = r.Match(line);
                    inputs.Add(new Rectangle
                    {
                        Id = int.Parse(match.Groups["Id"].Value),
                        X = int.Parse(match.Groups["X"].Value),
                        Y = int.Parse(match.Groups["Y"].Value),
                        Width = int.Parse(match.Groups["Width"].Value),
                        Height = int.Parse(match.Groups["Height"].Value),
                    });
                }

                file.Close();
            }

            int[,] fabric = new int[1000, 1000];
            int overlapCount = 0;
            //Part 1
            foreach (var item in inputs)
            {
                for (int i = item.X; i < item.X + item.Width; i++)
                {
                    for (int j = item.Y; j < item.Y + item.Height; j++)
                    {
                        if (fabric[j, i] == 1)
                        {
                            overlapCount++;
                        }
                        fabric[j, i]++;
                    }
                }
            }

            Console.WriteLine("Overlap count: " + overlapCount);

            //Part 2
            var notOverlapped = inputs.ToDictionary(g => g.Id);
            List<int>[,] fabric2 = new List<int>[1000, 1000];
            foreach (var item in inputs)
            {
                for (int i = item.X; i < item.X + item.Width; i++)
                {
                    for (int j = item.Y; j < item.Y + item.Height; j++)
                    {
                        if (fabric2[j, i] is null) fabric2[j, i] = new List<int>();
                        if (fabric2[j, i].Count > 0)
                        {
                            foreach (var el in fabric2[j, i])
                            {
                                notOverlapped.Remove(el);
                            }
                            notOverlapped.Remove(item.Id);
                        }
                        fabric2[j, i].Add(item.Id);
                    }
                }
            }

            Console.WriteLine("il reste " + notOverlapped.Count + " non overlap");
            Console.WriteLine("L'id est " + notOverlapped.Keys.FirstOrDefault());

        }

        class Rectangle
        {
            public int Id { get; set; }

            public int X { get; set; }

            public int Y { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }
        }
    }
}
