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

    public void TakeDamage(int damage)
    {
        CurrentHitPoints -= damage;
    }
}