using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ninth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instrunctions = File.ReadAllLines(@"D:\Documents\day9instructions.txt");
            getNames(instrunctions);
            Console.ReadLine();
        }
        private static void getNames(string[] input)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string[,,] array = new string[input.Length, 2, 2];
            List<string> cities = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                MatchCollection matches = Regex.Matches(input[i], @"[A-Z]\w+");
                string departure = matches[0].Value;
                string arrival = matches[1].Value;
                string distance = Regex.Match(input[i], @"\d+").Value;
                if (!cities.Contains(departure))
                    cities.Add(departure);
                if (!cities.Contains(arrival))
                    cities.Add(arrival);
                array[i, 0, 0] = departure;
                array[i, 1, 0] = arrival;
                array[i, 0, 1] = distance;
            }
            string toPerm = string.Join("", Enumerable.Range(0, cities.Count()));
            string[] permutations = FindPermutations(toPerm);
            List<string> allPaths = new List<string>();
            foreach (string perm in permutations)
            {
                int j = 0;
                int dist = 0;
                StringBuilder sb = new StringBuilder();
                while (j < perm.Length - 1)
                {
                    string departure = cities[int.Parse(perm[j].ToString())];
                    string destination = cities[int.Parse(perm[j + 1].ToString())];
                    for (int i = 0; i < array.GetLength(0); i++)
                    {
                        if ((array[i, 0, 0] == departure && array[i, 1, 0] == destination) || 
                            (array[i, 0, 0] == destination && array[i, 1, 0] == departure))
                        {
                            dist += int.Parse(array[i, 0, 1]);
                            if (!(sb.ToString()).Contains(departure))
                            {
                                sb.Append(departure + " ");
                            }
                        }
                    }
                    sb.Append(destination + " ");
                    j++;
                }
                sb.Append(dist);
                allPaths.Add(sb.ToString());
            }
            int smallest = int.MaxValue;
            string smallestDistance = "";
            int biggest = 0;
            string biggesttDistance = "";
            for (int i = 0; i < allPaths.Count; i++)
            {
                int newDist = int.Parse(new string(allPaths[i].Where(c => char.IsDigit(c)).ToArray()));
                if (newDist < smallest)
                {
                    smallestDistance = allPaths[i];
                    smallest = newDist;
                }
                else if (newDist > biggest)
                {
                    biggesttDistance = allPaths[i];
                    biggest = newDist;
                }
            }
            Console.WriteLine(smallestDistance);
            Console.WriteLine(biggesttDistance);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
        public static string[] FindPermutations(string word)
        {
            if (word.Length == 2)
            {
                char[] c = word.ToCharArray();
                string s = new string(new[] { c[1], c[0] });
                return new[] { word, s };
            }
            List<string> result = new List<string>();

            string[] subsetPermutations = FindPermutations(word.Substring(1));
            char firstChar = word[0];
            foreach (string temp in subsetPermutations
                            .Select(s => firstChar.ToString(CultureInfo.InvariantCulture) + s))
            {
                result.Add(temp);
                char[] chars = temp.ToCharArray();
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    char t = chars[i];
                    chars[i] = chars[i + 1];
                    chars[i + 1] = t;
                    string s2 = new string(chars);
                    result.Add(s2);
                }
            }
            return result.ToArray();
        }
    }
}
