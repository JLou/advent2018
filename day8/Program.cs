using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day8
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            int[] numbers = new int[0];
            using (var file = new System.IO.StreamReader(@"input.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    numbers = line.Split(" ").Select(c => int.Parse(c)).ToArray();
                }
            }

            Stack<Node> nodes = new Stack<Node>();
            Node root = null;
            int k = 0;
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++, k++)
            {
                if (k % 2 == 0)
                {
                    var node = new Node(numbers[i]);
                    if (nodes.TryPeek(out var parent))
                    {
                        parent.Push(node);
                    }
                    nodes.Push(node);

                }
                if (k % 2 == 1)
                {
                    nodes.Peek().NumberMetadata = numbers[i];
                    nodes.Peek().Metadata = new int[numbers[i]];
                    while (nodes.TryPeek(out var parent) && parent.RemainingChildren == 0)
                    {
                        var endNode = nodes.Pop();
                        for (int j = 1; j < endNode.Metadata.Length + 1; j++)
                        {
                            endNode.Metadata[j - 1] = numbers[i + j];
                            sum += numbers[i + j];
                        }
                        i += endNode.Metadata.Length;
                        root = endNode;
                        var v = root.ComputeValue();

                        Console.WriteLine($"Computed node {root.Name} = {v}");
                    }
                }


            }


            Console.WriteLine("sum is " + sum);
            Console.WriteLine("part 2:" + root.Value);
        }



        public class Node
        {
            private static char letter = 'A';

            public char Name;
            public Node[] Children;

            private int childrenIndex = 0;

            public int NumberMetadata;

            public int[] Metadata;

            public int RemainingChildren => Children.Length - childrenIndex;

            private int _value = -1;
            public int Value => _value == -1 ? _value = ComputeValue() : _value;
            public Node(int nbChildren)
            {
                NumberMetadata = 0;
                Children = new Node[nbChildren];
                Metadata = new int[0];
                Name = letter;
                letter++;
            }

            public bool Push(Node n)
            {
                if (childrenIndex >= Children.Count())
                {
                    return false;
                }
                Children[childrenIndex] = n;
                childrenIndex++;

                return true;
            }

            public override string ToString()
            {
                return Name.ToString();
            }

            public int ComputeValue()
            {

                if (Children.Length == 0)
                {
                    _value = Metadata.Sum();
                }
                else
                {
                    _value = Metadata.Aggregate(0, (acc, next) => acc + ((next == 0 || Children.Length < next) ? 0 : Children[next - 1].Value));
                }

                return _value;
            }
        }
    }
}
