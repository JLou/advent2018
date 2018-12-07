using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string line, originalline;
            using (var file = new System.IO.StreamReader(@"input.txt"))
            {
                line = file.ReadLine();
            }
            originalline = line;
            //part1
            if (false)
            {
                ReactPolymer(line);
            }
            else
            {
                int minSize = int.MaxValue;

                for (int c = 'a'; c <= 'z'; c++)
                {
                    var str = originalline.Replace(((char)c).ToString(), string.Empty);
                    str = str.Replace((Char.ToUpper((char)c)).ToString(), string.Empty);
                    var size = ReactPolymer(str);


                    minSize = size < minSize ? size : minSize;
                }

                Console.WriteLine("min polymer: " + minSize);
            }
        }

        private static int ReactPolymer(string polymer)
        {
            StringBuilder sb = new StringBuilder(polymer);

            for (int i = 0; i < sb.Length; i++)
            {
                if (i == sb.Length - 1)
                {
                    break;
                }
                if (!(Char.ToLower(sb[i]) == Char.ToLower(sb[i + 1]) && sb[i] != sb[i + 1]))
                {
                }
                else
                {
                    sb.Remove(i, 2);
                    i -= 2;
                    if (i < -1)
                    {
                        i = -1;
                    }
                }
            }


            return sb.Length;
        }
    }
}
