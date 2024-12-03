using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace AoC2024
{
    public static class Day3
    {
        private const string MUL_PATTERN = @"mul\(\d+,\d+\)";
        private const string MUL_DO_DONT_PATTERN = @"do\(\)|don't\(\)|mul\(\d+,\d+\)";
        public static void Run()
        {
            var input = System.IO.File.ReadAllLines(@"Inputs//day3.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            int sum = 0;
            foreach (var line in input)
            {
                foreach (var match in FindMatches(line, MUL_PATTERN))
                {
                    sum += CalculateResult(match.ToString());
                }
            }

            Console.WriteLine("Part I: " + sum);
        }

        private static void Part2(string[] input)
        {
            int sum = 0;
            // Let's clean up the data by removing what we don't (no pun intended) need
            StringBuilder sb = new StringBuilder();
            sb.Append("do()"); // Adding a do() at the start of my data for clarity
            foreach (var line in input)
            {
                foreach (var match in FindMatches(line, MUL_DO_DONT_PATTERN)) // Only return data that's either do(), mul(int,int) or don't()
                {
                    string result = string.Join("", match);
                    sb.Append(result);
                }
            }
            
            // A bit horrible, but now lets split the input by do()
            string[] splitDos = sb.ToString().Split("do()");
            foreach (var line in splitDos)
            {
                // If the line contains don't()s I can split again and only focus on
                // elements at index[0]
                string[] splitDonts = line.Split("don't()");
                
                // Now just a repeat of what we did in Part I
                foreach (var match in FindMatches(splitDonts[0], MUL_PATTERN))
                {
                    sum += CalculateResult(match.ToString());
                }
            }
            
            Console.WriteLine("Part II: " + sum);
        }

        private static MatchCollection FindMatches(string input, string pattern)
        {
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches;
        }
        private static int CalculateResult(string input)
        {
            int result = 0;

            if (input.StartsWith("mul"))
            {
                string[] split = input.Replace("mul(", "").Replace(")", "").Split(',');
                int a = int.Parse(split[0]);
                int b = int.Parse(split[1]);
                result = a * b;
            }

            return result;
        }
    }
}