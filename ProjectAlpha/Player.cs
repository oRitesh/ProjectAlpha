using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;

public static class Player
{
    public string Name { get; set; }

    public int CurrentHitPoints { get; set; }

    public int MaximumHitPoints { get; set; }

    public Weapon CurrentWeapon { get; set; }

    public Location CurrentLocation { get; set; }

    public Potion CurrentPotion { get; set; }

    public void TakeDamage(int damage)
    {
        CurrentHitPoints -= damage;

        if (CurrentHitPoints < 0)
        {
            CurrentHitPoints = 0;
        }
    }

    public Player(int maximumHitPoints, Weapon currentWeapon, Location currentLocation, Potion currentPotion, string name)
    {
        Name = name;
        MaximumHitPoints = maximumHitPoints;
        CurrentHitPoints = maximumHitPoints;
        CurrentWeapon = currentWeapon;
        CurrentLocation = currentLocation;
        CurrentPotion = currentPotion;
    }

    public static List<> Inventory()
    {

        return Inventory();
    }
}