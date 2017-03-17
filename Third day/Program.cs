using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Third_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string instructions = File.ReadAllText(@"D:\Documents\day3instructions.txt");
            Console.WriteLine(visitedHousesWithRoboSanta(instructions));
            Console.ReadLine();
        }
        private static int visitedHouses(string input)
        {
            int gotPresent = 0;
            Coordinate currentPosition = new Coordinate(0, 0);
            List<Coordinate> coordinates = new List<Coordinate>() { currentPosition };
            foreach (char c in input)
            {
                currentPosition.Move(c);
                if (!coordinates.Contains(currentPosition))
                    coordinates.Add(currentPosition);
            }
            return gotPresent = coordinates.Count;
        }
        private static int visitedHousesWithRoboSanta(string input)
        {
            int gotPresent = 0;
            Coordinate currentSantaPosition = new Coordinate(0, 0);
            Coordinate currentRoboSantaPosition = new Coordinate(0, 0);
            List<Coordinate> coordinates = new List<Coordinate>() { currentSantaPosition };
            for (int i = 0; i < input.Length; i++)
            {
                if (i % 2 == 0)
                {
                    currentSantaPosition.Move(input[i]);
                    if (!coordinates.Contains(currentSantaPosition))
                        coordinates.Add(currentSantaPosition);
                }
                else
                {
                    currentRoboSantaPosition.Move(input[i]);
                    if (!coordinates.Contains(currentRoboSantaPosition))
                        coordinates.Add(currentRoboSantaPosition);
                }
            }
            return gotPresent = coordinates.Count;
        }
    }
}
