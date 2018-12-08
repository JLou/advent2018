using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day7
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            List<(string pre, string post)> dependencies = new List<(string, string)>();
            using (var file = new System.IO.StreamReader(@"input.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    dependencies.Add((line[5].ToString(), line[36].ToString()));
                }
            }
            var allSteps = dependencies.Select(x => x.pre).Concat(dependencies.Select(x => x.post)).Distinct().OrderBy(x => x).ToList();

            //Part1
            // var result = "";
            // while (allSteps.Any())
            // {
            //     var valid = allSteps.Where(s => !dependencies.Any(d => d.post == s)).First();
            //     result += valid;

            //     allSteps.Remove(valid);
            //     dependencies.RemoveAll(d => d.pre == valid);
            // }
            // Console.WriteLine(result);


            (string task, int duration)[] workers = new (string, int)[5];
            //Part2
            int totalSeconds = 0;
            HashSet<string> currentTasks = new HashSet<string>();
            while (allSteps.Any())
            {
                var valids = allSteps.Where(s => !currentTasks.Contains(s) && !dependencies.Any(d => d.post == s)).ToList();
                if (valids.Any())
                {

                    for (int i = 0; i < workers.Length; i++)
                    {

                        if (workers[i].duration <= 0 && valids.Any())
                        {
                            var task = valids.First();
                            workers[i].task = task;
                            workers[i].duration = 60 + (task[0] - 'A' + 1);

                            Console.WriteLine("Task " + task + " added to worker #" + i + " for " + workers[i].duration + " seconds");
                            valids.Remove(task);
                            currentTasks.Add(task);
                        }
                    }
                }

                for (int i = 0; i < workers.Length; i++)
                {
                    workers[i].duration--;
                    if (workers[i].duration == 0)
                    {
                        Console.WriteLine("task " + workers[i].task + " finished after " + totalSeconds + " seconds");
                        allSteps.Remove(workers[i].task);
                        dependencies.RemoveAll(d => d.pre == workers[i].task);
                    }
                }

                totalSeconds++;
            }

            Console.WriteLine($"Total seconds: {totalSeconds}");

        }
    }
}
