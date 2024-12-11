using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AoC2024
{
    public static class Day11
    {
        public static void Run()
        {
            var input = System.IO.File.ReadAllLines(@"Inputs//day11.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            Console.WriteLine($"Part I: {CountStones(input[0], 25)}");
        }

        private static void Part2(string[] input)
        {
            long result = CountSmarter(input[0], 75);
            Console.WriteLine($"Part II {result}");
        }

        private static long CountSmarter(string input, int iterations)
        {
            long result = 0;
            Dictionary<long, long> stoneMap = new Dictionary<long, long>();

            var converted = input.Split(" ").Select(c => long.Parse(c.ToString())).ToList();
            foreach (var stone in converted)
            {
                stoneMap[stone] = 1;
            }
            int blinks = iterations;
            while (blinks > 0)
            {
                Dictionary<long, long> buffer = new Dictionary<long, long>();

                foreach (var stone in stoneMap)
                {
                    if (stone.Key == 0)
                    {
                        if (buffer.TryGetValue(1, out long value))
                        {
                            buffer[1] = stone.Value + value;
                        }
                        else
                        {
                            buffer[1] = stone.Value;
                        }
                    }
                    else if (stone.Key.ToString().Count() % 2 == 0)
                    {
                        int l = stone.Key.ToString().Count();

                        if (buffer.TryGetValue(long.Parse(stone.Key.ToString().Substring(0, l / 2)), out long value))
                        {
                            buffer[long.Parse(stone.Key.ToString().Substring(0, l / 2))] = value + stone.Value;
                        }
                        else
                        {
                            buffer[long.Parse(stone.Key.ToString().Substring(0, l / 2))] = stone.Value;
                        }

                        if (buffer.TryGetValue(long.Parse(stone.Key.ToString().Substring(l / 2, l / 2)), out value))
                        {
                            buffer[long.Parse(stone.Key.ToString().Substring(l / 2, l / 2))] = value + stone.Value;
                        }
                        else
                        {
                            buffer[long.Parse(stone.Key.ToString().Substring(l / 2, l / 2))] = stone.Value;
                        }

                    }
                    else
                    {

                        if (buffer.TryGetValue((stone.Key * 2024), out long value))
                        {
                            buffer[(stone.Key * 2024)] = stone.Value + value;
                        }
                        else
                        {
                            buffer[(stone.Key * 2024)] = stone.Value;
                        }

                    }
                }
                stoneMap.Clear();

                foreach (var kvp in buffer)
                {
                    stoneMap.Add(kvp.Key, kvp.Value);
                }
                buffer.Clear();
                blinks--;
            }

            foreach(var kvp in stoneMap)
            {
                result +=  kvp.Value;
            }

            return result;
        }

        private static int CountStones(string input, int iterations)
        {
            var converted = input.Split(" ").Select(c => long.Parse(c.ToString())).ToList();
            int blinks = iterations;
            while (blinks > 0)
            {
                List<long> buffer = new List<long>();

                for (int i = 0; i < converted.Count; i++)
                {
                    if (converted[i] == 0)
                        buffer.Add(1);
                    else if (converted[i].ToString().Count() % 2 == 0)
                    {
                        int l = converted[i].ToString().Count();
                        buffer.Add(long.Parse(converted[i].ToString().Substring(0, l / 2)));
                        buffer.Add(long.Parse(converted[i].ToString().Substring(l / 2, l / 2)));

                    }
                    else
                    {
                        buffer.Add(converted[i] * 2024);
                    }
                }
                converted.Clear();
                converted.AddRange(buffer);

                blinks--;
            }
            return converted.Count;
        }




    }
}