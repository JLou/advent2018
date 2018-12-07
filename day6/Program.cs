using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            List<(int x, int y)> coords = new List<(int, int)>();
            HashSet<(int, int)> nonInfinites = new HashSet<(int, int)>();
            Dictionary<(int, int), int> areaCount = new Dictionary<(int, int), int>();


            int maxX = 0, maxY = 0;
            using (var file = new System.IO.StreamReader(@"input.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    var chunks = line.Split(',');
                    var coord = (int.Parse(chunks[0]), int.Parse(chunks[1]));

                    maxX = coord.Item1 > maxX ? coord.Item1 : maxX;
                    maxY = coord.Item2 > maxY ? coord.Item2 : maxY;

                    coords.Add(coord);
                    areaCount.Add(coord, 0);
                }
            }

            maxX++; maxY++;
            int safeCount = 0;
            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    //each cell
                    var minDist = int.MaxValue;
                    List<(int, int)> currentClosest = new List<(int, int)>();
                    var sum = 0;
                    foreach (var coord in coords)
                    {
                        var dist = Math.Abs(i - coord.x) + Math.Abs(j - coord.y);
                        sum += dist;
                        if (dist < minDist)
                        {
                            currentClosest.Clear();
                            currentClosest.Add(coord);
                            minDist = dist;
                        }
                        else if (dist == minDist)
                        {
                            currentClosest.Add(coord);
                        }
                    }

                    if (currentClosest.Count == 1)
                    {
                        var item = currentClosest.Single();
                        if (areaCount.ContainsKey(item))
                            areaCount[item]++;

                    }
                    if (i == 0 || j == 0 || i == maxX || j == maxY)
                    {
                        foreach (var item in currentClosest)
                        {
                            areaCount.Remove(item);
                        }
                    }

                    if (sum < 10000)
                        safeCount++;
                }
            }

            Console.WriteLine("part1=" + areaCount.Max(kpv => kpv.Value));
            Console.WriteLine("part2=" + safeCount);
        }
    }
}
