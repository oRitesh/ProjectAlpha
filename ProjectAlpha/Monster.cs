public class Monster
{
    public int ID { get; }
    public string Name { get; }
    public int MaximumDamage { get; }
    public int CurrentHitPoints { get; set; }
    public int MaximumHitPoints { get; set; }

    public Monster(int id, string name, int maximumDamage, int currentHitPoints, int maximumHitpoints)
    {
        ID = id;
        Name = name;
        MaximumDamage = maximumDamage;
        CurrentHitPoints = currentHitPoints;
        MaximumHitPoints = maximumHitpoints;
    }

    public void TakeDamage(int damage)
    {
        CurrentHitPoints -= damage;

        if (CurrentHitPoints < 0)
        {
            CurrentHitPoints = 0;
        }
    }
}