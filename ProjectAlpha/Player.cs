using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;

public class Player
{
    public string Name { get; set; }
    public int CurrentHitPoints { get; set; }
    public int MaximumHitPoints { get; set; }
    public Weapon CurrentWeapon { get; set; }
    public Location CurrentLocation { get; set; }
    public List<Potion> Potions { get; set; } = new List<Potion>();
    public int StrengthBonus { get; set; } = 0;
    public int StrengthBonusTurnsLeft { get; set; } = 0;

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

    public static List<string> Inventory();
    public void ShowInventory()
    {
        Console.WriteLine("=== Inventory ===");
        Console.WriteLine($"Weapon: {CurrentWeapon.Name}");

        if (Potions.Count == 0)
        {
            Console.WriteLine("Potions: none");
        }
        else
        {
            Console.WriteLine("Potions:");
            foreach (Potion potion in Potions)
                Console.WriteLine($"  - {potion.Name} ({(potion.HealAmount > 0 ? $"+{potion.HealAmount} HP" : $"+{potion.StrengthBonus} STR")})");
        }
    }
}