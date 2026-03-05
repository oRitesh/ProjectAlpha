public class Quest
{
    public int ID { get; }
    public string Name { get; }
    public string Description { get; }
    public List<string> Rewards { get; }
    public bool IsCompleted { get; set; }

    public Quest(int id, string name, string description, List<string> rewards)
    {
        ID = id;
        Name = name;
        Description = description;
        Rewards = rewards;
        IsCompleted = false;
    }
}