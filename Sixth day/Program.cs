using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Sixth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines(@"D:\Documents\day6instructions.txt");
            Light[,] lightGrid = new Light[1000, 1000];
            tamperWithLights(instructions, ref lightGrid);
            Console.ReadLine();
        }
        private static void tamperWithLights(string[] input, ref Light[,] lightGrid)
        {
            // Fill the array with default lights
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    lightGrid[i, j] = new Light();
                }
            }
            foreach (string line in input)
            {
                // skips letters and takes digits until another letter, then trims it
                string min = new string(line
                    .SkipWhile(c => !char.IsDigit(c))
                    .TakeWhile(c => !char.IsLetter(c))
                    .ToArray()).Trim();
                // skips letters, skips digits, skips letters and takes digits until another letter, then trims it
                string max = new string(line
                    .SkipWhile(c => !char.IsDigit(c))
                    .SkipWhile(c => !char.IsLetter(c))
                    .SkipWhile(c => !char.IsDigit(c))
                    .TakeWhile(c => !char.IsLetter(c))
                    .ToArray()).Trim();
                // translates captured strings into coordinates
                int lineXStart = int.Parse(Regex.Split(min, ",")[0]);
                int lineYStart = int.Parse(Regex.Split(min, ",")[1]);
                int lineXEnd = int.Parse(Regex.Split(max, ",")[0]);
                int lineYEnd = int.Parse(Regex.Split(max, ",")[1]);
                string instructions = new string(line.TakeWhile(c => !char.IsDigit(c)).ToArray()).Trim();
                turnLights(lineXStart, lineYStart, lineXEnd, lineYEnd, instructions, ref lightGrid);
            }
            int count = 0;
            int brightness = 0;
            foreach (Light light in lightGrid)
            {
                // increase count for every light that is lit up
                if (light.Lit)
                    count++;
                // increase brightness for each level
                brightness += light.Brightness;
            }
            // type out the results
            Console.WriteLine($"Lights that are lit up: {count}\nTotal brightness level: {brightness}");
        }
        private static void turnLights(int xStart, int yStart, int xEnd, int yEnd, string instructions, ref Light[,] lightGrid)
        {
            // all horizontal lights from xStart to xEnd
            for (int i = xStart; i <= xEnd; i++)
            {
                // all vertical lights for every horizontal line from yStart to yEnd
                for (int j = yStart; j <= yEnd; j++)
                {
                    switch (instructions)
                    {
                        case "turn on":
                            // turns light on
                            lightGrid[i, j].Lit = true;
                            // increases the brightness
                            lightGrid[i, j].Brightness++;
                            break;
                        case "turn off":
                            lightGrid[i, j].Lit = false;
                            if (lightGrid[i, j].Brightness > 0)
                                lightGrid[i, j].Brightness--;
                            break;
                        case "toggle":
                            lightGrid[i, j].Lit = lightGrid[i, j].Lit ? false : true;
                            lightGrid[i, j].Brightness += 2;
                            break;
                    }
                }
            }
        }
    }
    public class Light
    {
        public bool Lit;
        public int Brightness;
        public Light()
        {
            Lit = false;
            Brightness = 0;
        }
    }
}
