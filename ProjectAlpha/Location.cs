public class Location
{
    public int ID { get; }
    public string Name { get; }
    public string Description { get; }

    public Quest? QuestAvailableHere { get; set; }
    public Monster? MonsterLivingHere { get; set; }

    public Location? LocationToNorth { get; set; }
    public Location? LocationToEast { get; set; }
    public Location? LocationToSouth { get; set; }
    public Location? LocationToWest { get; set; }

    public Location(int id, string name, string description, Quest? questAvailableHere, Monster? monsterLivingHere)
    {
        ID = id;
        Name = name;
        Description = description;
        QuestAvailableHere = questAvailableHere;
        MonsterLivingHere = monsterLivingHere;
    }
}