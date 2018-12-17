using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day12
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            string initialstate;

            Dictionary<string, char> combinaisons = new Dictionary<string, char>();
            using (var file = new System.IO.StreamReader(@"input.txt"))
            {
                initialstate = file.ReadLine();

                while ((line = file.ReadLine()) != null)
                {
                    var pattern = line.Substring(0, 5);
                    combinaisons.Add(pattern, line[9]);
                }
            }

            var gen = initialstate.Select((el, i) => (el, i)).ToDictionary(x => x.i, x => x.el);
            int minPlant = 0, maxplant = initialstate.Length - 1;

            int deltawithprev = 0;
            int prev = 0;
            for (long i = 0; i < 200; i++)
            {
                var nextGen = new Dictionary<int, char>(gen);
                for (int j = minPlant - 4; j < maxplant + 4; j++)
                {

                    string pattern = "";
                    for (int delta = 0; delta < 5; delta++)
                    {
                        pattern += GetValue(j - 2 + delta);
                    }

                    if (nextGen.ContainsKey(j))
                    {
                        nextGen[j] = combinaisons[pattern];

                    }
                    else if (combinaisons[pattern] == '#')
                    {
                        nextGen[j] = combinaisons[pattern];
                        minPlant = Math.Min(minPlant, j);
                        maxplant = Math.Max(maxplant, j);
                    }
                    // if (nextGen.ContainsKey(j)) Console.Write(nextGen[j]);
                }

                gen = nextGen;
                // Console.WriteLine();
                var v = gen.Aggregate(0, (acc, kpv) => acc + (kpv.Value == '#' ? kpv.Key : 0));
                deltawithprev = v - prev;
                prev = v;
                Console.WriteLine(i + ":" + deltawithprev);
                // Console.WriteLine("count of plants =" + gen.Aggregate(0, (acc, kpv) => acc + (kpv.Value == '#' ? kpv.Key : 0)));
            }
            long finalval = prev + (50000000000 - 200) * 42;
            Console.WriteLine("after 50M=" + finalval);


            char GetValue(int index)
            {
                if (gen.ContainsKey(index))
                {
                    return gen[index];
                }
                return '.';
            }
        }
    }
}
