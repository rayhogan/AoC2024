using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace AoC2024
{
    public static class Day4
    {
        public static void Run()
        {
            var input = System.IO.File.ReadAllLines(@"Inputs//day4.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            List<String> potentials = new List<String>();
            potentials.Add("XMAS");
            potentials.Add("SAMX");
            var height = input.Length;
            var width = input[0].Length;
            var count = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (input[i][j] == 'X')
                    {
                        // Check to my left
                        if (j >= 3) // Is there even enough room to my left?
                        {
                            // HIT
                            if (potentials.Contains(input[i].Substring(j - 3, 4)))
                            {
                                count++;
                            }
                        }

                        // Check to my right
                        if (j <= (width - 4)) // Is there even enough room to my right?
                        {
                            // HIT
                            if (potentials.Contains(input[i].Substring(j, 4)))
                            {
                                count++;
                            }
                        }

                        // Check upwards
                        if (i >= 3) // Is there room above me?
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(input[i][j]);
                            sb.Append(input[i - 1][j]);
                            sb.Append(input[i - 2][j]);
                            sb.Append(input[i - 3][j]);

                            if (potentials.Contains(sb.ToString()))
                            {
                                count++;
                            }
                        }

                        // Check downwards
                        if (i <= (height - 4))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(input[i][j]);
                            sb.Append(input[i + 1][j]);
                            sb.Append(input[i + 2][j]);
                            sb.Append(input[i + 3][j]);

                            if (potentials.Contains(sb.ToString()))
                            {
                                count++;
                            }
                        }

                        // Check top left
                        if (i >= 3 && j >= 3) // Is there room above me?
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(input[i][j]);
                            sb.Append(input[i - 1][j - 1]);
                            sb.Append(input[i - 2][j - 2]);
                            sb.Append(input[i - 3][j - 3]);

                            if (potentials.Contains(sb.ToString()))
                            {
                                count++;
                            }
                        }

                        // Check top right
                        if (i >= 3 && j <= (width - 4)) // Is there room above me?
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(input[i][j]);
                            sb.Append(input[i - 1][j + 1]);
                            sb.Append(input[i - 2][j + 2]);
                            sb.Append(input[i - 3][j + 3]);

                            if (potentials.Contains(sb.ToString()))
                            {
                                count++;
                            }
                        }

                        // Check bottom left
                        if (i <= (height - 4) && j >= 3)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(input[i][j]);
                            sb.Append(input[i + 1][j - 1]);
                            sb.Append(input[i + 2][j - 2]);
                            sb.Append(input[i + 3][j - 3]);

                            if (potentials.Contains(sb.ToString()))
                            {
                                count++;
                            }
                        }

                        // Check bottom right 
                        if (i <= (height - 4) && j <= (width - 4))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(input[i][j]);
                            sb.Append(input[i + 1][j + 1]);
                            sb.Append(input[i + 2][j + 2]);
                            sb.Append(input[i + 3][j + 3]);

                            if (potentials.Contains(sb.ToString()))
                            {
                                count++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Part I: "+count);
        }

        private static void Part2(string[] input)
        {
            
            List<String> potentials = new List<String>();
            potentials.Add("MAS");
            potentials.Add("SAM");
            var height = input.Length;
            var width = input[0].Length;
            var count = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j <= (width - 3) && i <= (height - 3)) // enough clearence to go right and down
                    {
                        StringBuilder sb = new StringBuilder();
                        StringBuilder sb2 = new StringBuilder();
                        
                        //Left to right diagonally
                        sb.Append(input[i][j]);
                        sb.Append(input[i+1][j+1]);
                        sb.Append(input[i+2][j+2]);
                        
                        //Right to left diagonally
                        sb2.Append(input[i][j+2]);
                        sb2.Append(input[i+1][j+1]);
                        sb2.Append(input[i+2][j]);

                        if (potentials.Contains(sb.ToString()) && potentials.Contains(sb2.ToString()))
                        {
                            count++;
                        }
                        
                    }
                }
            }

            Console.WriteLine("Part II: "+count);
        }

        
    }
}