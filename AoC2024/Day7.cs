using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AoC2024
{
    public static class Day7
    {
        public static void Run()
        {
            var input = System.IO.File.ReadAllLines(@"Inputs//day7.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            long result = 0;
            foreach (var line in input)
            {
                var parts = line.Split(":");
                long sum = long.Parse(parts[0]);
                int[] numbers = parts[1].Trim().Split(' ').Select(int.Parse).ToArray();

                if (CheckEquation(numbers, sum))
                    result += sum;
            }
            
            Console.WriteLine("Part I: "+result);
        }

        private static void Part2(string[] input)
        {
            long result = 0;
            foreach (var line in input)
            {
                var parts = line.Split(":");
                long sum = long.Parse(parts[0]);
                int[] numbers = parts[1].Trim().Split(' ').Select(int.Parse).ToArray();

                if (CheckEquation(numbers, sum, 3)) 
                    result += sum;
            }
            
            Console.WriteLine("Part II: "+result);
        }

        private static bool CheckEquation(int[] numbers, long targetSum, int numOperators = 2)
        {
            int n = numbers.Length;
            int operatorCombinations = (int)Math.Pow(numOperators, n - 1);
            bool targetFound = false;
            Parallel.For(0, operatorCombinations, (i, state) =>
            {
                if (targetFound) return; 

                long result = numbers[0];

                int temp = i; 
                for (int j = 0; j < n - 1; j++)
                {
                    int op = temp % numOperators; 
                    temp /= numOperators;         

                    switch (op)
                    {
                        case 0: // +
                            result += numbers[j + 1];
                            break;
                        case 1: // *
                            result *= numbers[j + 1];
                            break;
                        case 2: // ||
                            string number = $"{result}{numbers[j + 1]}";
                            result = long.Parse(number);
                            break;
                    }
                }

                if (result == targetSum)
                {
                    targetFound = true;
                    state.Stop();
                }
            });
            
            return targetFound;
        }
    }
}