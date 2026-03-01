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