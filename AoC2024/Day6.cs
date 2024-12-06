using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AoC2024
{
    public static class Day6
    {
        public static void Run()
        {
            var input = System.IO.File.ReadAllLines(@"Inputs//day6.txt");
            Part1(input);
        }

        private static void Part1(string[] input)
        {
            var startingPost = FindStartingPosition(input);
            var result = CountSteps(input, startingPost);
            Console.WriteLine("Part I: " + result.Count());


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Part2(input, result, startingPost);
            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void Part2(string[] input, Dictionary<(int x, int y), int> result, (int, int) startingPosition)
        {
            result.Remove(startingPosition);
            int count = 0;
            foreach (var kvp in result)
            {
                // Copy the map
                string[] copied = new string[input.Length];
                Array.Copy(input, copied, input.Length);
                
                char[] charArray = input[kvp.Key.x].ToCharArray();
                charArray[kvp.Key.y] = '#';
                copied[kvp.Key.x] = new string(charArray);
                
                // Forgive me father for I have sinned
                try
                {
                    CountSteps(copied, startingPosition);
                }
                catch (Exception e)
                {
                    count++;
                }
                
            }

            Console.WriteLine("Part II: " + count);
        }

        private static (int, int) FindStartingPosition(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0 + 1; j < input[i].Length; j++)
                {
                    if (input[i][j] == '^')
                        return (i, j);
                }
            }

            return (0, 0);
        }

        private static Dictionary<(int, int), int> CountSteps(string[] input, (int x, int y) startingPosition)
        {
            Direction dir = Direction.North;
            int x = startingPosition.x;
            int y = startingPosition.y;
            Dictionary<(int, int), int> tracker = new Dictionary<(int, int), int>();
            tracker.Add((x, y), 0);

            while (true)
            {
                //Console.WriteLine("=======================");
                //DrawMap(input, x, y);
                if (dir == Direction.North)
                {
                    if (x == 0) // Stepped upwards off the map
                    {
                        return tracker;
                    }
                    else if (input[x - 1][y] == '.' || input[x - 1][y] == '^')
                    {
                        if (tracker.ContainsKey((x - 1, y)))
                        {
                            tracker[(x - 1, y)]++;
                        }
                        else
                        {
                            tracker.Add((x - 1, y), 0);
                        }

                        x--;
                    }
                    else if (input[x - 1][y] == '#')
                    {
                        dir = Direction.East;
                    }
                }

                if (dir == Direction.East)
                {
                    if (y == input[x].Length - 1) // Stepped right off the map
                    {
                        return tracker;
                    }
                    else if (input[x][y + 1] == '.' || input[x][y + 1] == '^')
                    {
                        if (tracker.ContainsKey((x, y + 1)))
                        {
                            tracker[(x, y + 1)]++;
                        }
                        else
                        {
                            tracker.Add((x, y + 1), 0);
                        }

                        y++;
                    }
                    else if (input[x][y + 1] == '#')
                    {
                        dir = Direction.South;
                    }
                }

                if (dir == Direction.South)
                {
                    if (x == input.Length - 1) // Stepped right off the map
                    {
                        return tracker;
                    }
                    else if (input[x + 1][y] == '.' || input[x + 1][y] == '^')
                    {
                        if (tracker.ContainsKey((x + 1, y)))
                        {
                            tracker[(x + 1, y)]++;
                        }
                        else
                        {
                            tracker.Add((x + 1, y), 0);
                        }

                        x++;
                    }
                    else if (input[x + 1][y] == '#')
                    {
                        dir = Direction.West;
                    }
                }

                if (dir == Direction.West)
                {
                    if (y == 0) // Stepped right off the map
                    {
                        return tracker;
                    }
                    else if (input[x][y - 1] == '.' || input[x][y - 1] == '^')
                    {
                        if (tracker.ContainsKey((x, y - 1)))
                        {
                            tracker[(x, y - 1)]++;
                        }
                        else
                        {
                            tracker.Add((x, y - 1), 0);
                        }

                        y--;
                    }
                    else if (input[x][y - 1] == '#')
                    {
                        dir = Direction.North;
                    }

                    // Hacky.com :/
                    if (tracker.Count() >= 2)
                    {
                        int testCheck = tracker.Count(kv => kv.Value > 2);
                        if (testCheck > 0)
                            throw new Exception("Caught in a loop");
                    }
                }
            }

            return tracker;
        }

        private static void DrawMap(string[] map, int x, int y)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (i == x && j == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('O');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(map[i][j]);
                    }
                }

                Console.WriteLine();
            }
        }
        enum Direction
        {
            North = 1,
            South = 2,
            East = 3,
            West = 4
        }
    }
}