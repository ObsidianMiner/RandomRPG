using RandomRPG.Campaigns;

namespace RandomRPG.Entities
{
    public class Kaleidoscope : Enemy
    {
        int turnNum = 0;
        int lensOptionIndex = 0;
        public Kaleidoscope(float hp, string name, float dmg) : base(hp, name, dmg)
        {
            Messages.ColoredWriteLine("Behold Kaleidoscope, the absorber of light, and your holy end.", ConsoleColor.Yellow);
            Messages.ColoredWriteLine("✨ enemies will disappear after dealing damage", ConsoleColor.Yellow);
            DarkContent.lampOil += 100;
            stunImmune = true;
        }
        void Summon(TemporaryEnemy summon)
        {
            Messages.ColoredWriteLine($"The Kaleidoscope rotates to reveal {summon.name}.", summon.nameColor);
            summon.OnSpawn();
            summon.stuned = true;
            RPG.enemies.Add(summon);
            summon.PrintStatus();
        }
        public override void DoTurn()
        {
            base.DoTurn();
            SpawnEnemies();
            if (turnNum > 0) TurnEvents();

            turnNum++;
        }
        public void SpawnEnemies()
        {
            switch (turnNum % 3)
            {
                case 0:
                    nameColor = ConsoleColor.Red;
                    Summon(new TemporaryEnemy(37f, "Queen Bee", 21f, ConsoleColor.Yellow));
                    Summon(new TemporaryEnemy(15f, "Plantera", 22f, ConsoleColor.Green));
                    Summon(new TemporaryEnemy(21f, "Moon Lord", 30f, ConsoleColor.Cyan));
                    Summon(new TemporaryEnemy(13f, "Duke Fishron", 8f, ConsoleColor.Blue));
                    break;
                case 1:
                    nameColor = ConsoleColor.Magenta;
                    Summon(new TemporaryEnemy(18f, "Crimson Mesenger", 28f, ConsoleColor.Red));
                    Summon(new TemporaryEnemy(6f, "Burning Trunker", 32f, ConsoleColor.Yellow));
                    break;
                case 2:
                    nameColor = ConsoleColor.Green;
                    Summon(new TemporaryEnemy(13f, "Wandering Vangerant", 28f, ConsoleColor.Cyan));
                    Summon(new TemporaryEnemy(40f, "Overloading Worm", 40f, ConsoleColor.Blue));
                    Summon(new TemporaryEnemy(21f, "Void Devistator", 40f, ConsoleColor.Magenta));
                    break;
                default:
                    break;
            }
        }
        public void TurnEvents()
        {
            string[] lensOptions = ["Kick the red lens", "Shoot the yellow lens", "Scream at the blue lens", "C4 The Heart"];
            string currentLensOption = lensOptions[lensOptionIndex];
            string pickedOption = "";
            string[] possibleOptions = [];
            switch (turnNum - 1)
            {
                case 0:
                    Messages.ColoredWriteLine("The light shifts into a few paths ahead of you. Use them quickly to gain an advantage in the fight!", ConsoleColor.Yellow);
                    possibleOptions = [currentLensOption,
                        "Look through the infinite mirror maze, for a lost companion."];
                    break;
                case 1:
                    Messages.ColoredWriteLine("Shards of glass enclose you likes walls and try to entrap and entrance you in the infinity.", ConsoleColor.Yellow);
                    Messages.ColoredWriteLine("Void devistarors and overloading worms have teleported in.", ConsoleColor.Yellow);
                    possibleOptions = [currentLensOption,
                        RPG.heros.Any(h => h.name == "Apotheosis") ? "Cast the light of apotheosis to shatter the walls." : "Ride a chain of glass outside the walls and blast the overloading worm down the mouth.",
                        RPG.heros.Any(h => h.name == "Loader" || h.name == "True Form Loader") ? "Do the ususal 200mph loader swing." : ""];
                    break;
                default:
                    switch (turnNum % 3)
                    {
                        case 0:
                            possibleOptions = [currentLensOption,
                                "Look through the infinite mirror maze, for a lost companion.",
                                "Attatch someone to the duplicating glass.",
                                "Bask in the light (All heal 15)"];
                            break;
                        case 1:
                            possibleOptions = [currentLensOption,
                                "Bask in the light (All heal 15)"];
                            break;
                        case 2:
                            Messages.ColoredWriteLine("You see an endless expanse of light on the horizon", ConsoleColor.Yellow);
                            possibleOptions = [currentLensOption, "Bask in the light (All heal 15)"];
                            break;
                    }
                    break;
            }
            int optionIndex = Input.Options("What do you do?", possibleOptions);
            pickedOption = possibleOptions[optionIndex];

            if (lensOptions.Contains(pickedOption)) ShatterOption(pickedOption);

            if (pickedOption == "Look through the infinite mirror maze, for a lost companion.")
            {
                Hero[] everySinglePreviousHero = StartingContent.possibleHeros.Concat(TechContent.possibleHeros).ToArray();
                //These upgrades are technicly destructive to the hero pool, but this is the last fight so it does not matter.
                for (int i = 0; i < everySinglePreviousHero.Length; i++)
                {
                    if (everySinglePreviousHero[i].upgrade != null) everySinglePreviousHero[i].Upgrade();
                    if (everySinglePreviousHero[i].upgrade != null) everySinglePreviousHero[i].Upgrade();
                }
                for (int i = 0; i < everySinglePreviousHero.Length; i++)
                {
                    everySinglePreviousHero[i].PrintHeroDescription();
                }
                int selectedHero = Input.Options("Bring back any hero from a previous campaign...", everySinglePreviousHero.Select(h => h.name).ToArray());
                RPG.RecruitHero(everySinglePreviousHero[selectedHero].Clone());
            }

            if (pickedOption == "Attatch someone to the duplicating glass.")
            {
                Console.WriteLine("Select a hero to be duplicated");
                int heroSelectedToDuplicate = Input.HeroOption(RPG.heros);
                RPG.heros.Add(RPG.heros[heroSelectedToDuplicate].Clone());
                RPG.heros[RPG.heros.Count - 1].name += " 2";
                RPG.heros[RPG.heros.Count - 1].OnSpawn();
            }

            if (pickedOption == "Cast the light of apotheosis to shatter the walls.")
            {
                for (int i = 0; i < RPG.enemies.Count; i++)
                {
                    RPG.enemies[i].TakeDamage(40f, true);
                }
                Battlefield.KillDeadEnemies();
            }

            if (pickedOption == "Do the ususal 200mph loader swing.")
            {
                for (int i = 0; i < RPG.enemies.Count; i++)
                {
                    RPG.enemies[i].TakeDamage(40f, true);
                }
                Battlefield.KillDeadEnemies();
            }

            if (pickedOption == "Ride a chain of glass outside the walls and blast the overloading worm down the mouth.")
            {
                RPG.enemies.First(e => e.name == "Overloading Worm").TakeDamage(50f);
            }

            if (pickedOption == "Bask in the light (All heal 15)")
            {
                for (int i = 0; i < RPG.heros.Count; i++)
                {
                    RPG.heros[i].Heal(15);
                }
            }
        }
        void ShatterOption(string pickedOption)
        {
            if (pickedOption == "C4 The Heart")
            {
                Messages.ColoredWriteLine("You shatter the heart, into bolts and pieces...", ConsoleColor.Cyan);
                Thread.Sleep(3000);
                Console.WriteLine("Crash!");
                Thread.Sleep(800);
                Console.WriteLine("Crack!");
                Thread.Sleep(800);
                Console.WriteLine("KRRASH!");
                Thread.Sleep(800);
                name = "Shattered Kaleidoscope";
                damageTakenMult.AddMult("Shattered", 3f);
            }
            else
            {
                ConsoleColor textColor = ConsoleColor.Red;
                if (pickedOption.Contains("yellow")) textColor = ConsoleColor.Yellow;
                else if (pickedOption.Contains("blue")) textColor = ConsoleColor.Blue;
                Messages.ColoredWriteLine("It shatters to pieces", textColor);
                damageTakenMult.AddMult(pickedOption, 1.5f);
                lensOptionIndex++;
            }
        }
    }
}
