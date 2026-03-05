    class Program
    {
        static List<int> acceptedQuests = new List<int>();
        static List<int> completedQuests = new List<int>();
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

                CheckForMonster(player);

                CheckForQuestCompletion(player);


                ShowAvailableDirections(player);

                Console.Write("Enter direction (N/E/S/W) or Q to quit: ");
                string input = (Console.ReadLine() ?? "").Trim().ToUpper();

                if (input == "Q")
                    break;

                MovePlayer(player, input);
            }
        }
        static void CheckForQuest(Player player)
        {
            Quest quest = player.CurrentLocation.QuestAvailableHere;

            if (quest == null) return;
            if (acceptedQuests.Contains(quest.ID)) return;

            Console.WriteLine($"[QUEST] {quest.Name}");
            Console.WriteLine($"  {quest.Description}");
            Console.Write("Accept quest? (Y/N): ");
            string answer = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (answer == "Y")
            {
                acceptedQuests.Add(quest.ID);
                Console.WriteLine($"Quest accepted: {quest.Name}");
            }
        }
        static void CheckForQuestCompletion(Player player)
    {
        if (player.CurrentLocation.ID == World.LOCATION_ID_ALCHEMIST_HUT
            && acceptedQuests.Contains(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN)
            && !completedQuests.Contains(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN))
        {
            Console.WriteLine("The alchemist checks your work...");
            Console.WriteLine("[QUEST COMPLETE] Clear the alchemist's garden!");

            completedQuests.Add(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

            Console.WriteLine("[NEW] Head to the Farmhouse for your next quest!");
        }

        if (player.CurrentLocation.ID == World.LOCATION_ID_FARMHOUSE
            && acceptedQuests.Contains(World.QUEST_ID_CLEAR_FARMERS_FIELD)
            && !completedQuests.Contains(World.QUEST_ID_CLEAR_FARMERS_FIELD))
        {
            Console.WriteLine("The farmer thanks you for clearing his field!");
            Console.WriteLine("[QUEST COMPLETE] Clear the farmer's field!");

            completedQuests.Add(World.QUEST_ID_CLEAR_FARMERS_FIELD);

            Console.WriteLine("[NEW] Head to the Bridge for your next quest!");
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