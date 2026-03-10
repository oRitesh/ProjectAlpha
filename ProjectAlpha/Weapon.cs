public class Weapon
{
    public int ID { get; }
    public string Type { get; }
    public string Name { get; }
    public int MaximumDamage { get; }

    public Weapon(int id, string name, int maximumDamage, string type = "Weapon")
    {
        ID = id;
        Name = name;
        MaximumDamage = maximumDamage;
        Type = type;
    }
}