using System.Configuration.Assemblies;

class Program
{
    static List<int> acceptedQuests = new List<int>();
    static List<int> completedQuests = new List<int>();
    static int ratsKilled = 0;
    static int snakesKilled = 0;
    static int spidersKilled = 0;
    static void Main()
    {
        Player player = new Player(
            100,
            World.WeaponByID(World.WEAPON_ID_RUSTY_SWORD),
            World.LocationByID(World.LOCATION_ID_HOME),
            "Hero"
        );
        player.CurrentLocation = World.LocationByID(World.LOCATION_ID_HOME);

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("You are at: " + player.CurrentLocation.Name);
            Console.WriteLine(player.CurrentLocation.Description);
            Console.WriteLine();

            CheckForQuest(player);
            CheckForHealing(player);
            CheckForMonster(player);
            CheckForQuestCompletion(player);
            ShowAvailableDirections(player);

            Console.Write("Choose an option:\n- Enter direction (N/E/S/W)>\n- I for inventory>\n- C to change weapon>\n- Q to quit> ");
            string input = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (input == "Q")
                break;

            if (input == "I")
            {
                player.ShowInventory();
                continue;
            }
            if (input == "C")
            {
                player.SelectOutsideBattle();
                continue;
            }

            MovePlayer(player, input);
        }
    }

    static void CheckForHealing(Player player)
    {
        if (player.CurrentLocation.ID == World.LOCATION_ID_HOME
            && player.CurrentHitPoints < player.MaximumHitPoints)
        {
            player.CurrentHitPoints = player.MaximumHitPoints;
            Console.WriteLine("You rest at home and recover your health.");
        }
    }
    static void CheckForQuest(Player player)
    {
        Quest quest = player.CurrentLocation.QuestAvailableHere;

        if (quest == null) return;

        if (player.CurrentLocation.ID == World.LOCATION_ID_GUARD_POST && completedQuests.Count != 2)
        {
            Console.WriteLine("Guard: 'Turn back at once, peasant! Unless thee hast proof of thy grit!'\n");
            Console.WriteLine("You returned to the town square.");
            player.CurrentLocation = World.LocationByID(World.LOCATION_ID_TOWN_SQUARE);
            return;
        }

        if (acceptedQuests.Contains(quest.ID)) return;

        Console.WriteLine($"[QUEST] {quest.Name}");
        Console.WriteLine($"        {quest.Description}");
        Console.Write("Accept quest? (Y/N): ");
        string answer = (Console.ReadLine() ?? "").Trim().ToUpper();

        if (answer == "Y")
        {
            acceptedQuests.Add(quest.ID);
            Console.WriteLine($"Quest accepted: {quest.Name}\n");
        }
        else if (answer == "N")
        {
            player.CurrentLocation = World.LocationByID(World.LOCATION_ID_TOWN_SQUARE);
            Console.WriteLine();
            Console.WriteLine("You declined the quest and returned to the town square.");
        }
    }

    static void CheckForMonster(Player player)
    {

        Monster monster = player.CurrentLocation.MonsterLivingHere;

        if (monster == null)
        {
            return;
        }

        bool battleResult = BattleSystem.StartBattle(player, monster);

        if (player.CurrentHitPoints <= 0)
        {
            Console.WriteLine("Game Over!");
            Environment.Exit(0);
        }

        if (!battleResult)
        {
            Console.WriteLine("You fled from the battle.");
            return;
        }

        if (monster.ID == World.MONSTER_ID_RAT) ratsKilled++;
        else if (monster.ID == World.MONSTER_ID_SNAKE) snakesKilled++;
        else if (monster.ID == World.MONSTER_ID_GIANT_SPIDER) spidersKilled++;

        if (monster.ID == World.MONSTER_ID_RAT)
            Console.WriteLine($"Rats defeated: {ratsKilled}/3");
        else if (monster.ID == World.MONSTER_ID_SNAKE)
            Console.WriteLine($"Snakes defeated: {snakesKilled}/3");
        else if (monster.ID == World.MONSTER_ID_GIANT_SPIDER)
            Console.WriteLine($"Spiders defeated: {spidersKilled}/3");

        int currentKills =
            monster.ID == World.MONSTER_ID_RAT ? ratsKilled :
            monster.ID == World.MONSTER_ID_SNAKE ? snakesKilled :
            spidersKilled;

        if (currentKills < 3)
        {
            monster.CurrentHitPoints = monster.MaximumDamage; // Reset monster's HP for next battle
            return;
        }

        else
        {
            Console.WriteLine("You have defeated all the monsters in this location!");
            player.CurrentLocation.MonsterLivingHere = null;
        }

    }
    static void CheckForQuestCompletion(Player player)
    {
        if (player.CurrentLocation.ID == World.LOCATION_ID_ALCHEMIST_HUT
            && acceptedQuests.Contains(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN)
            && !completedQuests.Contains(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN)
            && ratsKilled >= 3)
        {
            Console.WriteLine("The alchemist checks your work...");
            Console.WriteLine("[QUEST COMPLETE] Clear the alchemist's garden!");
            completedQuests.Add(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

            player.Potions.Add(World.PotionByID(World.POTION_ID_HEAL));
            player.Potions.Add(World.PotionByID(World.POTION_ID_STRENGTH));
            Console.WriteLine("\nReward: Heal Potion + Strength Potion added to your inventory!");
            Console.WriteLine("[NEW] Head to the Farmhouse for your next quest!\n");
        }

        if (player.CurrentLocation.ID == World.LOCATION_ID_FARMHOUSE
            && acceptedQuests.Contains(World.QUEST_ID_CLEAR_FARMERS_FIELD)
            && !completedQuests.Contains(World.QUEST_ID_CLEAR_FARMERS_FIELD)
            && snakesKilled >= 3)
        {
            Console.WriteLine("The farmer thanks you for clearing his field!");
            Console.WriteLine("[QUEST COMPLETE] Clear the farmer's field!");
            completedQuests.Add(World.QUEST_ID_CLEAR_FARMERS_FIELD);


            player.Potions.Add(World.PotionByID(World.POTION_ID_HEAL));
            player.Weapons.Add(World.WeaponByID(World.WEAPON_ID_FARMERS_PITCHFORK));
            Console.WriteLine("\nReward: Heal Potion added to your inventory! + Farmer's Pitchfork added to your weapons!");
            Console.WriteLine("[NEW] Head to the Bridge for your next quest!\n");
        }
    }

    static void ShowAvailableDirections(Player player)
    {
        Console.WriteLine("You can move:");

        if (player.CurrentLocation.LocationToNorth != null)
            Console.WriteLine("N - " + player.CurrentLocation.LocationToNorth.Name);

        if (player.CurrentLocation.LocationToEast != null)
            Console.WriteLine("E - " + player.CurrentLocation.LocationToEast.Name);

        if (player.CurrentLocation.LocationToSouth != null)
            Console.WriteLine("S - " + player.CurrentLocation.LocationToSouth.Name);

        if (player.CurrentLocation.LocationToWest != null)
            Console.WriteLine("W - " + player.CurrentLocation.LocationToWest.Name);

        Console.WriteLine();
    }

    static void MovePlayer(Player player, string direction)
    {
        Location newLocation = null;

        switch (direction)
        {
            case "N":
                newLocation = player.CurrentLocation.LocationToNorth;
                break;
            case "E":
                newLocation = player.CurrentLocation.LocationToEast;
                break;
            case "S":
                newLocation = player.CurrentLocation.LocationToSouth;
                break;
            case "W":
                newLocation = player.CurrentLocation.LocationToWest;
                break;
            default:
                Console.WriteLine("Invalid direction.");
                return;
        }

        if (newLocation == null)
        {
            Console.WriteLine("You cannot go that way.");
            return;
        }

        player.CurrentLocation = newLocation;
        Console.WriteLine("You moved to: " + player.CurrentLocation.Name);
    }
}