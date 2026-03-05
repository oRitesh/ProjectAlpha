public static class BattleSystem
{
    public static bool StartBattle(Player player, Monster monster)
    {
        double PlayerHitChance = 0.8; // 80% chance to hit
        double MonsterHitChance = 0.7; // 70% chance to hit
        Console.WriteLine("You are battling a " + monster.Name);

        while (player.CurrentHitPoints > 0 && monster.CurrentHitPoints > 0)
        {
            Console.WriteLine("1 - Attack");
            Console.WriteLine("2 - Flee");
            string choice = Console.ReadLine();

            if (choice == "2")
            {
                Console.WriteLine("You fled from the battle.");
                return false;
            }
            bool isPlayerHit = new Random().NextDouble() > PlayerHitChance;
            if (isPlayerHit)
            {
                Console.WriteLine("Your attack missed!");
                Console.WriteLine("The " + monster.Name + " has " + monster.CurrentHitPoints + " HP left.");
            }
            else
            {
                monster.TakeDamage(player.CurrentWeapon.MaximumDamage);
                Console.WriteLine("You hit the " + monster.Name + " for " + player.CurrentWeapon.MaximumDamage + " damage.");
                Console.WriteLine("The " + monster.Name + " has " + monster.CurrentHitPoints + " HP left.");
            }

            bool isMonsterHit = new Random().NextDouble() >= MonsterHitChance;
            if (isMonsterHit)
            {
                Console.WriteLine("The " + monster.Name + "'s attack missed!");
                Console.WriteLine("You have " + player.CurrentHitPoints + " HP left.");
            }
            else
            {
                player.TakeDamage(monster.MaximumDamage);
                Console.WriteLine("The " + monster.Name + " hit you for " + monster.MaximumDamage + " damage.");
                Console.WriteLine("You have " + player.CurrentHitPoints + " HP left.");
            }

            if (monster.CurrentHitPoints <= 0)
            {
                Console.WriteLine("You defeated the " + monster.Name + "!");
                return true;
            }
            else if (player.CurrentHitPoints <= 0)
            {
                Console.WriteLine("You were defeated by the " + monster.Name + "...");
                return false;
            }
        }
        return false;
    }
}