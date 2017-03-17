using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace Second_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines(@"D:\Documents\day2instructions.txt");

            string s1 = "test";
            string s2 = "test";
            string s3 = "test1".Substring(0, 4);
            object s4 = s3;
            Console.WriteLine("{0} {1} {2}", ReferenceEquals(s1, s2), s1 == s2, s1.Equals(s2));
            Console.WriteLine("{0} {1} {2}", ReferenceEquals(s1, s3), s1 == s3, s1.Equals(s3));
            Console.WriteLine("{0} {1} {2}", ReferenceEquals(s1, s4), s1 == s4, s1.Equals(s4));

            Console.WriteLine(calculateWrappingPaper(instructions));
            Console.WriteLine(calculateRibbonLength(instructions));
            Console.ReadLine();
        }
        /// <summary>
        /// Calculates area of needed wrapping paper based on instructions of format LxWxH
        /// </summary>
        /// <param name="input">Instructions</param>
        /// <returns></returns>
        private static int calculateWrappingPaper(string[] input)
        {
            int sum = 0;
            int smallestArea = 0;
            foreach (string line in input)
            {
                int length = int.Parse(Regex.Split(line, "x")[0]);
                int width = int.Parse(Regex.Split(line, "x")[1]);
                int height = int.Parse(Regex.Split(line, "x")[2]);
                
                int lengthWidth = length * width;
                int widthHeight = width * height;
                int heightLength = height * length;
                
                int[] small = new int[] { lengthWidth, widthHeight, heightLength };

                smallestArea = small.Min();
                sum += (2 * lengthWidth) + (2 * widthHeight) + (2 * heightLength) + smallestArea;
            }
            return sum;
        }
        private static int calculateRibbonLength(string[] input)
        {
            int sum = 0;
            foreach (string line in input)
            {
                string[] dimenstions = Regex.Split(line, "x");

                int length = int.Parse(dimenstions[0]);
                int width = int.Parse(dimenstions[1]);
                int height = int.Parse(dimenstions[2]);

                int lengthWidth = length * width;
                int widthHeight = width * height;
                int heightLength = height * length;
                
                sum += length * width * height;

                int[] dim = new int[] { length, width, height };
                Array.Sort(dim);
                int smallest = dim[0];
                int secondSmallest = dim[1];

                sum += (2 * smallest) + (2 * secondSmallest);
            }
            return sum;
        }
    }
}
