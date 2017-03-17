using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifteenth_day
{
    class Ingredient
    {
        public int Capacity { get; set; }
        public int Durability { get; set; }
        public int Flavour { get; set; }
        public int Texture { get; set; }
        public int Calories { get; set; }
        public string Name { get; set; }
        public Ingredient(string name, int capacity, int durability, int flavour, int texture, int calories)
        {
            Name = name;
            Capacity = capacity;
            Durability = durability;
            Flavour = flavour;
            Texture = texture;
            Calories = calories;
        }
    }
}
