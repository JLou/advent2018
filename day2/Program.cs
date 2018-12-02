using System;
using System.Collections.Generic;
using System.Linq;

namespace day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            int twoCount = 0, threeCount = 0;
            List<string> ids = new List<string>();

            System.IO.StreamReader file = new System.IO.StreamReader(@"input.txt");
            while ((line = file.ReadLine()) != null)
            {
                //Part 1
                Dictionary<char, int> counts = new Dictionary<char, int>();
                var vals = line.GroupBy(c => c)
                               .Select(kv => kv.Count())
                               .Distinct();

                foreach (var count in vals)
                {
                    if (count == 2) twoCount++;
                    if (count == 3) threeCount++;
                }

                //Part 2
                bool matchFound = false;
                if (!matchFound)
                {
                    foreach (var id in ids)
                    {
                        int diff = 0;
                        string match = "";
                        for (int i = 0; i < id.Length; i++)
                        {
                            if (id[i] != line[i]) diff++;
                            else match += id[i];

                            if (diff > 1) break;

                        }
                        if (diff == 1)
                        {
                            Console.WriteLine("Matching boxes have in common: " + match);
                            matchFound = true;
                        }
                    }
                }

                ids.Add(line);
            }

            Console.WriteLine("checksum:" + twoCount * threeCount);

        }
    }
}
