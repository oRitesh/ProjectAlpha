<<<<<<< HEAD
﻿
=======
﻿using System;

>>>>>>> 287e84da976e5b18a976777232acbd1069dede29
class Program
{
    static void Main()
    {
        Player player = new Player(100, "Town", "Sword", 100, "Hero");
        Monster monster = new Monster("Scary creature", 1, "Goblin");

        Console.WriteLine(player.Name);
        Console.WriteLine(monster.Name);
    }
}