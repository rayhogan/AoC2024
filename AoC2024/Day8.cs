using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AoC2024
{
    public static class Day8
    {
        public static void Run()
        {
            var input = System.IO.File.ReadAllLines(@"Inputs//day8.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            List<(int, int)> result = new List<(int, int)>(0);

            int xBounds = input.Length;
            int yBounds = input[0].Length;
            var map = ParseMap(input);

            foreach (var item in map)
            {
                for (int i = 0; i < item.Value.Count - 1; i++)
                {
                    for (int j = i + 1; j < item.Value.Count; j++)
                    {
                        int x1 = item.Value[i].Item1;
                        int y1 = item.Value[i].Item2;
                        int x2 = item.Value[j].Item1;
                        int y2 = item.Value[j].Item2;

                        // Calculate the direction vector
                        int deltaX = x2 - x1;
                        int deltaY = y2 - y1;

                        // Calculate the new points
                        (int newX1, int newY1) tower1 = (x1 - deltaX, y1 - deltaY); // Point before
                        (int newX2, int newY2) tower2 = (x2 + deltaX, y2 + deltaY); // Point after

                        if (tower1.newX1 < xBounds && tower1.newX1 >= 0 && tower1.newY1 < yBounds && tower1.newY1 >= 0)
                            result.Add(tower1);

                        if (tower2.newX2 < xBounds && tower2.newX2 >= 0 && tower2.newY2 < yBounds && tower2.newY2 >= 0)
                            result.Add(tower2);
                    }
                }
            }

            Console.WriteLine("Part I: " + result.Distinct().Count());
        }

        private static void Part2(string[] input)
        {
            List<(int, int)> result = new List<(int, int)>(0);

            int xBounds = input.Length;
            int yBounds = input[0].Length;
            var map = ParseMap(input);

            foreach (var item in map)
            {
                for (int i = 0; i < item.Value.Count - 1; i++)
                {
                    for (int j = i + 1; j < item.Value.Count; j++)
                    {
                        int x1 = item.Value[i].Item1;
                        int y1 = item.Value[i].Item2;
                        int x2 = item.Value[j].Item1;
                        int y2 = item.Value[j].Item2;

                        // Calculate the direction vector
                        int deltaX = x2 - x1;
                        int deltaY = y2 - y1;

                        // Calculate the new points
                        (int newX1, int newY1) tower1 = (x1, y1); // Point before
                        (int newX2, int newY2) tower2 = (x2, y2); // Point after

                        // Add in first direction
                        while (tower1.newX1 < xBounds && tower1.newX1 >= 0 && tower1.newY1 < yBounds &&
                               tower1.newY1 >= 0)
                        {
                            result.Add(tower1);
                            tower1 = (tower1.newX1 - deltaX, tower1.newY1 - deltaY); // Point before
                        }


                        while ((tower2.newX2 < xBounds && tower2.newX2 >= 0 && tower2.newY2 < yBounds &&
                                tower2.newY2 >= 0))
                        {
                            result.Add(tower2);
                            tower2 = (tower2.newX2 + deltaX, tower2.newY2 + deltaY);
                        }
                        
                    }
                }
            }

            Console.WriteLine("Part II: " + result.Distinct().Count());
        }

        private static Dictionary<string, List<(int, int)>> ParseMap(string[] input)
        {
            Dictionary<string, List<(int, int)>> map = new Dictionary<string, List<(int, int)>>();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[i][j] != '.')
                    {
                        if (map.ContainsKey(input[i][j].ToString()))
                        {
                            map[input[i][j].ToString()].Add((i, j));
                        }
                        else
                        {
                            map.Add(input[i][j].ToString(), new List<(int, int)>());
                            map[input[i][j].ToString()].Add((i, j));
                        }
                    }
                }
            }

            return map;
        }
    }
}