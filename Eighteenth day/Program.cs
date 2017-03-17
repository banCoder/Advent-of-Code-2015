using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Eighteenth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines(@"D:\Documents\day18instructions.txt");
            lightsOn(instructions, 100);
            Console.ReadLine();
        }
        private static int[,] nextStep(int[,] input)
        {
            int[,] nextStep = new int[input.GetLength(0), input.GetLength(0)];
            for (int i = 1; i < input.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < input.GetLength(0) - 1; j++)
                {
                    int neighbours = 0;
                    if (input[i - 1, j - 1] == 2) // top left
                        neighbours++;
                    if (input[i - 1, j] == 2) // top
                        neighbours++;
                    if (input[i - 1, j + 1] == 2) // top right
                        neighbours++;
                    if (input[i, j + 1] == 2) // right
                        neighbours++;
                    if (input[i + 1, j + 1] == 2) // bottom right
                        neighbours++;
                    if (input[i + 1, j] == 2) // bottom
                        neighbours++;
                    if (input[i + 1, j - 1] == 2) // bottom left
                        neighbours++;
                    if (input[i, j - 1] == 2) // left
                        neighbours++;
                    // stuck lights
                    if ((i == 1 && j == 1) || (i == 1 && j == input.GetLength(0) - 2) ||
                        (i == input.GetLength(0) - 2 && j == 1) || (i == input.GetLength(0) - 2 && j == input.GetLength(0) - 2))
                    {
                        nextStep[i, j] = input[i, j];
                    }
                    // lights that are on
                    else if (input[i, j] == 2)
                    {
                        // turn off
                        if (neighbours < 2 || neighbours > 3)
                            nextStep[i, j] = 1;
                        // or leave on
                        else
                            nextStep[i, j] = input[i, j];
                    }
                    // turn on condition for lights that are off
                    else if (neighbours == 3)
                        nextStep[i, j] = 2;
                    // simply copy others
                    else
                        nextStep[i, j] = input[i, j];
                }
            }
            return nextStep;
        }
        private static int[,] lights(string[] input)
        {
            int[,] lights = new int[input.Length + 2, input.Length + 2];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if ((i == 0 && j == 0) || (i == 0 && j == input.Length - 1) ||
                        (i == input.Length - 1 && j == 0) || (i == input.Length - 1 && j == input.Length - 1))
                    {
                        lights[i + 1, j + 1] = 2;
                        continue;
                    }
                    lights[i + 1, j + 1] = input[i][j] == '.' ? 1 : 2;
                }
            }
            return lights;
        }
        private static void lightsOn(string[] input, int steps)
        {
            int[,] initial = lights(input);
            for (int i = 0; i < steps; i++)
            {
                int[,] nextStepArray = nextStep(initial);
                initial = nextStepArray;
                int count = 0;
                foreach (int ayy in initial)
                {
                    if (ayy == 2)
                        count++;
                }
                Console.WriteLine(count);
            }
        }
    }
}
