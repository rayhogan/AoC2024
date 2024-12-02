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
                if (DetermineSafety(numbers).Result)
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

        private static DetermineSafetyResult DetermineSafety(List<int> numbers, int lower = 1, int upper = 3)
        {
            Direction counterDirection = Direction.Undetermined;
            for (int i = 0; i < numbers.Count-1; i++)
            {
                int difference = Math.Abs(numbers[i] - numbers[i + 1]);
                if (difference < lower || difference > upper)
                    return new DetermineSafetyResult(false, i);

                var direction = DetermineDirection(numbers[i], numbers[i + 1]);

                if (counterDirection == Direction.Undetermined)
                    counterDirection = direction;
                else if (counterDirection != direction)
                    return new DetermineSafetyResult(false, i);
            }

            return new DetermineSafetyResult(true, -1);

        }
        
        private static bool DetermineSafety2(List<int> numbers, int lower = 1, int upper = 3)
        {
            // Check line to see if it's safe or unsafe (before modification)
            DetermineSafetyResult dsr = DetermineSafety(numbers);
            
            if (!dsr.Result) // Found an unsafe line!
            {
                // Let's see if it can be made safe ...
                int i = dsr.BadIndex;
                if (i <=  1) // If issue detected at first or second element
                {
                    var removeFirst  = numbers.Where((item, index) => index != 0).ToList();
                    var removeSecond = numbers.Where((item, index) => index != 1).ToList();
                    var result1 = DetermineSafety(removeFirst);
                    var result2 = DetermineSafety(removeSecond);
                    if (!result1.Result && !result2.Result)
                        return false; // Not safe! 
                }
                else // Else check safety by removing the problematic element (i + 1)
                {
                    var removeElement = numbers.Where((item, index) => index != i + 1).ToList();
                    var result1 = DetermineSafety(removeElement);
                    
                    // Return it's safety Result
                    return result1.Result;
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

    /// <summary>
    /// Result class that holds information regarding the safety checks
    /// done on the collection of Integers.
    /// </summary>
    internal class DetermineSafetyResult
    {
        /// <summary>
        /// The Result True/False whether the collections of ints
        /// are safe or unsafe.
        /// </summary>
        public Boolean Result { get; private set; }
        /// <summary>
        /// The Index value of the element that failed the safety checks.
        /// </summary>
        public int BadIndex { get; private set; }
        public DetermineSafetyResult(Boolean result, int badIndex)
        {
            Result = result;
            BadIndex = badIndex;
        }
        
    }
}
