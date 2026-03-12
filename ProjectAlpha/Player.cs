using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Xml.Serialization;

public class Player
{
    public string Name { get; set; }
    public int CurrentHitPoints { get; set; }
    public int MaximumHitPoints { get; set; }
    public Weapon CurrentWeapon { get; set; }
    public Location CurrentLocation { get; set; }
    public List<Potion> Potions { get; set; } = new List<Potion>();
    public List<Weapon> Weapons { get; set; } = new List<Weapon>();
    public int StrengthBonus { get; set; } = 0;
    public int StrengthBonusTurnsLeft { get; set; } = 0;

    public bool InventoryExit { get; set; } = false;
    public bool ReturnToGame { get; set; } = false;

    public void TakeDamage(int damage)
    {
        CurrentHitPoints -= damage;
        if (CurrentHitPoints < 0)
            CurrentHitPoints = 0;
    }

    public Player(int maximumHitPoints, Weapon currentWeapon, Location currentLocation, string name)
    {
        Name = name;
        MaximumHitPoints = maximumHitPoints;
        CurrentHitPoints = maximumHitPoints;
        CurrentWeapon = currentWeapon;
        CurrentLocation = currentLocation;
    }

    public void ShowInventory()
    {
        while (!InventoryExit)
        {
            Console.Clear();
            Console.WriteLine("\n=== Inventory ===");
            Console.WriteLine($"Current weapon: {CurrentWeapon.Name}");
            Console.WriteLine($"\nAll weapons:");
            foreach (Weapon weapon in Weapons)
            {
                Console.WriteLine($" - {weapon.Name}");
            }

            if (Potions.Count == 0)
            {
                Console.WriteLine("\nPotions: none");
            }
            else
            {
                Console.WriteLine("\nPotions:");
                foreach (Potion potion in Potions)
                    Console.WriteLine($"  - {potion.Name} ({(potion.HealAmount > 0 ? $"+{potion.HealAmount} HP" : $"+{potion.StrengthBonus} STR")})");
            }
            Console.WriteLine($"\nIf you wish to exit the inventory, enter 'E'");
            if ((Console.ReadLine() ?? "").ToLower() == "e")
            {
                InventoryExit = true;
                Console.Clear();
            }
        }
    }

    public void SelectOutsideBattle()
    {
        while (!ReturnToGame)
        {
            Console.Clear();
            Console.WriteLine("\n=== Select active weapon ===");
            Console.WriteLine($"Current weapon: {CurrentWeapon.Name}");
            Console.WriteLine($"All weapons:");
            foreach (Weapon weapon in Weapons)
            {
                Console.WriteLine($"{weapon.ID} - {weapon.Name}");
            }

            Console.Write($"\nEnter 'E' if you want to continue slaying evil enemies with you mighty weapon\n-> ");

            string choice = (Console.ReadLine() ?? "").Trim();
            if (choice.ToLower() == "e")
            {
                ReturnToGame = true;
                Console.Clear();
                return;
            }

            int choiceInt;

            if (!int.TryParse(choice, out choiceInt))
            {
                Console.WriteLine("Invalid choice");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
            }

            else if (choiceInt != 1 && choiceInt != 2 && Weapons.Count == 2)
            {
                Console.WriteLine("Invalid choice, choice is not in the list!");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
            }

            else if (choiceInt != 1 && Weapons.Count == 1)
            {
                Console.WriteLine("Invalid choice, choice is not in the list!");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
            }

            else if (CurrentWeapon.ID == choiceInt)
            {
                Console.WriteLine("Invalid choice, weapon is already active!");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
            }

            else
            {
                CurrentWeapon = Weapons.FirstOrDefault(weapon => weapon.ID == choiceInt);
                Console.WriteLine($"You have selected the {CurrentWeapon.Name}");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
            }
        }
    }
}