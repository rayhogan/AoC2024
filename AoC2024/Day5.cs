using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace AoC2024
{
    public static class Day5
    {
        public static void Run()
        {
            var input = System.IO.File.ReadAllLines(@"Inputs//day5.txt");
            Part1(input);
        }

        private static void Part1(string[] input)
        {
            int count = 0;
            List<int[]> validUpdates = new List<int[]>();
            List<int[]> invalidUpdates = new List<int[]>();
            var instructions = ParseInput(input);

            foreach (var line in instructions.Item2)
            {
                // Parse string of ints into int array
                var intArray = line.Split(',').Select(i => int.Parse(i)).ToArray();
                bool valid = true;
                for (int i = 0; i < intArray.Length-1; i++)
                {
                    if (!instructions.Item1.Contains(string.Format("{0}|{1}", intArray[i], intArray[i + 1])))
                    {
                        valid = false;
                        invalidUpdates.Add(intArray); // For Part II
                        break;
                    }
                }
                
                if(valid)
                    count += intArray[(intArray.Length)/2];
            }
            
            Console.WriteLine("Part I: "+count);
            
            count = 0;
            // Part II
            foreach (var invalid in invalidUpdates)
            {
                for (int i = 0; i < invalid.Length-1; i++)
                {
                    if (instructions.Item1.Contains(string.Format("{1}|{0}", invalid[i], invalid[i + 1]))) // Instructions say they're in the wrong order
                    {
                        // Swap them
                        (invalid[i], invalid[i+1]) = (invalid[i+1], invalid[i]);
                        i = -1; // Start again (slow, I know)
                    }
                }
                count += invalid[(invalid.Length)/2];
            }
            Console.WriteLine("Part II: "+count);
        }

        private static (List<string>, List<string>) ParseInput(string[] input)
        {
            List<string> rules = new List<string>();
            List<string> updates = new List<string>();
            foreach (string line in input)
            {
                if (line.Contains('|'))
                {
                    rules.Add(line);
                }
                else if (line.Contains(','))
                {
                    updates.Add(line);
                }
            }
            return (rules, updates);
        }

        
    }
}