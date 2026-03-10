public static class BattleSystem
{
    public static bool StartBattle(Player player, Monster monster)
    {
        double PlayerHitChance = 0.8; // 80% chance to hit
        double MonsterHitChance = 0.7; // 70% chance to hit
        Console.WriteLine("You are battling a " + monster.Name);

        while (player.CurrentHitPoints > 0 && monster.CurrentHitPoints > 0)
        {
            Console.WriteLine("A - Attack");
            Console.WriteLine("F - Flee");
            if (player.Potions.Count > 0)
                Console.WriteLine("U - Use Potion");

            string choice = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (choice == "F")
            {
                ResetStrengthBonus(player);
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

                Console.Write("Kies een potion (of 0 om te annuleren): ");
                if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= player.Potions.Count)
                {
                    Potion chosen = player.Potions[index - 1];
                    chosen.Use(player);
                    player.Potions.RemoveAt(index - 1);
                }
                continue; // monster slaat niet terug na potion gebruik
            }

            // Player attacks
            bool isPlayerHit = new Random().NextDouble() <= PlayerHitChance;
            if (!isPlayerHit)
            {
                Console.WriteLine("Your attack missed!");
                Console.WriteLine("The " + monster.Name + " has " + monster.CurrentHitPoints + " HP left.");
                Console.WriteLine();
            }
            else
            {
                int totalDamage = player.CurrentWeapon.MaximumDamage + player.StrengthBonus;
                monster.TakeDamage(totalDamage);
                Console.WriteLine($"You hit the {monster.Name} for {totalDamage} damage.");
                Console.WriteLine("The " + monster.Name + " has " + monster.CurrentHitPoints + " HP left.");
                Console.WriteLine();
            }

            // Verlaag strength bonus teller na aanval
            if (player.StrengthBonusTurnsLeft > 0)
            {
                player.StrengthBonusTurnsLeft--;
                Console.WriteLine($"Strength bonus: {player.StrengthBonusTurnsLeft} turns left.");

                if (player.StrengthBonusTurnsLeft == 0)
                {
                    player.StrengthBonus = 0;
                    Console.WriteLine("Your strength bonus has worn off.");
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