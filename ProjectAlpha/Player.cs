public class Player
{
    public string Name { get; set; }

    public int CurrentHitPoints { get; set; }

    public int MaximumHitPoints { get; set; }

    public Weapon CurrentWeapon { get; set; }

    public Location CurrentLocation { get; set; }

    public string username { get; set; }

    public Player(int maximumHitPoints, Weapon currentWeapon, Location currentLocation, string name)
    {
        Name = name;
        MaximumHitPoints = maximumHitPoints;
        CurrentHitPoints = maximumHitPoints;
        CurrentWeapon = currentWeapon;
        CurrentLocation = currentLocation;
    }
}