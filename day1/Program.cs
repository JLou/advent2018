using System;
using System.Collections.Generic;
using System.IO;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            string line;
            HashSet<int> values = new HashSet<int>();
            bool found = false;
            bool firstLoop = true;
            while(!found)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@"input.txt");  
                while((line = file.ReadLine()) != null)  
                {
                    if(!values.Add(count))
                    {
                        Console.WriteLine("repeated: " + count);
                        found = true;
                        break;
                    }
                    count += int.Parse(line);  
                }  
                if(firstLoop){
                    Console.WriteLine(count);    
                    firstLoop = false;
                }
            }
            


            Console.Read();
        }
    }
}
