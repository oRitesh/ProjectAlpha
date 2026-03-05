class Program
{
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

            ShowAvailableDirections(player);

            Console.Write("Enter direction (N/E/S/W) or Q to quit: ");
            string input = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (input == "Q")
                break;

            MovePlayer(player, input);
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