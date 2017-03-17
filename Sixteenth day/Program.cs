using System;
using System.IO;
using System.Linq;

namespace Sixteenth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines(@"D:\Documents\day16instructions.txt");
            getPoints(instructions);
            Console.ReadLine();
        }
        private static void getPoints(string[] input)
        {
            Sue[] allSues = new Sue[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                string[] words = input[i].Split(' ');
                int children = -1;
                int cats = -1;
                int goldfish = -1;
                int samoyeds = -1;
                int pomeranians = -1;
                int akitas = -1;
                int vizslas = -1;
                int trees = -1;
                int cars = -1;
                int perfumes = -1;
                for (int j = 0; j < words.Length; j++)
                {
                    switch (words[j])
                    {
                        case "children:":
                            children = int.Parse(new string(words[j + 1]
                                .TakeWhile(c => char.IsDigit(c)).ToArray()));
                            break;
                        case "cats:":
                            cats = int.Parse(new string(words[j + 1]
                                .TakeWhile(c => char.IsDigit(c)).ToArray()));
                            break;
                        case "samoyeds:":
                            samoyeds = int.Parse(new string(words[j + 1]
                                .TakeWhile(c => char.IsDigit(c)).ToArray()));
                            break;
                        case "pomeranians:":
                            pomeranians = int.Parse(new string(words[j + 1]
                                .TakeWhile(c => char.IsDigit(c)).ToArray()));
                            break;
                        case "akitas:":
                            akitas = int.Parse(new string(words[j + 1]
                                .TakeWhile(c => char.IsDigit(c)).ToArray()));
                            break;
                        case "vizslas:":
                            vizslas = int.Parse(new string(words[j + 1]
                                .TakeWhile(c => char.IsDigit(c)).ToArray()));
                            break;
                        case "goldfish:":
                            goldfish = int.Parse(new string(words[j + 1]
                                .TakeWhile(c => char.IsDigit(c)).ToArray()));
                            break;
                        case "trees:":
                            trees = int.Parse(new string(words[j + 1]
                                .TakeWhile(c => char.IsDigit(c)).ToArray()));
                            break;
                        case "cars:":
                            cars = int.Parse(new string(words[j + 1]
                                .TakeWhile(c => char.IsDigit(c)).ToArray()));
                            break;
                        case "perfumes:":
                            perfumes = int.Parse(new string(words[j + 1]
                                .TakeWhile(c => char.IsDigit(c)).ToArray()));
                            break;
                    }
                }
                allSues[i] = new Sue(children, cats, samoyeds, pomeranians, akitas, vizslas, goldfish, trees, cars, perfumes);
            }
            int topPoints = 0;
            int index = 0;
            for (int i = 0; i < allSues.Length; i++)
            {
                int points = allSues[i].Points(3, 7, 2, 3, 0, 0, 5, 3, 2, 1);
                if (points > topPoints)
                {
                    topPoints = points;
                    index = i;
                }
            }
            Console.WriteLine($"It was aunt Sue {index + 1} with {topPoints} points and {topPoints / 3 * 100}% probability!\n");
        }
    }
}
