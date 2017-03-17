using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace Thirteenth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines(@"D:\Documents\day13instructions.txt");
            calculateSitting(instructions);
            Console.ReadLine();
        }
        private static void calculateSitting(string[] input)
        {
            // split each line with regex
            List<string> guests = new List<string>();
            foreach (string line in input)
            {
                string guest = Regex.Split(line, " ")[0];
                if (!guests.Contains(guest))
                {
                    guests.Add(guest);
                }
            }
            guests.Add("Urban");
            // make an int array
            // 1st dimention = first name (index into guests)
            // 2nd dimension = 2nd name (index into guests)
            // value = points
            int guestLength = guests.Count();
            int[,] namesAndPoints = new int[guestLength, guestLength];
            foreach (string line in input)
            {
                MatchCollection matches = Regex.Matches(line, @"[A-Z][a-z]+");
                string first = matches[0].Value;
                string second = matches[1].Value;
                int points = int.Parse(Regex.Match(line, @"\d+").Value);
                points = Regex.IsMatch(line, "lose") ? -points : points;
                // we have names, we need to find their positions in the list and add points
                int firstIndex = guests.IndexOf(first);
                int secondIndex = guests.IndexOf(second);
                namesAndPoints[firstIndex, secondIndex] = points;
            }
            // we need permutations, then calculate values for each permutation
            string perm = string.Join("", Enumerable.Range(0, guestLength).ToArray());
            string[] permutations = FindPermutations(perm);
            int comboPoints = 0;
            string optimal = "";
            foreach (string combo in permutations)
            {
                int currentPoints = 0;
                // for each combo we make a substring
                for (int i = 0; i < combo.Length - 1; i++)
                {
                    string calc = combo.Substring(i, 2);
                    // for each substring we calculate points
                    currentPoints += namesAndPoints[int.Parse(calc[0].ToString()), int.Parse(calc[1].ToString())];
                    currentPoints += namesAndPoints[int.Parse(calc[1].ToString()), int.Parse(calc[0].ToString())];
                }
                // it's a round table so people at the start sit next to people at the end
                currentPoints += namesAndPoints[int.Parse(combo[0].ToString()), int.Parse(combo[combo.Length - 1].ToString())];
                currentPoints += namesAndPoints[int.Parse(combo[combo.Length - 1].ToString()), int.Parse(combo[0].ToString())];
                if (currentPoints > comboPoints)
                {
                    comboPoints = currentPoints;
                    optimal = combo;
                }
            }
            // write the results
            Console.Write("Optimal sitting arrangement is: ");
            for (int i = 0; i < guestLength; i++)
            {
                Console.Write(guests[int.Parse(optimal[i].ToString())] + " ");
            }
            Console.Write("with the total of {0} points.", comboPoints);
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
