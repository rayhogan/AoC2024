using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AoC2024
{
    public static class Day9
    {
        public static void Run()
        {
            var input = System.IO.File.ReadAllLines(@"Inputs//day9.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            // 12345
            // 0..111....22222
            InputParser ip = new InputParser(input[0]);            
            ip.SimpleMemoryFill();
            Console.WriteLine($"Part I: {ip.CalculateCheckSum()}");
            
        }

        private static void Part2(string[] input)
        {
            
        }

        
        internal class InputParser
        {
            public List<(int,int)> MemoryIndex { get;  }
            public List<int> FreeSpaces { get;  }
            
            public List<(int, int)> FreeSpaceBlocks { get; }
            
            public InputParser(string input)
            { 
                MemoryIndex = new List<(int, int)>();
                FreeSpaces = new List<int>();
                FreeSpaceBlocks = new List<(int, int)>();
                
                var converted = input.Select(c => int.Parse(c.ToString())).ToArray();
                
                // 12345
                // 0..111....22222
                int index = 0;
                int id = 0;
                for (int i = 0; i < converted.Length; i++)
                {
                    // Free space calculator
                    if ((i + 1) % 2 == 0)
                    {
                        // Loop through and track free index spaces
                        for (int j = converted[i]; j >= 1; j--)
                        {
                            FreeSpaces.Add(index);
                            index++;
                        }
                    }
                    else // Everything else
                    {
                        // Loop through and track our memory usage
                        for (int j = converted[i]; j >= 1; j--)
                        {
                            MemoryIndex.Add((index, id));
                            index++;
                        }

                        id++; // Increment our ID
                    }
                }
            }

            public void SimpleMemoryFill()
            {
                int index = MemoryIndex.Count-1;
                foreach (int i in FreeSpaces)
                {
                    // Exit conditions
                    if (index < 0)
                        throw new Exception("A terrible thing has happened here");

                    if (i > MemoryIndex[index].Item1)
                        break;
                    
                    MemoryIndex[index] = (i, MemoryIndex[index].Item2);
                    index--;
                }
            }

            public void ComplexMemoryFill()
            {
                // Track starting positions of free spaces
                // as well as memory blocks
                // Iterate over memory block locations and if there's space
                // figure out how to update their values
                // Maybe dictionary of key: ID, value: startindex, count
            }

            public long CalculateCheckSum()
            {
                long result = 0;
                foreach (var kvp in MemoryIndex)
                {
                    result += (kvp.Item1 * kvp.Item2);
                }

                return result;
            }
        }
      
    }
}