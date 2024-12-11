using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AoC2024
{
    public static class Day10
    {
        public static void Run()
        {
            var input = System.IO.File.ReadAllLines(@"Inputs//day10.txt");
            Part1(input);
        }

        private static void Part1(string[] input)
        { 
            var ints = ParseInput(input);
            var result1 = 0;
            var result2 = 0;
            
            
            for(int x = 0;x<ints.Count;x++)
            {
                for (int y = 0; y < ints[x].Length; y++)
                {
                    if(ints[x][y] == 0)
                    {
                        var nodes = new List<(int, int)>();
                        result2 += CountTrails(x, y, ints, ref nodes);
                        result1 += nodes.Distinct().Count();
                    }
                }
            }
            Console.WriteLine($"Part I: {result1}");
            Console.WriteLine($"Part II: {result2}");

        }

        private static int CountTrails(int x, int y, List<int[]> trails, ref List<(int,int)> nodes)
        {
            int result = 0;
            //PrintMap(trails, x, y);


            // Can only look go up, down, left and right
            // Try go up
            if (x > 0)
            {
                if ((trails[x-1][y] - trails[x][y]) == 1)
                {
                    if (trails[x - 1][y] == 9)
                    {
                        result++;
                        nodes.Add((x-1, y));
                    }
                    else
                        result += CountTrails(x - 1, y, trails, ref nodes);
                }
            }
            // Look down
            if(x < trails.Count-1)
            {
                if ((trails[x + 1][y] - trails[x][y]) == 1)
                {
                    if (trails[x + 1][y] == 9)
                    {
                        nodes.Add((x+1, y));
                        result++;
                    }
                    else
                        result += CountTrails(x + 1, y, trails, ref nodes);
                }
            }

            // Look left
            if (y > 0)
            {
                if ((trails[x][y-1] - trails[x][y]) == 1)
                {
                    if (trails[x][y - 1] == 9)
                    {
                        nodes.Add((x, y-1));
                        result++;
                    }
                    else
                        result += CountTrails(x, y - 1, trails, ref nodes);
                }
            }

            // Look right
            if(y < trails[0].Length-1)
            {
                if ((trails[x][y + 1] - trails[x][y]) == 1)
                {
                    if (trails[x][y + 1] == 9)
                    {
                        nodes.Add((x, y+1));
                        result++;
                    }
                    else
                        result += CountTrails(x, y + 1, trails, ref nodes);
                }
            }

            return result;
        }

        private static List<int[]> ParseInput(string[] input)
        {
            var result = new List<int[]>();

            foreach (string line in input)
            {
                var parsedInts = line.Select(c => int.Parse(c.ToString())).ToArray();
                result.Add(parsedInts);
            }

            return result;
        }

        private static void PrintMap(List<int[]> map, int xpos, int ypos)
        {
            for (int x = 0; x < map.Count; x++)
            {
                for (int y = 0; y < map[x].Length; y++)
                {
                    if(x == xpos && y == ypos)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(map[x][y]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(map[x][y]);
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("#######");
        }
        
      
    }
}