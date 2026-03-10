public class Potion
{
    public int ID { get; }
    public string Name { get; }
    public string Type { get; }
    public int HealAmount { get; }
    public int StrengthBonus { get; }

    public Potion(int id, string name, int healAmount, int strengthBonus = 0, string type = "Potion")
    {
        ID = id;
        Name = name;
        HealAmount = healAmount;
        StrengthBonus = strengthBonus;
        Type = type;
    }

    public void Use(Player player)
    {
        if (HealAmount > 0)
        {
            player.CurrentHitPoints += HealAmount;
            if (player.CurrentHitPoints > player.MaximumHitPoints)
                player.CurrentHitPoints = player.MaximumHitPoints;
            Console.WriteLine($"\nYou used a {Name} and healed {HealAmount} HP!");
            Console.WriteLine($"Your current HP: {player.CurrentHitPoints}/{player.MaximumHitPoints}\n");
        }

        if (StrengthBonus > 0)
        {
            player.StrengthBonus += StrengthBonus;
            player.StrengthBonusTurnsLeft = 3;
            Console.WriteLine($"\nYou used a {Name} and gained {StrengthBonus} bonus damage for 3 turns!");
            Console.WriteLine($"You currently deal {player.CurrentWeapon.MaximumDamage + player.StrengthBonus} damage with your weapon.\n");
        }
    }
}