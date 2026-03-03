using System;
public static class Weapon
{

    public int Id;
    public int MaximumDamage;
    public string Name;

    public Weapon(int id, int maximumDamage, string name)
    {
        this.Id = id;
        this.MaximumDamage = maximumDamage;
        this.Name = name;
    }

    public void Attack()
    {
        
    }

}
public class SuperAdventure
{
    public string CurrentMonster;
    public string ThePlayer;
    public SuperAdventure(string currentMonster, string thePlayer)
    {
        this.CurrentMonster = currentMonster;
        this.ThePlayer = thePlayer;
    }
}
public class Quest
{
    public string Description;
    public int Id;
    public string Name;
    public Quest(string description, int id, string name)
    {
        this.Description = description;
        this.Id = id;
        this.Name = name;
    }
}
public class Player
{
    public int CurrentHitPoints;
    public string CurrentLocation;
    public string CurrentWeapon;
    public int MaximumHitPoints;
    public string Name;
    public Player(int currentHitPoints, string currentLocation, string currentWeapon, int maximumHitPoints, string Name)
    {
        this.CurrentHitPoints = currentHitPoints;
        this.CurrentLocation = currentLocation;
        this.CurrentWeapon = currentWeapon;
        this.MaximumHitPoints = maximumHitPoints;
        this.Name = name;
    }
}
public class Monster
{
    public string Description;
    public int Id;
    public string Name;
    public Monster(string description, int id, string name)
    {
        this.Description = description;
        this.Id = id;
        this.Name = name;
    }
}
class Program
{
    static void Main()
    {
        
    }
}