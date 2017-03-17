using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fifteenth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines(@"D:\Documents\day15instructions.txt");
            cookieIngredients(instructions);
            Console.ReadLine();
        }
        private static void cookieIngredients(string[] input)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (string line in input)
            {
                string[] splitLine = line.Split(' ');
                ingredients.Add(new Ingredient(
                    new string(splitLine[0].TakeWhile(c => char.IsLetter(c)).ToArray()),
                    int.Parse(new string(splitLine[2].TakeWhile(c => char.IsDigit(c) || c == '-').ToArray())),
                    int.Parse(new string(splitLine[4].TakeWhile(c => char.IsDigit(c) || c == '-').ToArray())),
                    int.Parse(new string(splitLine[6].TakeWhile(c => char.IsDigit(c) || c == '-').ToArray())),
                    int.Parse(new string(splitLine[8].TakeWhile(c => char.IsDigit(c) || c == '-').ToArray())),
                    int.Parse(new string(splitLine[10].TakeWhile(c => char.IsDigit(c) || c == '-').ToArray()))
                    ));
            }
            Ingredient[] ingArray = ingredients.ToArray();
            //int totalScore = 0;
            //for (int i = 0; i < 101; i++)
            //{
            //    for (int j = 0; j < 101 - i; j++)
            //    {
            //        for (int k = 0; k < 101 - i - j; k++)
            //        {
            //            int capacity = ingredients[0].Capacity * i + ingredients[1].Capacity * j + ingredients[2].Capacity * k + ingredients[3].Capacity * (100 - i - j - k);
            //            int durability = ingredients[0].Durability * i + ingredients[1].Durability * j + ingredients[2].Durability * k + ingredients[3].Durability * (100 - i - j - k);
            //            int flavour = ingredients[0].Flavour * i + ingredients[1].Flavour * j + ingredients[2].Flavour * k + ingredients[3].Flavour * (100 - i - j - k);
            //            int texture = ingredients[0].Texture * i + ingredients[1].Texture * j + ingredients[2].Texture * k + ingredients[3].Texture * (100 - i - j - k);
            //            int calories = ingredients[0].Calories * i + ingredients[1].Calories * j + ingredients[2].Calories * k + ingredients[3].Calories * (100 - i - j - k);
            //            if (calories == 500)
            //            {
            //                int currentScore = 0;
            //                currentScore = capacity < 1 || durability < 1 || flavour < 1 || texture < 1 ? 0 : capacity * durability * flavour * texture;
            //                totalScore = currentScore > totalScore ? currentScore : totalScore;
            //            }
            //        }
            //    }
            //}
            //Console.WriteLine(totalScore);
            //Console.WriteLine(findMaxIngredients(ingArray, 100));
            Console.WriteLine(findMax(ingArray, 100));
            Console.WriteLine(findMax(ingArray, 100, true, 500));
            //int bestCapacity = 0;
            //for (int i = 0; i < 101; i++)
            //{
            //    int capacity = (i * ingredients[0].Capacity) + ((100 - i) * ingredients[1].Capacity);
            //    int durability = (i * ingredients[0].Durability) + ((100 - i) * ingredients[1].Durability);
            //    int flavour = (i * ingredients[0].Flavour) + ((100 - i) * ingredients[1].Flavour);
            //    int texture = (i * ingredients[0].Texture) + ((100 - i) * ingredients[1].Texture);
            //    int currentScore = 0;
            //    if (capacity < 1 || durability < 1 || flavour < 1 || texture < 1)
            //        currentScore = 0;
            //    else
            //        currentScore = capacity * durability * flavour * texture;
            //    if (currentScore > totalScore)
            //    {
            //        totalScore = currentScore;
            //        bestCapacity = i;
            //    }
            //}
            //Console.WriteLine($"Best balance is {bestCapacity}g of {ingredients[0].Name} and {100 - bestCapacity}g of {ingredients[1].Name}.");
        }
        private static int findMax(Ingredient[] ingredients, int needed, bool limitCalories = false, int calorieLimit = 0, int index = 0, int capacity = 0, int durability = 0, int flavour = 0, int texture = 0, int calories = 0)
        {
            if (needed <= 0)
                return capacity < 1 || durability < 1 || flavour < 1 || texture < 1 || (calories != calorieLimit && limitCalories) ? 0 : capacity * durability * flavour * texture;
            int totalScore = 0;
            if (index == ingredients.Length - 1)
            {
                return findMax(
                    ingredients,
                    0,
                    limitCalories,
                    calorieLimit,
                    index,
                    capacity + ingredients[index].Capacity * needed,
                    durability + ingredients[index].Durability * needed,
                    flavour + ingredients[index].Flavour * needed,
                    texture + ingredients[index].Texture * needed,
                    calories + ingredients[index].Calories * needed
                    );
            }
            for (int i = 0; i <= needed; i++)
            {
                int currentScore = findMax(
                    ingredients,
                    needed - i,
                    limitCalories,
                    calorieLimit,
                    index + 1,
                    capacity + ingredients[index].Capacity * i,
                    durability + ingredients[index].Durability * i,
                    flavour + ingredients[index].Flavour * i,
                    texture + ingredients[index].Texture * i,
                    calories + ingredients[index].Calories * i
                    );
                totalScore = Math.Max(currentScore, totalScore);
            }
            return totalScore;
        }
        private static int findMaxIngredients(Ingredient[] ingredients, int needed, int index = 0, int capacity = 0, int durability = 0, int flavour = 0, int texture = 0)
        {
            if (needed <= 0)
                return capacity < 1 || durability < 1 || flavour < 1 || texture < 1 ? 0 : capacity * durability * flavour * texture;
            int totalScore = 0;
            if (index == ingredients.Length - 1)
            {
                return findMaxIngredients(
                    ingredients,
                    0,
                    index + 1,
                    capacity ,
                    durability + ingredients[index].Durability * needed,
                    flavour + ingredients[index].Flavour * needed,
                    texture + ingredients[index].Texture * needed
                    );
            }
            for (int i = 0; i <= needed; i++)
            {
                int currentScore = findMaxIngredients(
                    ingredients,
                    needed - i,
                    index + 1,
                    capacity + ingredients[index].Capacity * i,
                    durability + ingredients[index].Durability * i,
                    flavour + ingredients[index].Flavour * i,
                    texture + ingredients[index].Texture * i);
                totalScore = Math.Max(currentScore, totalScore);
            }
            return totalScore;
        }
    }
}
