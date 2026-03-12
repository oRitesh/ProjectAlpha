public static class BattleSystem
{
    public static bool StartBattle(Player player, Monster monster)
    {
        double PlayerHitChance = 0.8; // 80% chance to hit
        double MonsterHitChance = 0.7; // 70% chance to hit

        while (player.CurrentHitPoints > 0 && monster.CurrentHitPoints > 0)
        {
            Console.WriteLine($"\nYou are battling a {monster.Name}.");
            Console.WriteLine("A - Attack");
            if (player.Potions.Count <= 0)
            {
                Console.Write("F - Flee\n-> ");
            }
            else
            {
                Console.WriteLine("F - Flee ");
                Console.Write("U - Use Potion\n-> ");
            }

            string choice = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (choice == "F")
            {
                ResetStrengthBonus(player);
                Console.WriteLine("You fled from the battle.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return false;
            }

            if (choice == "U" && player.Potions.Count > 0)
            {
                for (int i = 0; i < player.Potions.Count; i++)
                {
                    Potion p = player.Potions[i];
                    string effect = p.HealAmount > 0 ? $"+{p.HealAmount} HP" : $"+{p.StrengthBonus} STR for 3 turns";
                    Console.WriteLine($"{i + 1} - {p.Name} ({effect})");
                }

                Console.Write("Choose a potion (or 0 to cancel):\n");
                if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= player.Potions.Count)
                {
                    Potion chosen = player.Potions[index - 1];
                    chosen.Use(player);
                    player.Potions.RemoveAt(index - 1);
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                continue;
            }

            if (choice != "A")
            {
                Console.WriteLine("Invalid choice. Try again.");
                Console.ReadKey();
                Console.Clear();
                continue;
            }

            // Player attacks
            bool isPlayerHit = new Random().NextDouble() <= PlayerHitChance;
            if (!isPlayerHit)
            {
                Console.Clear();
                Console.WriteLine("Your attack missed!");
                Console.WriteLine("The " + monster.Name + " has " + monster.CurrentHitPoints + " HP left.");
                Console.WriteLine();
            }
            else
            {
                int totalDamage = player.CurrentWeapon.MaximumDamage + player.StrengthBonus;
                monster.TakeDamage(totalDamage);
                Console.Clear();
                Console.WriteLine($"You hit the {monster.Name} for {totalDamage} damage.");
                Console.WriteLine("The " + monster.Name + " has " + monster.CurrentHitPoints + " HP left.");
                Console.WriteLine();
            }

            if (player.StrengthBonusTurnsLeft > 0)
            {
                player.StrengthBonusTurnsLeft--;
                Console.WriteLine($"\nStrength bonus: {player.StrengthBonusTurnsLeft} turns left.");

                if (player.StrengthBonusTurnsLeft == 0)
                {
                    player.StrengthBonus = 0;
                    Console.WriteLine("\nYour strength bonus has worn off.");
                }
            }

            // Monster attacks
            if (monster.CurrentHitPoints > 0)
            {
                bool isMonsterHit = new Random().NextDouble() <= MonsterHitChance;
                if (!isMonsterHit)
                {
                    Console.WriteLine("The " + monster.Name + "'s attack missed!");
                    Console.WriteLine("You have " + player.CurrentHitPoints + " HP left.");
                    Console.WriteLine();
                }
                else
                {
                    player.TakeDamage(monster.MaximumDamage);
                    Console.WriteLine("The " + monster.Name + " hit you for " + monster.MaximumDamage + " damage.");
                    Console.WriteLine("You have " + player.CurrentHitPoints + " HP left.");
                    Console.WriteLine();
                }
            }

            if (monster.CurrentHitPoints <= 0)
            {
                Console.WriteLine("You defeated the " + monster.Name + "!");
                ResetStrengthBonus(player);
                return true;
            }
            else if (player.CurrentHitPoints <= 0)
            {
                Console.WriteLine("You were defeated by the " + monster.Name + "...");
                ResetStrengthBonus(player);
                return false;
            }
        }
        ResetStrengthBonus(player);
        return false;
    }

    private static void ResetStrengthBonus(Player player)
    {
        player.StrengthBonus = 0;
        player.StrengthBonusTurnsLeft = 0;
    }
}