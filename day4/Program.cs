using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            SortedList<DateTime, Input> inputs = new SortedList<DateTime, Input>();

            using (var file = new System.IO.StreamReader(@"input.txt"))
            {
                Regex r = new Regex(@"\[(\d+)-(\d+)-(\d+) (\d+):(\d+)\] (.+)");
                int currentGuard = 0;
                while ((line = file.ReadLine()) != null)
                {
                    var m = r.Match(line);
                    var date = new DateTime(
                            int.Parse(m.Groups[1].Value),
                            int.Parse(m.Groups[2].Value),
                            int.Parse(m.Groups[3].Value),
                            int.Parse(m.Groups[4].Value),
                            int.Parse(m.Groups[5].Value),
                            0
                        );

                    Input input = new Input();
                    switch (m.Groups[6].Value[0])
                    {
                        case 'G':
                            input.Action = Action.NewGuard;
                            currentGuard = int.Parse(m.Groups[6].Value.Split(' ')[1].Substring(1));
                            break;
                        case 'f':
                            input.Action = Action.Asleep;
                            break;
                        case 'w':
                            input.Action = Action.WakesUp;
                            break;
                    }
                    input.GuardId = currentGuard;
                    inputs.Add(date, input);
                }
            }
            Dictionary<int, int> totalSlept = new Dictionary<int, int>();
            Dictionary<int, int[]> hoursSlept = new Dictionary<int, int[]>();

            int startSleepMinute = 0;
            int guardId = 0;
            foreach (var input in inputs)
            {

                if (input.Value.Action == Action.NewGuard)
                {
                    guardId = input.Value.GuardId;
                }
                if (!totalSlept.ContainsKey(guardId))
                {
                    totalSlept.Add(guardId, 0);
                    hoursSlept.Add(guardId, new int[60]);
                }
                if (input.Value.Action == Action.Asleep)
                {
                    startSleepMinute = input.Key.Minute;
                }

                if (input.Value.Action == Action.WakesUp)
                {
                    var totalMinutes = input.Key.Minute - startSleepMinute;
                    totalSlept[guardId] += totalMinutes;
                    for (int i = 0; i < totalMinutes; i++)
                    {
                        hoursSlept[guardId][i + startSleepMinute]++;
                    }
                }
            }

            var lazyGuy = totalSlept.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            int maxValue = hoursSlept[lazyGuy].Max();
            int maxIndex = hoursSlept[lazyGuy].ToList().IndexOf(maxValue);
            Console.WriteLine("answer1: " + lazyGuy * maxIndex);

            (int guard2, int minute, int total) = (0, 0, 0);
            foreach (var (guardid, schedule) in hoursSlept)
            {
                int maxValue2 = schedule.Max();

                if (total < maxValue2)
                {
                    minute = schedule.ToList().IndexOf(maxValue2);
                    total = maxValue2;
                    guard2 = guardid;
                }
            }

            Console.WriteLine("answer2: " + guard2 * minute);
        }

        class Input
        {
            public Action Action { get; set; }

            public int GuardId { get; set; }
        }

        private enum Action
        {
            NewGuard = 1,
            Asleep = 2,
            WakesUp = 3
        }
    }
}
