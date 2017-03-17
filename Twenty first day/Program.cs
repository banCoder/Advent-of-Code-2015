using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Twenty_first_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines(@"D:\Documents\day21instructions.txt");
            bossFight(instructions);
            Console.ReadLine();
        }
        static Random _r = new Random();
        const int ITERATIONS = int.MaxValue;
        private static void bossFight(string[] input)
        {
            List<Equipment> equipment = new List<Equipment>();
            //equipment.Add(new Equipment("None", 0, 0, 0));
            //equipment.Add(new Equipment("NoDamage", 0, 0, 0));
            //equipment.Add(new Equipment("NoDamage2", 0, 0, 0));
            //for (int i = 0; i < input.Length; i++)
            //{
            //    MatchCollection words = Regex.Matches(input[i], @"\w+");
            //    int n;
            //    if (words.Count < 4 || !int.TryParse(words[1].Value, out n))
            //        continue;
            //    else if (words.Count == 4)
            //        equipment.Add(new Equipment(words[0].Value, int.Parse(words[1].Value), int.Parse(words[2].Value), int.Parse(words[3].Value)));
            //    else
            //        equipment.Add(new Equipment(words[0].Value + words[1].Value, int.Parse(words[2].Value), int.Parse(words[3].Value), int.Parse(words[4].Value)));
            //}
            //Equipment[] weapons = equipment.Where(e => e.Class == Class.Weapon).ToArray();
            //Equipment[] armors = equipment.Where(e => e.Class == Class.Armor).ToArray();
            //Equipment[] rings = equipment.Where(e => e.Class == Class.Ring).ToArray();
            //Player cheapestCombo = new Player();
            //Player mostExpensiveCombo = new Player();
            //for (int i = 0; i < weapons.Length; i++)
            //{
            //    for (int j = 0; j < armors.Length; j++)
            //    {
            //        for (int k = 0; k < rings.Length; k++)
            //        {
            //            for (int l = 0; l < rings.Length; l++)
            //            {
            //                if (k != l)
            //                {
            //                    Warrior playerr = new Warrior(weapons[i], armors[j], rings[k] + rings[l]);
            //                    Warrior bosss = new Warrior(9, 2, 103);
            //                    playerr.Fight(playerr, bosss, ref cheapestCombo, ref mostExpensiveCombo);
            //                }
            //            }
            //        }
            //    }
            //}
            //Console.WriteLine($"Cheapest combo takes costs {cheapestCombo.EquipmentCost} gold!");
            //Console.WriteLine($"Most expensive combo costs {mostExpensiveCombo.EquipmentCost} gold!");

            equipment.Add(new Equipment("MagicMissile", 53, 4, 0));
            equipment.Add(new Equipment("Drain", 73, 2, 0, 0, 0, 2));
            equipment.Add(new Equipment("Shield", 113, 0, 7, 6));
            equipment.Add(new Equipment("Poison", 173, 3, 0, 6));
            equipment.Add(new Equipment("Recharge", 229, 0, 0, 6, 101));
            Equipment[] spells = equipment.Where(e => e.Class == Class.Spell).ToArray();

            int leastManaSpent = int.MaxValue;
            float ranOutOfMana = 0;
            float died = 0;

            for (int i = 0; i < ITERATIONS; i++)
            {
                Warrior boss = new Warrior(10, 0, 71);
                Wizard player = new Wizard(50, 500);
                List<Equipment> activeSpells = new List<Equipment>();
                while (boss.Health > 0 && player.Health > 0 && player.Mana > 0)
                {
                    Console.WriteLine("Cast a spell: M, D, S, P, R");
                    List<Equipment> newActiveSpells = new List<Equipment>();
                    foreach (Equipment spell in activeSpells)
                    {
                        spell.Duration--;
                        if (spell.Duration > 0)
                        {
                            Console.WriteLine($"{spell.Name} {spell.Duration}");
                            newActiveSpells.Add(spell);
                            player.Mana += spell.Mana;
                            player.Armor += spell.Armor;
                            boss.Health -= spell.Damage;
                            player.Health += spell.Health;
                        }
                    }
                    activeSpells = newActiveSpells;
                    switch (Console.ReadLine().ToUpper())
                    {
                        case "M":
                            player.CastASpell(player, boss, equipment[0]);
                            activeSpells.Add(equipment[0]);
                            player.ManaSpent += equipment[0].Cost;
                            break;
                        case "D":
                            player.CastASpell(player, boss, equipment[1]);
                            player.ManaSpent += equipment[1].Cost;
                            break;
                        case "S":
                            if (!activeSpells.Contains(equipment[2]))
                            {
                                player.CastASpell(player, boss, equipment[2]);
                                activeSpells.Add(equipment[2]);
                                player.ManaSpent += equipment[2].Cost;
                            }
                            break;
                        case "P":
                            if (!activeSpells.Contains(equipment[3]))
                            {
                                player.CastASpell(player, boss, equipment[3]);
                                activeSpells.Add(equipment[3]);
                                player.ManaSpent += equipment[3].Cost;
                            }
                            break;
                        case "R":
                            if (!activeSpells.Contains(equipment[4]))
                            {
                                player.CastASpell(player, boss, equipment[4]);
                                activeSpells.Add(equipment[4]);
                                player.ManaSpent += equipment[4].Cost;
                            }
                            break;
                    }
                    Console.WriteLine($"Your health: {player.Health}, Your Mana: {player.Mana}\t\tBoss health: {boss.Health}");
                    if (boss.Health < 1 || player.Mana < 1)
                        break;
                    boss.Attack(boss, player);
                    newActiveSpells = new List<Equipment>();
                    player.Armor = 0;
                    foreach (Equipment spell in activeSpells)
                    {
                        spell.Duration--;
                        if (spell.Duration > 0)
                        {
                            Console.WriteLine($"{spell.Name} {spell.Duration}");
                            newActiveSpells.Add(spell);
                            player.Mana += spell.Mana;
                            player.Armor += spell.Armor;
                            boss.Health -= spell.Damage;
                            player.Health += spell.Health;
                        }
                    }
                    activeSpells = newActiveSpells;
                    player.Armor = 0;
                    Console.WriteLine($"Your health: {player.Health}, Your Mana: {player.Mana}\t\tBoss health: {boss.Health}");
                }
                string message = "";
                if (player.Mana <= 0)
                {
                    message = "You ran out of mana!";
                    ranOutOfMana++;
                }
                else
                {
                    if (player.Health > 0)
                    {
                        message = $"You won with {player.Health}hp and {player.ManaSpent} mana spent!";
                        leastManaSpent = Math.Min(player.ManaSpent, leastManaSpent);
                    }
                    else
                    {
                        message = $"You lost for {boss.Health}hp and {player.ManaSpent} mana spent!";
                        died++;
                    }
                }
                Console.WriteLine(message);
            }
            Console.WriteLine("Least mana spent was {0}\nYou died {1}% and ran out of mana {2}% of times!", leastManaSpent, (int)(ranOutOfMana / ITERATIONS * 100), (int)(died / ITERATIONS * 100));
        }
    }
    public class Equipment
    {
        public int Damage { get; set; }
        public int Cost { get; set; }
        public int Armor { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public Class Class { get; set; }
        public int Duration;
        public int Mana;
        public Equipment(string name, int cost, int damage, int armor, int duration = 0, int mana = 0, int health = 0)
        {
            Name = name;
            Cost = cost;
            Damage = damage;
            Armor = armor;
            Duration = duration;
            Mana = mana;
            Health = health;
            if (name.Contains("Damage") || name.Contains("Defense"))
                Class = Class.Ring;
            else if (name == "None")
                Class = Class.Armor;
            else if (Name == "MagicMissile" || Name == "Drain" || Name == "Shield" || Name == "Poison" || Name == "Recharge")
                Class = Class.Spell;
            else if (armor == 0)
                Class = Class.Weapon;
            else
                Class = Class.Armor;
        }
        public static Equipment operator +(Equipment e1, Equipment e2)
        {
            return new Equipment($"{e1.Name} {e2.Name}", e1.Cost + e2.Cost, e1.Damage + e2.Damage, e1.Armor + e2.Armor);
        }
    }
    public class Player
    {
        public int Armor { get; set; }
        public int Damage { get; set; }
        public int Health { get; set; }
        public int EquipmentCost;
    }
    public class Warrior : Player
    {
        public Warrior(int damage, int armor, int health)
        {
            Damage = damage;
            Armor = armor;
            Health = health;
        }
        public Warrior(Equipment weapon, Equipment armor, Equipment ring, int health = 100)
        {
            EquipmentCost = weapon.Cost + armor.Cost + ring.Cost;
            Armor = weapon.Armor + armor.Armor + ring.Armor;
            Damage = weapon.Damage + armor.Damage + ring.Damage;
            Health = health;
        }
        public void Fight(Player p1, Player p2, ref Player cheapestCombo, ref Player mostExpensiveCombo)
        {
            while (p1.Health > 0 && p2.Health > 0)
            {
                Attack(p1, p2);
                if (p2.Health <= 0) break;
                Attack(p2, p1);
            }
            cheapestCombo = p1.Health > 0 && (p1.EquipmentCost < cheapestCombo.EquipmentCost || cheapestCombo.EquipmentCost == 0) ? p1 : cheapestCombo;
            mostExpensiveCombo = p2.Health > 0 && (p1.EquipmentCost > mostExpensiveCombo.EquipmentCost || mostExpensiveCombo.EquipmentCost == 0) ? p1 : mostExpensiveCombo;
        }
        public void Attack(Player p1, Player p2)
        {
            p2.Health -= Math.Max(1, p1.Damage - p2.Armor);
        }
    }
    public class Wizard : Player
    {
        public int Mana { get; set; }
        public int ManaSpent;
        public Wizard(int health, int mana)
        {
            Health = health;
            Mana = mana;
        }
        public void CastASpell(Wizard p1, Warrior p2, Equipment spell)
        {
            p1.ManaSpent += spell.Cost;
            p1.Mana -= spell.Cost;
            p2.Health -= spell.Damage;
            p1.Health += spell.Health;
            p1.Armor += spell.Armor;
        }
    }
    public enum Class
    {
        Weapon,
        Armor,
        Ring,
        Spell
    }
}
