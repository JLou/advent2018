using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day11
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = 7347;

            int[,] values = new int[300, 300];
            int[,] sums = new int[301, 301];

            for (int i = 0; i < 300; i++)
            {
                for (int j = 0; j < 300; j++)
                {
                    var rackId = j + 1 + 10;
                    var powerLevel = (rackId * (i + 1) + input) * rackId;
                    powerLevel = powerLevel / 100 % 10 - 5;

                    values[j, i] = powerLevel;
                    sums[j + 1, i + 1] = powerLevel - sums[j, i] + sums[j, i + 1] + sums[j + 1, i];

                    //Console.Write(powerLevel.ToString().PadLeft(2, ' ') + " ");
                }
                // Console.WriteLine();
            }

            var (maxsum, x, y, maxdelta) = (0, 0, 0, 0);
            for (int i = 0; i < 300 - 2; i++)
            {
                for (int j = 0; j < 300 - 2; j++)
                {
                    int sum = 0;
                    for (int delta = 0; delta < Math.Min(300 - i, 300 - j); delta++)
                    {
                        sum = sums[i, j] + sums[i + 1 + delta, j + 1 + delta] - sums[i + 1 + delta, j] - sums[i, j + 1 + delta];

                        if (sum > maxsum)
                        {
                            maxsum = sum;
                            x = i;
                            y = j;
                            maxdelta = delta;
                        }
                    }


                }
            }

            Console.WriteLine($"max coord: ({x + 1}, {y + 1} with sum {maxsum} and delta {maxdelta + 1})");
        }
    }
}
