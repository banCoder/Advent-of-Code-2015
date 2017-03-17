using System;
using System.IO;

namespace Eight_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines(@"D:\Documents\day8instructions.txt");
            numberOfCharacters(instructions);
            encodedCharacters(instructions);
            Console.ReadLine();
        }
        private static void numberOfCharacters(string[] input)
        {
            int codeNum = 0;
            int count = 0;
            foreach (string line in input)
            {
                count += 2; // ignore the outer quotes
                codeNum += line.Length;
                for (int i = 1; i < line.Length - 1; i++) // igonres " " around the string
                {
                    if (line[i] == '\\')
                    {
                        if (line[i + 1] == '\\' || line[i + 1] == '\"')
                        {
                            count++;
                            i++;
                        }
                        else if (line[i + 1] == 'x')
                        {
                            count += 3;
                            i += 3;
                        }
                    }
                }
            }
            Console.WriteLine($"In code: {codeNum}\tEscaped: {count}");
        }
        private static void encodedCharacters(string[] input)
        {
            int codeNum = 0;
            int count = 0;
            foreach (string line in input)
            {
                codeNum += line.Length;
                count += 4; // each outter " turns into \" plus another " gets added
                for (int i = 1; i < line.Length - 1; i++) // igonres first and last character
                {
                    if (line[i] == '\\' || line[i] == '\"')
                    {
                        count += 1;
                    }
                }
            }
            Console.WriteLine($"In code: {codeNum}\tEscaped: {count}");
        }
    }
}
