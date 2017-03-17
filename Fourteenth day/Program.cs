using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Fourteenth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines(@"D:\Documents\day14instructions.txt");
            raindeerDistance(instructions, 2503);
            Console.ReadLine();
        }
        private static void raindeerDistance(string[] input, int seconds)
        {
            List<string> raindeer = new List<string>();
            int[] speeds = new int[input.Length];
            int[] flyTime = new int[input.Length];
            int[] restTime = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                raindeer.Add(new string(input[i].TakeWhile(c => char.IsLetter(c)).ToArray()));
                speeds[i] = int.Parse(new string(input[i]
                    .SkipWhile(c => !char.IsDigit(c))
                    .TakeWhile(c => char.IsDigit(c))
                    .ToArray()));
                flyTime[i] = int.Parse(new string(Regex.Split(input[i], " ")[6]
                    .TakeWhile(c => char.IsDigit(c))
                    .ToArray()));
                restTime[i] = int.Parse(new string(Regex.Split(input[i], " ")[13]
                    .TakeWhile(c => char.IsDigit(c))
                    .ToArray()));
            }
            int[] distance = new int[input.Length];
            for (int i = 0; i < raindeer.Count(); i++)
            {
                int times = seconds / (flyTime[i] + restTime[i]);
                int left = Math.Min(seconds % (flyTime[i] + restTime[i]), flyTime[i]);
                distance[i] = ((times * flyTime[i]) + left) * speeds[i];
            }
            Console.WriteLine($"After {seconds}s, {raindeer[Array.IndexOf(distance, distance.Max())]} won by travelling {distance.Max()}km!");
            int[] points = new int[raindeer.Count()];
            for (int i = 1; i <= seconds; i++)
            {
                for (int j = 0; j < raindeer.Count(); j++)
                {
                    int times = i / (flyTime[j] + restTime[j]);
                    int left = Math.Min(i % (flyTime[j] + restTime[j]), flyTime[j]);
                    distance[j] = ((times * flyTime[j]) + left) * speeds[j];
                }
                int biggestDistance = distance.Max();
                for (int k = 0; k < distance.Length; k++)
                {
                    if (distance[k] == biggestDistance)
                        points[k]++;
                }
            }
            Console.WriteLine($"After {seconds}s, {raindeer[Array.IndexOf(points, points.Max())]} won by gaining {points.Max()} points!");
        }
    }
}
