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
            Console.WriteLine($"Part I: {ip.CalculateCheckSumSimple()}");
            
        }

        private static void Part2(string[] input)
        {
            InputParser ip = new InputParser(input[0]);
            ip.ComplexMemoryFill();            
            Console.WriteLine($"Part II {ip.CalculateCheckSumComplex()}");
        }

        
        internal class InputParser
        {
            public List<(int,int)> MemoryIndex { get;  }
            public List<int> FreeSpaces { get;  }
            
            public List<(int, int)> FreeSpaceBlocks { get; }

            public List<(int, int, int)> MemoryBlocks { get; }
            
            public InputParser(string input)
            { 
                MemoryIndex = new List<(int, int)>();
                FreeSpaces = new List<int>();
                //(start index, length)
                FreeSpaceBlocks = new List<(int, int)>();
                //(value, startIndex, length)
                MemoryBlocks = new List<(int, int, int)>();
                
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
                        // (start index, length)
                        FreeSpaceBlocks.Add((index, converted[i]));
                        // Loop through and track free index spaces
                        for (int j = converted[i]; j >= 1; j--)
                        {
                            FreeSpaces.Add(index);
                            index++;
                        }
                        
                    }
                    else // Everything else
                    {
                        // 12345
                        // 0..111....22222
                        MemoryBlocks.Add((id, index, converted[i]));
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
                int index = MemoryBlocks.Count - 1;

                // Loop thru and try reallocate file blocks
                for (int i=index; i >=0; i--)
                {
                    var fileBlock = MemoryBlocks[i];
                    for(int j=0; j<FreeSpaceBlocks.Count; j++)
                    {
                        var freeBlock = FreeSpaceBlocks[j];


                        if (freeBlock.Item2 > 0 && fileBlock.Item2 > freeBlock.Item1)
                        {
                            
                            if (fileBlock.Item3 <= freeBlock.Item2)
                            {
                                // We've identified a file that will fit
                                // So now we need to update index values to reflect this change
                                MemoryBlocks[i] = (fileBlock.Item1,freeBlock.Item1, fileBlock.Item3);

                                FreeSpaceBlocks[j] = (freeBlock.Item1+fileBlock.Item3, freeBlock.Item2-fileBlock.Item3);
                                // successfully moved so let's break out
                                break;
                            }

                            
                        }
                    }
                }

            }

            public long CalculateCheckSumSimple()
            {
                long result = 0;
                foreach (var kvp in MemoryIndex)
                {
                    result += (kvp.Item1 * kvp.Item2);
                }

                return result;
            }

            public long CalculateCheckSumComplex()
            {
                long result = 0;
                foreach (var kvp in MemoryBlocks)
                {
                    for (int i = 0; i < kvp.Item3; i++)
                    {
                        result += (kvp.Item1 * (kvp.Item2+i));
                    }
                }

                return result;
            }
        }
      
    }
}