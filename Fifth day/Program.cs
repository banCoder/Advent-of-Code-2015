using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace Fifth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines(@"D:\Documents\day5instructions.txt");
            Console.WriteLine(niceString(instructions));
            Console.WriteLine(niceStringLambda(instructions));
            Console.WriteLine(actualNiceString(instructions));
            Console.WriteLine(actualNiceStringLambda(instructions));
            Console.ReadLine();
        }
        private static int niceString(string[] input)
        {
            int nice = 0;
            foreach (string line in input)
            {
                if (line.Where(c => c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u').Count() < 3)
                    continue;
                if (line.Where(c => line.Contains($"{c}{c}")).Count() < 1)
                    continue;
                if (line.Contains("ab") || line.Contains("cd") || line.Contains("pq") || line.Contains("xy"))
                    continue;
                nice++;
            }
            return nice;
        }
        private static int niceStringLambda(string[] input)
        {
            List<string> stringList = new List<string>();
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            string[] prohibited = new string[] { "ab", "cd", "pq", "xy" };
            foreach (string line in input)
            {
                // .Count gets a number of times line contains any of the vowels
                int vowelCount = line.Where(c => vowels.Contains(c)).Count();
                if (vowelCount < 3)
                {
                    continue;
                }
                // line gets added to the list if it contains 2 same characters 1 after another
                //foreach (char c in line.Where(c => line.Contains($"{c}{c}")))
                //{
                //    stringList.Add(line);
                //    break;
                //}
                // another way would be to get an index of the same character 1 slot ahead
                bool gotAdded = false;
                for (int i = 0; i < line.Length - 1; i++)
                {
                    if (line[i] == line[i + 1])
                    {
                        gotAdded = true;
                        stringList.Add(line);
                        break;
                    }
                }
                if (!gotAdded)
                {
                    continue;
                }
                // remove any occurances of strings containing prohibited ones
                foreach (string nope in prohibited)
                {
                    if (line.Contains(nope))
                    {
                        stringList.Remove(line);
                        break;
                    }
                }
            }
            return stringList.Count();
        }
        private static int actualNiceString(string[] input)
        {
            int nice = 0;
            foreach (string line in input)
            {
                // contains a pair of letters that appear at least twice but don't overlap
                bool containsAPair = false;
                bool overlaps = false;
                List<string> pairList = new List<string>();
                for (int i = 0; i < line.Length - 1; i++)
                {
                    string pair = line.Substring(i, 2);
                    if (pairList.Contains(pair))
                    {
                        containsAPair = true;
                        // compare the indexes i, i + 1
                        int index = pairList.IndexOf(pair);
                        if (index + 1 == i)
                        {
                            overlaps = true;
                        }
                    }
                    pairList.Add(pair);
                }
                bool containsRepeatedletter = false;
                for (int i = 0; i < line.Length - 2; i++)
                {
                    string sub = line.Substring(i, 3);
                    if (sub[0] == sub[2])
                    {
                        containsRepeatedletter = true;
                    }
                }
                if (containsAPair && containsRepeatedletter && !overlaps)
                {
                    nice++;
                }
            }
            return nice;
        }
        private static int actualNiceStringLambda(string[] input)
        {
            List<string> niceStrings = new List<string>();
            foreach (string line in input)
            {
                bool lol = false;
                for (int i = 0; i < line.Length - 1; i++)
                {
                    string pair = line.Substring(i, 2);
                    // index of can search for a string starting at specified location
                    // if it's not found it simply returns -1 instead of found position
                    if (line.IndexOf(pair, i + 2) != -1)
                    {
                        lol = true;
                        break;
                    }
                }
                // to keep a nice flow
                if (!lol)
                {
                    continue;
                }
                for (int j = 0; j < line.Length - 2; j++)
                {
                    // simply compare current character with a character 2 spots ahead
                    if (line[j] == line[j + 2])
                    {
                        niceStrings.Add(line);
                        break;
                    }
                }
            }
            return niceStrings.Count();
        }
        
    }
}
