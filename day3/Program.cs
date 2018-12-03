using System;

namespace day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            using (var file = new System.IO.StreamReader(@"input.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    //Part 1


                    //Part 2
                }

                file.Close();
            }
        }
    }
}
