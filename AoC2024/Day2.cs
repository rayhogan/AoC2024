using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2024
{
    public static class Day2
    {
        public static void Run()
        {
            var input = System.IO.File.ReadAllLines(@"Inputs//day2.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            var sum = 0;
            foreach (var line in input)
            {
                List<int> numbers = line.Split(" ").Select(int.Parse).ToList();
                if (DetermineSafety(numbers))
                    sum++;
            }
            
            Console.WriteLine("Part I: "+sum);
        }
        private static void Part2(string[] input)
        {
            var sum = 0;
            foreach (var line in input)
            {
                List<int> numbers = line.Split(" ").Select(int.Parse).ToList();
                if (DetermineSafety2(numbers))
                    sum++;
            }
            
            Console.WriteLine("Part II: "+sum);
        }

        private static bool DetermineSafety(List<int> numbers, int lower = 1, int upper = 3)
        {
            Direction counterDirection = Direction.Undetermined;
            for (int i = 0; i < numbers.Count-1; i++)
            {
                int difference = Math.Abs(numbers[i] - numbers[i + 1]);
                if (difference < lower || difference > upper)
                    return false;

                var direction = DetermineDirection(numbers[i], numbers[i + 1]);

                if (counterDirection == Direction.Undetermined)
                    counterDirection = direction;
                else if (counterDirection != direction)
                    return false;
            }

            return true;

        }
        
        private static bool DetermineSafety2(List<int> numbers, int lower = 1, int upper = 3)
        {
            Direction counterDirection = Direction.Undetermined;
            int i = 0;
            for (i = 0; i < numbers.Count-1; i++)
            {
                int difference = Math.Abs(numbers[i] - numbers[i + 1]);
                if (difference < lower || difference > upper)
                    break;

                var direction = DetermineDirection(numbers[i], numbers[i + 1]);

                if (counterDirection == Direction.Undetermined)
                    counterDirection = direction;
                else if (counterDirection != direction)
                    break;
            }

            if (i < numbers.Count - 1) // Check if unsafe lines can be made Safe
            {

                if (i <=  1) // If issue detected at first or second element
                {
                    var removeFirst  = numbers.Where((item, index) => index != 0).ToList();
                    var removeSecond = numbers.Where((item, index) => index != 1).ToList();
                    bool result1 = DetermineSafety(removeFirst);
                    bool result2 = DetermineSafety(removeSecond);
                    if (!result1 && !result2)
                        return false; // Not safe! 
                }
                else // Else check safety by removing the problematic element (i + 1)
                {
                    var removeElement = numbers.Where((item, index) => index != i + 1).ToList();
                    bool result1 = DetermineSafety(removeElement);
                    
                    // return true or false
                    return result1;
                }

            }
            // Safe!
            return true;
        }

        private static Direction DetermineDirection(int a, int b)
        {
            if (a > b)
                return Direction.Decreasing;
            else if (a < b)
                return Direction.Increasing;
            else
            {
                return Direction.Undetermined;
            }
        }
        
        private enum Direction
        {
            Undetermined = 0,
            Increasing = 1,
            Decreasing = 2
        }
        
    }
}
