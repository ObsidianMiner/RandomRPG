using RandomRPG.Entities;

namespace RandomRPG.Campaigns
{
    public static class MagicEvents
    {
        public static void Events()
        {
            if (RPG.turnNum > 22)
            {
                return;
            }
            int rand = RandomUtil.Next(0, 11);
            if (rand < 7) Console.WriteLine();

            //If lamp oil is low, guarentee events that give lamp oil.
            if (DarkContent.lampOil <= 6)
            {
                if (rand < 4) rand = 0;
                else rand = 4;
            }

            switch (rand)
            {
                case 0:
                    SpiderEvent();
                    break;
                case 1:
                    BlackHoleEvent();
                    break;
                case 2:
                    CandyBowl();
                    break;
                case 3:
                    FruitPunchEvent();
                    break;
                case 4:
                    HouseInTheVoidEvent();
                    break;
                case 5:
                    VoidAnts();
                    break;
                case 6:
                    BuildABot();
                    break;
                case 7:
                    if (RPG.waveNum > 2) AntiSpaghettiSquadEvent();
                    break;
                default:
                    break;
            }
        }
        public static void VoidAnts()
        {
            Console.WriteLine("A giant ant hill is in front of you, and somehow is the only way forward.");
            Console.WriteLine("Inside is a 2 paths. One through a giant queen ant's room, and another a worker camp");
            int option = Input.Options("Where would you like to progress", ["Queen's Room", "Worker Camp"]);

            Battle.skipDefaultGenerating = true;
            if (option == 0)
            {
                RPG.enemies.Add(new Enemy(45f, "Queen Ant", 10f));
                RPG.enemies.Add(new Enemy(2f, "Ant Bro", 10f));
            }
            else
            {
                RPG.enemies.Add(new Enemy(2f, "Ant Bro", 15f));
                RPG.enemies.Add(new Enemy(2f, "Ant Bro", 15f));
                RPG.enemies.Add(new Enemy(2f, "Ant Bro", 15f));
                RPG.enemies.Add(new Enemy(2f, "Ant Bro", 15f));
                RPG.enemies.Add(new Enemy(2f, "Ant Bro", 15f));
                RPG.enemies.Add(new Enemy(2f, "Ant Bro", 15f));
            }
        }
        public static void FruitPunchEvent()
        {
            Console.WriteLine("You spot a lonley fruit punch sitting in the void.");
            Console.WriteLine("It has probably been there for days.");
            if (Input.GetUserYN("Drink it?"))
            {
                Console.WriteLine("Who should drink it?");
                int pickedHero = Input.HeroOption(RPG.heros.ToArray());
                if (RandomUtil.NextBool(1, 3))
                {
                    Console.WriteLine($"{RPG.heros[pickedHero].name} put their mouth to the fruit punch to drink it, but then it smacked them in the face.");
                    RPG.heros[pickedHero].TakeDamage(7);
                    Input.WaitForUser();
                    Battle.skipDefaultGenerating = true;
                    RPG.enemies.Add(new Enemy(30, "Living Fruit Punch", 10));
                }
                else if (RandomUtil.NextBool(1, 3))
                {
                    Console.WriteLine("The fruit punch was not fruit punch...");
                    Console.WriteLine($"{RPG.heros[pickedHero].name} took 5 damage, and was stunned.");
                    RPG.heros[pickedHero].TakeDamage(5);
                    RPG.heros[pickedHero].stuned = true;
                    Input.WaitForUser();
                }
                else
                {
                    Console.WriteLine($"{RPG.heros[pickedHero].name} was refreshed by the fruit punch");
                    Console.WriteLine($"{RPG.heros[pickedHero].name} gained 15 Max HP");
                    RPG.heros[pickedHero].maxHP += 15;
                    RPG.heros[pickedHero].Heal(15);
                    Input.WaitForUser();
                }
            }
        }
        public static void SpiderEvent()
        {
            Console.WriteLine("You find a village of dark figures slowly crawling around in the dark.");
            Console.WriteLine("One comes up to your crew");
            Input.WaitForUser();
            Console.WriteLine("We desperatly crave... fooooooood.");
            Console.WriteLine("The figure gesters to some lamp oil behind them (20).");
            RPG.DisplayLampOil();
            if (Input.GetUserYN("Offer a sacrifice?"))
            {
                int heroNum = Input.HeroOption(RPG.heros.ToArray());
                Console.WriteLine("Thank you... looks like my children will be eating well tonight");
                while (RPG.heros[heroNum].hp > 0)
                {
                    RPG.heros[heroNum].TakeDamage(1, true);
                    Console.WriteLine($"{RPG.heros[heroNum].name} took 1 dmg");
                    Thread.Sleep(100);
                    RPG.heros[heroNum].TakeDamage(1, true);
                    Console.WriteLine($"{RPG.heros[heroNum].name} took 1 dmg");
                    Thread.Sleep(100);
                    RPG.heros[heroNum].TakeDamage(1, true);
                    Console.WriteLine($"{RPG.heros[heroNum].name} took 1 dmg");
                    Thread.Sleep(100);
                    RPG.heros[heroNum].TakeDamage(9, true);
                    Console.WriteLine($"{RPG.heros[heroNum].name} took 9 dmg");
                    Thread.Sleep(300);
                }
                Battle.KillDeadPlayers();
                DarkContent.lampOil += 20;
            }
            else
            {
                Battle.skipDefaultGenerating = true;
                RPG.enemies.Add(new Enemy(35, "Spider", 11));
                RPG.enemies.Add(new Enemy(5, "Baby Spider", 1));
                RPG.enemies.Add(new Enemy(5, "Baby Spider", 1));
                RPG.enemies.Add(new Enemy(5, "Baby Spider", 1));
                DarkContent.lampOil += 2;
            }
        }
        public static void HouseInTheVoidEvent()
        {
            Console.WriteLine("A suburban house lights up in the distance.");
            Console.WriteLine("As you get closer you hear music coming from the inside.");
            RPG.DisplayLampOil();
            if (Input.GetUserYN("A man greets you at the enterance. Hey we are selling lamp oil, want some? I'll trade you for some work. (It will take 5 turns)"))
            {
                RPG.turnNum += 5;
                DarkContent.lampOil += 10;
            }
        }
        public static void CandyBowl()
        {
            Console.WriteLine("You approach a candy bowl omminously hoveirng a few inches of the ground, making a soft drone.");
            Console.WriteLine("There is a sign that says \"take ONLY ONE\"");
            Input.WaitForUser();
            RPG.DisplayLampOil();
            bool picking = true;
            bool shouldSpawnWitch = false;
            int timesTaken = 0;
            while (picking)
            {
                string[] options = [
                    "Gas Can (6 lamp oil)",
                    "Box of Candies (+2 Max HP to all)",
                    "Matching helmet set (Start next combat with 10 block)",
                    "Leave"];

                int pickedOption = Input.Options("Take something?", options);
                switch (pickedOption)
                {
                    case 0:
                        DarkContent.lampOil += 6;
                        Console.WriteLine("You took the gas can, and got 6 lamp oil.");
                        break;
                    case 1:
                        for (int i = 0; i < RPG.heros.Count; i++)
                        {
                            RPG.heros[i].maxHP += 2;
                            RPG.heros[i].Heal(2);
                        }
                        Console.WriteLine("All heros gained 2 hp!");
                        if (RandomUtil.Next(1) > 0.3f) Console.WriteLine($"but {RPG.heros[RPG.heros.Count - 1].name} absolutely hated the taste.");
                        break;
                    case 2:
                        for (int i = 0; i < RPG.heros.Count; i++)
                        {
                            RPG.heros[i].defence += 10;
                        }
                        Console.WriteLine("The team now looks epic, and is pretty impenetrable (for a single turn)");
                        break;
                    default:
                        picking = false;
                        break;
                }
                timesTaken++;
                if (picking && timesTaken > 1 && RandomUtil.NextBool(1, 3))
                {
                    shouldSpawnWitch = true;
                    picking = false;
                }
            }
            if (shouldSpawnWitch)
            {
                Messages.ColoredWriteLine("You short sighted, nitwit there are more lifes at stake than just your own!", ConsoleColor.Blue);
                Battle.skipDefaultGenerating = true;
                RPG.enemies.Add(new RegeneratingEnemy(65f, "The Wicked Witch of the West", 22, 20f));
                RPG.enemies.Add(new RegeneratingEnemy(18f, "Black Cat", 2, 18f));
            }
            else if (timesTaken == 1)
            {
                Console.WriteLine("You here a raspy whisper in the distance...");
                Messages.ColoredWriteLine("Good luck fending off the Kaleidoscope", ConsoleColor.Blue);
            }
        }
        public static void AntiSpaghettiSquadEvent()
        {
            Console.WriteLine("People swing down from ropes above surrounding you.");
            Console.WriteLine("A little gremlin steps out of the shadows, it looks like he wants to talk.");
            if (Input.GetUserYN("Hear them out? (They look pretty dangerous)"))
            {
                Console.WriteLine("We need your help to beat the spaghetti monsters.");
                Console.WriteLine("They have overcome 'Japan', and we need your help reclaiming it from them. It would be a difficult battle, but we would fight with eachother side by side.");
                if (Input.GetUserYN("Will you join us in battle?"))
                {
                    Battle.skipDefaultGenerating = true;
                    DarkContent.lampOil += 25;
                    RPG.enemies.Add(new Enemy(99, "Spaghetti God", 19));
                    RPG.enemies.Add(new DefendingEnemy(17, "Spaghettius MK.2🛡", 8, 7));
                    RPG.enemies.Add(new Enemy(26, "Meatballer", 13));
                    RPG.enemies.Add(new Enemy(18, "Spagettera", 14));
                    RPG.enemies.Add(new Enemy(6, "Noodler", 8));
                    RPG.enemies.Add(new Enemy(6, "Noodlest", 7));
                    RPG.heros.Add(new Hero(14f, "Gremlin", new Move[] { new WeakenMove("Pee on them"), new StunningMove("Slam em'", 9, 0.3f) }));
                    RPG.heros[RPG.heros.Count - 1].OnSpawn();
                    RPG.heros.Add(new Hero(19f, "Fruit Ninja", new Move[] { new AllTargetsDamage("Slice", 4f) }));
                    RPG.heros[RPG.heros.Count - 1].OnSpawn();
                    RPG.heros.Add(new Hero(24f, "Ninja #132", new Move[] { new StealthMove("Stealh Strike", 14f), new AllTargetsDamage("Ninja Slice", 4f), new DefendMove("Hide", 10, false) }));
                    RPG.heros[RPG.heros.Count - 1].OnSpawn();
                    RPG.heros.Add(new Hero(34f, "Golem", new Move[] { new TargetedDefendMove("Golem Block", 8), new StunningMove("Big Fat Punch", 4f, 0.2f) }));
                    RPG.heros[RPG.heros.Count - 1].OnSpawn();
                }
                else
                {
                    Console.WriteLine("We wish you best of luck on your travels.");
                    if (DarkContent.lampOil <= 5)
                    {
                        Console.WriteLine("Here is some lamp oil, I see you are struggling for it");
                        Console.WriteLine("Gained 5 lamp oil");
                        DarkContent.lampOil += 5;
                    }
                    Input.WaitForUser();
                }
            }
            else
            {
                Battle.skipDefaultGenerating = true;
                RPG.enemies.Add(new Enemy(9, "Gremlin", 11));
                RPG.enemies.Add(new Enemy(6, "Fruit Ninja", 6));
                RPG.enemies.Add(new Enemy(5, "Ninja #132", 5));
                RPG.enemies.Add(new DefendingEnemy(16, "Golem🛡", 6, 4));
            }
        }
        public static void BlackHoleEvent()
        {
            Messages.ColoredWriteLine("You stumble upon a black hole.", ConsoleColor.DarkBlue);
            Console.WriteLine("Don't question it.");
            Input.WaitForUser();
            if (Input.GetUserYN("Touch it?"))
            {
                if (Input.GetUserYN("Are you sure?"))
                {
                    if (Input.GetUserYN("Are you sure you are sure?"))
                    {
                        RPG.heros.Add(new Hero(20f, "Black Hole", new Move[] { new GuiotineMove("Suck in", 4f) },
                            new Hero(45f, "Destabalizing Hole", [new AllTargetsIncludingHerosDamage("Expell Radiation", 5f)],
                            new Hero(100f, "Apotheosis", [new GlowMove("Glow"), new AllTargetsDamage("Blind", 9f), new HealMove("Gleam", 40f, 2), new SecretTechniqueMove("Empower", false)]))));
                        RPG.heros[RPG.heros.Count - 1].OnSpawn();
                        RPG.heros[RPG.heros.Count - 1].PrintHeroDescription();
                    }
                }
            }
        }
        public static void BuildABot()
        {
            Console.WriteLine($"{RPG.heros[0].name} walks through the growing fog. It gets harder and harder to see with each step. Smog fills their lungs.");
            Console.WriteLine("BAM, they hit a giant steel door, it appears to be unlocked.");
            if (Input.GetUserYN("Open the door?"))
            {
                Console.WriteLine("Machines whirl around you spinning around the facility. Arms grab at parts nearby assembling them into robots.");
                Console.WriteLine("With all the smog you can't see where they are going");
                Console.WriteLine("Something seems odd about the noises coming from them, but you can't put your finger on it.");
                Input.WaitForUser();
                Console.WriteLine("A mechanical arm slings at you");
                Messages.ColoredWriteLine("Would you like to build a bot today sir or madam?", ConsoleColor.Green);
                int option = Input.Options(question: "", ["Build a bot", "Book it"]);
                if (option == 0)
                {

                    int health = 25;
                    List<Move> moves = new List<Move>();
                    Messages.ColoredWriteLine("Great!", ConsoleColor.Green);
                    Messages.ColoredWriteLine("Which ribcage would you like to use!", ConsoleColor.Green);
                    string[] ribcageOptions = ["RC Car", "Giant Ball Of Putty", "Volitile Oil Drum"];
                    int ribcageOptionIndex = Input.Options("", ribcageOptions);
                    if (ribcageOptionIndex == 0)
                    {
                        health += 10;
                        moves.Add(new StunningMove("Run Over", 6, 0.5f));
                        Messages.ColoredWriteLine("Let me just make sure the batteries last long enough", ConsoleColor.Green);
                        Console.WriteLine("[Places in a AA battery]");
                    }
                    else if (ribcageOptionIndex == 1)
                    {
                        health += 30;
                        moves.Add(new SelfHealMove("Meld Together", 2f, 99));
                        Messages.ColoredWriteLine("Uhhh... interesting, I'll see what I can do", ConsoleColor.Green);
                    }
                    else
                    {
                        moves.Add(new SelfDamageMove("Explode", 40f, 99f));
                        Messages.ColoredWriteLine("I hope you live a good long life...", ConsoleColor.Green);
                    }

                    Messages.ColoredWriteLine("Now for some limbs, what body is good without some limbs!", ConsoleColor.Green);
                    Messages.ColoredWriteLine($"Which limbs would you like to add to your {ribcageOptions[ribcageOptionIndex]}?", ConsoleColor.Green);
                    int limbOptionIndex = Input.Options("", ["Razerblades", "Comic Books", "You"]);
                    if (limbOptionIndex == 0)
                    {
                        health -= 5;
                        moves.Add(new DamageMove("Blade 'Em", 11));
                        Messages.ColoredWriteLine("Nice pick, should be able to do some REAL carnage.", ConsoleColor.Green);
                    }
                    else if (limbOptionIndex == 1)
                    {
                        health += 10;
                        moves.Add(new UselessMove("Read from your sleves"));
                        Messages.ColoredWriteLine("This does not seem very, uhh practical.", ConsoleColor.Green);
                    }
                    else
                    {
                        Messages.ColoredWriteLine("I'm not sure I can do that?", ConsoleColor.Green);
                        Messages.ColoredWriteLine("I guess I can try?", ConsoleColor.Green);
                        Input.WaitForUser();
                        Console.WriteLine("The arm starts sawing itself off");
                        Messages.ColoredWriteLine("Wait, hold up I might be stupid-,", ConsoleColor.Green);
                        Thread.Sleep(1500);
                        for (int i = 0; i < 7; i++)
                        {
                            Messages.ColoredWriteLine("stupid-", i > 2 ? ConsoleColor.Red : ConsoleColor.Green);
                            Thread.Sleep(300);
                        }
                        Input.WaitForUser();
                        Console.WriteLine($"The arm is still somewhat opperational, but no longer sentient.");
                        Console.WriteLine($"{RPG.heros[0].name} decides to take it with him for some repairs if nessesary");
                        RPG.heros[0].moves = RPG.heros[0].moves.Append(new SelfHealMove("Mechanical Arm Repair", 20f, 1)).ToArray();
                        RPG.heros[0].OnSpawn();
                        Input.WaitForUser();
                        return;
                    }


                    Messages.ColoredWriteLine("Now time for the finishing touches!", ConsoleColor.Green);
                    Messages.ColoredWriteLine("What would you like to name your new form!", ConsoleColor.Green);

                    string name = "";
                    while (name.Trim().Length == 0)
                    {
                        string? nameGiven = Console.ReadLine();
                        if (nameGiven != null) name = nameGiven;
                    }

                    Messages.ColoredWriteLine("Are you ready to become a bot an embrace the steel!", ConsoleColor.Green);
                    Console.WriteLine("Sawblades, chainsaws, hammers and more come from the ceiling and surround you.");
                    Input.WaitForUser();
                    RPG.heros.Add(new Hero(health, name, moves.ToArray()));
                    RPG.heros[RPG.heros.Count - 1].OnSpawn();

                    while (RPG.heros[0].hp > 0)
                    {
                        RPG.heros[0].TakeDamage(8, true);
                        Console.WriteLine($"{RPG.heros[0].name} took 8 dmg");
                        Thread.Sleep(100);
                    }
                    Battle.KillDeadPlayers();
                    Input.WaitForUser();
                }
                else
                {
                    Console.WriteLine($"{RPG.heros[0].name} dashes away as fast as possible, triping and falls over a shiny metal object");
                    RPG.heros[0].TakeDamage(2);
                    Console.WriteLine($"{RPG.heros[0].name} took 2 dmg");
                    Input.WaitForUser();
                    Console.WriteLine($"{RPG.heros[0].name} seems to have triped on a fine titanium shield.");
                    RPG.heros[0].moves = RPG.heros[0].moves.Append(new DefendMove("Titatium Shield", 4)).ToArray();
                    RPG.heros[0].OnSpawn();
                    Console.WriteLine($"{RPG.heros[0].name} gained the move titanium shield, which blocks all characters for 4 damage.");
                    Input.WaitForUser();
                }
            }
            else
            {
                Console.WriteLine($"{RPG.heros[0].name} walks away, slowly. When he notices something on the ground.");
                Console.WriteLine($"{RPG.heros[0].name} bends down, and picks up a fine titanium shield off the ground.");
                RPG.heros[0].moves = RPG.heros[0].moves.Append(new DefendMove("Titatium Shield", 4)).ToArray();
                RPG.heros[0].OnSpawn();
                Console.WriteLine($"{RPG.heros[0].name} gained the move titanium shield, which blocks all characters for 4 damage.");
                Input.WaitForUser();
            }
        }
    }
}
