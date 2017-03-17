using System;
using System.Linq;
using System.Text;

namespace Tenth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string instructions = "1113122113";
            int length = 0;
            lookAndSay(ref instructions, ref length, 50);
            Console.WriteLine("Length " + length);
            Console.ReadLine();
        }
        private static string lookAndSay(ref string input, ref int length, int times)
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < times; j++)
            {
                int i = 0;
                while (i < input.Length)
                {
                    char currentDigit = input[i];
                    int repeats = 0;
                    for (int k = i; k < input.Length; k++)
                    {
                        if (input[k] == currentDigit)
                            repeats++;
                        else
                            break;
                    }
                    //int repeats = input
                    //    .Skip(i)
                    //    .TakeWhile(c => c == currentDigit)
                    //    .Count();
                    sb.Append(repeats);
                    sb.Append(currentDigit);
                    i += repeats;
                }
                input = sb.ToString();
                sb.Clear();
            }
            length = input.Length;
            return input;
        }
    }
}
