using System.Xml.Serialization;

public class Monster
{
    public int ID { get; }
    public string Name { get; }
    public int MaximumDamage { get; }
    public int RewardExperiencePoints { get; }
    public int RewardGold { get; }

    public int CurrentHitPoints { get; set; }

    public void TakeDamage(int damage)
    {
        CurrentHitPoints -= damage;
    }

    public Monster(int id, string name, int maximumDamage, int rewardExperiencePoints, int rewardGold)
    {
        ID = id;
        Name = name;
        MaximumDamage = maximumDamage;
        RewardExperiencePoints = rewardExperiencePoints;
        RewardGold = rewardGold;
        CurrentHitPoints = 10;
    }
}