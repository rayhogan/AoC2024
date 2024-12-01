using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2024
{
    public static class Day1
    {
        public static void Run()
        {
            var input = System.IO.File.ReadAllLines(@"Inputs//day1.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            // Parse
            var left = new List<int>();
            var right = new List<int>();
            foreach (var line in input)
            {
                var splited = line.Split("   ");
                left.Add(int.Parse(splited[0]));
                right.Add(int.Parse(splited[1]));
            }
            // Sort
            left.Sort();
            right.Sort();
            
            // Calculate
            var sum = 0;
            for (int i = 0; i < left.Count; i++)
            {
                sum += Math.Abs(left[i] - right[i]);
            }
            
            Console.WriteLine("Part I: "+sum);
        }
        private static void Part2(string[] input)
        {
            // Dictionary: Key, (value, multiplier)
            var dict = new Dictionary<int, (int, int)>();
            var right = new List<int>();
            // Parse input
            foreach (var line in input)
            {
                var splited = line.Split("   ");
                right.Add(int.Parse(splited[1]));

                if (!dict.ContainsKey(int.Parse(splited[0])))
                    dict.Add(int.Parse(splited[0]), (0, 1));
                else
                {
                    dict[int.Parse(splited[0])] = (dict[int.Parse(splited[0])].Item1,
                        dict[int.Parse(splited[0])].Item2 + 1);
                }
            }

            //  Find occurrences
            foreach (var num in right)
            {
                if (dict.ContainsKey(num))
                {
                    dict[num] = (dict[num].Item1 + 1, dict[num].Item2);
                }
            }
            
            // Calculate sum
            var sum = 0;
            foreach (var kvp in dict)
            {
                sum += (kvp.Key * kvp.Value.Item1) * kvp.Value.Item2;
            }
            
            Console.WriteLine("Part II: "+sum);
        }
        
    }
}
