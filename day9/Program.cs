using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day9
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            using (var file = new System.IO.StreamReader(@"input.txt"))
            {
                line = file.ReadLine();
            }

            var chunks = line.Split(" ");
            int nbPlayers = int.Parse(chunks[0]), maxPoint = int.Parse(chunks[6]);

            Console.WriteLine(nbPlayers + " " + maxPoint);

            LinkedList<int> marbles = new LinkedList<int>();

            marbles.AddFirst(0);
            var current = marbles.AddLast(1);

            var scores = new long[nbPlayers];
            for (int i = 2; i < maxPoint; i++)
            {
                if (i % 23 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        current = marbles.First == current ? marbles.Last : current.Previous;
                    }
                    scores[i % nbPlayers] += current.Value + i;

                    var next = current.Next;

                    marbles.Remove(current);
                    current = next;

                }
                else
                {
                    var next = marbles.Last == current ? marbles.First : current.Next;
                    current = marbles.AddAfter(next, i);
                }
            }

            Console.WriteLine("High score=" + scores.Max());
        }
    }
}
