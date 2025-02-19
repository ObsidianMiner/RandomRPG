using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Entities;

namespace RandomRPG
{
    public static class Program
    {
        public static List<Hero> heros = new List<Hero>();
        public static List<Enemy> enemies = new List<Enemy>();
        public static int windowWidth = 170;
        public static int turnNum;
        public static int waveNum;
        public static int herosRecruited = 0;
        public static bool gameRunning = true;
        public static bool techCampaign = false;
        public static int recruitsTillBoss = 6;
        static bool bossSpawned = false;
        static bool cacheAccsessed = false;
        public static bool voidCall = false;
        public static string[] possibleGenericHeroNames = new string[]
        {
            "TheeThyThoe",
            "Solider #384",
            "John Smith",
            "Rodrick",
            "Man Of Pringlesvile"
        };
        public static Hero[] possibleHeros = new Hero[]
        {
            new Hero(20f, "Callie", new Move[]{new DamageMove("Scratch", 5f), new WeakenMove("Meow")},
                new Hero(75f, "Mecha-Callie", new Move[] { new DamageMove("Missiles", 8f), new WeakenMove("Meow"), new AllTargetsDamage("TailSwipe", 3f) })),
            new Hero(75f, "Pringle Man", new Move[]{new UselessMove("Eat Pringles")}),
            new Hero(20f, "Fruit Ninja", new Move[]{new AllTargetsDamage("Slice", 3f)},
                new Hero(40f, "Soda Samari", new Move[]{ new AllTargetsDamage("Ultra Gamer Attack", 5f), new HealMove("Healing Dew", 20f, 3)})),
            new Hero(20f, "Trunker", new Move[]{new DamageMove("Hacksaw", 6f), new AllTargetsDamage("Shotgun", 3f)}),
            new Hero(32f, "Baby Shark", new Move[]{new UselessMove("Cry")}),
            new Hero(25f, "Bowl Of Soup", new Move[]{new HealMove("Healing Soup", 12f, 6), new DamageMove("Burn", 3f)},
                new Hero(50f, "Hot Chili", new Move[]{new HealMove("Healing Soup", 25f, 6), new DamageMove("Bean Blast", 8f), new WeakenMove("Enflame")})),
            new Hero(28f, "Fruit Punch", new Move[]{new DamageMove("Punch", 7f)}),
            new Hero(30f, "Minecraft Dog", new Move[]{new DamageMove("Bite", 7f), new HealMove("Feed Rotten Flesh", 5f, 64)},
                new Hero(60f, "Dog's Ghost", new Move[]{new WeakenMove("Haunt"), new DefendMove("Bestow Luck", 4f)})),
            new Hero(20f, "Steve", new Move[]{new DamageMove("Punch", 5f)})
        };
        public static Enemy[] possibleEasyEnemies = new Enemy[]
        {
            new Enemy(8f, "Migget", 3f),
            new Enemy(5f, "Red Pepper!", 6f),
            new Enemy(5f, "Green Pepper!", 6f),
            new Enemy(2f, "Cockroach", 3f),
            new Enemy(1f, "Earth Worm", 2f),
            new Enemy(6f, "Bacon", 2f),
            new Enemy(10f, "Flying Pizza", 3f)
        };
        public static Enemy[] possibleMediumEnemies = new Enemy[]
        {
            new Enemy(19f, "Ferrari!!", 10f),
            new Enemy(15f, "Archer #394!", 11f),
            new Enemy(30f, "The Wall", 0.5f),
            new Enemy(17f, "Snake!!", 14f),
            new Enemy(18f, "Asian Parents!!", 13f)
        };
        public static Enemy[] possibleHardEnemies = new Enemy[]
        {
            new Enemy(36f, "Uncle Sam!!!", 16f),
            new Enemy(44f, "Astro Guardian!!", 13f)
        };
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowWidth = windowWidth;
            Console.WindowHeight++;
            Setup();
            while (gameRunning)
            {
                waveNum++;
                MainLoop();
                if (heros.Count == 0)
                {
                    gameRunning = false;
                    LoseGameMessage();
                    break;
                }
                if (herosRecruited >= recruitsTillBoss && bossSpawned)
                {
                    WinGameMessage();
                    break;
                }
                if (voidCall && TechContent.voidCallTurn >= 4) TechContent.Sacrifice();
                else Camp();
                GenerateEnemies();
            }
            WaitForUser();
        }
        public static bool GetUserYN(string question)
        {
            Console.WriteLine(question);
            bool answered = false;
            bool answer = false;
            while (!answered)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Y)
                {
                    answer = true;
                    answered = true;
                }
                else if (key == ConsoleKey.N)
                {
                    answer = false;
                    answered = true;
                }
                else
                {
                    Console.WriteLine(question);
                }
            }
            return answer;
        }
        static void GenerateCache()
        {
            Console.WriteLine();
            Console.WriteLine("A mysterious cache appears in front of you.");

            if (GetUserYN("Do you want to approch it? (y/n)"))
            {
                cacheAccsessed = true;
                enemies.Add(new Cache(100f, "Meta Cache", 0f, new Enemy[] {
                            new RecoverableEnemy(55f, "Bilbert🥽", 0f,
                                new Hero(55f, "Bilbert", new Move[]{ new SuperStunMove("Laugh")})),
                            new RecoverableEnemy(25f, "Mac!!!🥽", 14f,
                                new Hero(25f, "Mac", new Move[]{ new DefenceErrasingMove("BBQ Blaster", 5f), new HealMove("Eat Mac&Cheese", 20f, 1)},
                                    new Hero(50f, "Mac & Cheese", new Move[]{ new HealMove("Eat Mac&Cheese", 20f, 4), new DefenceErrasingMove("Scolding Sticky Cheese", 9f)}))),
                            new RecoverableEnemy(30f, "Table!!!!!!🥽", 90f,
                                new Hero(30f, "Table", new Move[]{ new RandomMove("Break Reality"), new DefendMove("Table Shield", 2), new UselessMove("Table Flight")})),
                            new Enemy(45f, "Cache Protector!!!", 17f)}));
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].OnSpawn();
                }
                return;
            }
        }
        public static void MainLoop()
        {
            if (herosRecruited >= recruitsTillBoss && !voidCall)
            {
                BossMessage();
                bossSpawned = true;
                if (techCampaign)
                {
                    enemies.Clear();
                    enemies.Add(new Mark(1000f, "Mark Zuckerberg", 12f));
                    enemies[0].OnSpawn();
                }
                else
                {
                    enemies.Clear();
                    enemies.Add(new Boss(200f, "The Government!!!!", 13f));
                    enemies[0].OnSpawn();
                }
            }
            while (turnNum < 1000)
            {
                turnNum++;
                Console.WriteLine($"Turn {turnNum}");
                for (int i = 0; i < heros.Count; i++)
                {
                    PrintField();
                    heros[i].TryTurn();
                    if (enemies.Count == 0) return;
                }
                Console.WriteLine();
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].defence = 0;
                }
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].TryTurn();
                    if (heros.Count == 0) return;
                }
                for (int i = 0; i < heros.Count; i++)
                {
                    heros[i].defence = 0;
                }
                if (heros.Count == 0) return;
                WaitForUser();
            }
        }
        public static void GenerateEnemies()
        {
            if (techCampaign)
            {

                if (turnNum < 18 && turnNum > 10 && RandomUtil.NextDouble() < 0.5f && PlayerHasRecoveringMove() && !cacheAccsessed)
                {
                    GenerateCache();
                }
                if (waveNum % 2 == 0)
                {
                    RecoverableEnemy enemy = TechContent.recoverableEnemies[RandomUtil.Next(0, TechContent.recoverableEnemies.Length)];
                    if (!enemy.recoverableHero.recruited) enemies.Add(enemy);
                }
                if (turnNum >= 18 && RandomUtil.NextDouble() < 0.7f)
                {
                    Enemy[] miniBoss = TechContent.miniBossess[RandomUtil.Next(0, TechContent.miniBossess.Length)];
                    for (int i = 0; i < miniBoss.Length; i++)
                    {
                        if(!(miniBoss[i] is RecoverableEnemy recoverableEnemy) || !recoverableEnemy.recoverableHero.recruited) enemies.Add(miniBoss[i].Clone());
                    }
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].OnSpawn();
                    }
                    return;
                }
            }

            enemies.Add(possibleEasyEnemies[RandomUtil.Next(0, possibleEasyEnemies.Length)].Clone());
            enemies.Add(possibleMediumEnemies[RandomUtil.Next(0, possibleMediumEnemies.Length)].Clone());
            if(RandomUtil.NextDouble() < 0.5f && turnNum > 6) enemies.Add(possibleMediumEnemies[RandomUtil.Next(0, possibleMediumEnemies.Length)].Clone());
            if (RandomUtil.NextDouble() < 0.5f)
            {
                int swarmEnemyCount = (turnNum / 8) + 1;
                if(turnNum > 20) enemies.Add(possibleHardEnemies[RandomUtil.Next(0, possibleHardEnemies.Length)].Clone());
                for (int i = 0; i < swarmEnemyCount; i++)
                {
                    enemies.Add(possibleEasyEnemies[RandomUtil.Next(0, possibleEasyEnemies.Length)].Clone());
                }
            }
            else
            {
                if (turnNum < (techCampaign ? 18 : 26) + RandomUtil.Next(-6, 10)) enemies.Add(possibleMediumEnemies[RandomUtil.Next(0, possibleMediumEnemies.Length)].Clone());
                else if (turnNum < (techCampaign ? 20 : 30) + RandomUtil.Next(-6, 20)) enemies.Add(possibleHardEnemies[RandomUtil.Next(0, possibleHardEnemies.Length)].Clone());
                else
                {
                    enemies.Add(possibleHardEnemies[RandomUtil.Next(0, possibleHardEnemies.Length)].Clone());
                    enemies.Add(possibleHardEnemies[RandomUtil.Next(0, possibleHardEnemies.Length)].Clone());
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].OnSpawn();
            }
        }
        public static bool PlayerHasRecoveringMove()
        {
            for (int i = 0; i < heros.Count; i++)
            {
                if (heros[i].HasRecoveryMove()) return true;
            }
            return false;
        }
        public static void KillDeadPlayers()
        {
            for (int i = 0; i < heros.Count; i++)
            {
                if(heros[i].hp <= 0)
                {
                    Console.WriteLine($"{heros[i].name} Died! Get Your Act Together!");
                }
            }
            heros.RemoveAll(hero => hero.hp <= 0);
        }
        public static void KillDeadEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].hp <= 0)
                {
                    if(enemies[i].hp < -4) Console.WriteLine($"You Obliterated {enemies[i].name}!");
                    else Console.WriteLine($"You Killed {enemies[i].name}!");
                }
            }
            enemies.RemoveAll(enemy => enemy.hp <= 0);
        }
        static void Setup()
        {
            StartGameMessage();

            Console.WriteLine("Press Any Key To Continue");
            char selectedCampaign = Console.ReadKey().KeyChar;
            if (selectedCampaign == 'v') techCampaign = true;
            Console.WriteLine("");
            if (!techCampaign)
            {
                heros.Add(new Hero(20f, possibleGenericHeroNames[RandomUtil.Next(0, possibleGenericHeroNames.Length)], Move.basicMoveset));
                heros.Add(possibleHeros[RandomUtil.Next(0, possibleHeros.Length)]);
                for (int i = 0; i < heros.Count; i++)
                {
                    heros[i].OnSpawn();
                    heros[i].PrintHeroDescription();
                }
                enemies.Add(new Enemy(2f, "LittleBadGuy", 3f));
                enemies.Add(new Enemy(20f, "BigBadGuy!", 6f));
                enemies.Add(new Enemy(8f, "Theif!!", 9f));
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].OnSpawn();
                }
            }
            else
            {
                TechContent.SetupTechContent();
                heros.Add(new Hero(20f, possibleGenericHeroNames[RandomUtil.Next(0, possibleGenericHeroNames.Length)], new Move[] { new DamageMove("Uppercut", 7f), new StunningMove("Slam", 4f, 0.5f)}));
                heros.Add(possibleHeros[RandomUtil.Next(0, possibleHeros.Length)]);
                for (int i = 0; i < heros.Count; i++)
                {
                    heros[i].OnSpawn();
                    heros[i].PrintHeroDescription();
                }
                enemies.Add(new Enemy(14f, "Meta Officer!", 6f));
                enemies.Add(new Enemy(7f, "Theif!!", 9f));
                enemies.Add(new Enemy(12f, "Living Boomerang", 4f));
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].OnSpawn();
                }
            }
        }
        static void PrintField()
        {
            Console.WriteLine(new string('#', windowWidth));
            PrintSide(heros.ToEntities());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            PrintSide(enemies.ToEntities());
            Console.WriteLine(new string('#', windowWidth));
        }
        static void PrintSide(List<Entity> side)
        {
            
            int spacing = windowWidth / (side.Count + 1);
            string[] names = new string[side.Count];
            string[] status = new string[side.Count];
            string[][] sideInfo = new string[][] { names, status };
            for (int i = 0; i < side.Count; i++)
            {
                names[i] = side[i].name;
                status[i] = side[i].HPStatus();
            }
            string[] rows = EvenColumns(spacing, sideInfo);
            for (int i = 0; i < rows.Length; i++)
            {
                Console.WriteLine(rows[i].PadLeft(rows[i].Length + spacing));
            }

            Console.WriteLine();
        }
        static void Camp()
        {
            CampMessage();
            bool gotAid = false;
            while (!gotAid)
            {
                Console.WriteLine("Wellcome back to the cozy campsite. Your heros have each healed by 10. There are recruits waiting pick one.");
                string[] tips =
                {
                    "TIP: The ! on enemies determenes there danger or how much damage they wil do.",
                    "TIP: Enemies get harder with each turn, not just wave! Make your battles short.",
                    "TIP: Take out the strong enemies first so that you won't be struggling to take them out later.",
                    "TIP: Upgrading will delay you be noticed by the boss."
                };
                if (techCampaign)
                {
                    tips = new string[]
                    {
                        "TIP: VR(🥽 ) enemies can be recovered with special moves.",
                        "TIP: Enemies get harder with each turn, not just wave! Make your battles short.",
                        "TIP: After turn 18, VERY POWERFULL MINIBOSSES can occur! Watch out, and be prepared!",
                        "TIP: Heros recover 10hp after every battle.",
                        "TIP: Enemies get harder with each turn, not just wave! Make your battles short.",
                        "TIP: Defending enemies (🛡 ) protect all other enemies!"
                    };
                }
                Console.WriteLine(tips[Math.Min(waveNum - 1, tips.Length - 1)]);
                Console.WriteLine("Recruits at camp:");
                Console.WriteLine();
                int firstRecruit = FindNewRecruit(-1);
                int secondRecruit = FindNewRecruit(firstRecruit);
                if (firstRecruit != -1)
                {
                    Console.Write("0.");
                    possibleHeros[firstRecruit].PrintHeroDescription();
                    Console.WriteLine();
                }
                if (secondRecruit != -1)
                {
                    Console.Write("1.");
                    possibleHeros[secondRecruit].PrintHeroDescription();
                    Console.WriteLine();
                }


                int upgrade = FindUpgrade();
                if (waveNum <= 3) Console.WriteLine("Upgrading Characters will be unlcoked at wave 4.");
                else
                {
                    if(upgrade != -1)
                    {
                        Console.WriteLine("Upgrade of the day:");
                        Console.WriteLine($"Upgrade {heros[upgrade].name} to");
                        Console.Write("2.");
                        heros[upgrade].upgrade.PrintHeroDescription();
                        Console.WriteLine();
                    }
                }

                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out int pickedHero)) continue;
                if (pickedHero == 0)
                {
                    RecruitHero(possibleHeros[firstRecruit]);
                    gotAid = true;
                }
                else if (pickedHero == 1)
                {
                    RecruitHero(possibleHeros[secondRecruit]);
                    gotAid = true;
                }
                else if (pickedHero == 2)
                {
                    heros[upgrade].Upgrade();
                    gotAid = true;
                }
            }
            for (int i = 0; i < heros.Count; i++)
            {
                heros[i].Heal(10f);
            }
        }
        public static void RecruitHero(Hero hero)
        {
            heros.Add(hero);
            hero.OnSpawn();
            herosRecruited++;
        }
        static int FindNewRecruit(int alreadyInSelection)
        {
            int attempts = 0;
            while (attempts < 900)
            {
                attempts++;
                int recruit = RandomUtil.Next(0, possibleHeros.Length);
                if(!possibleHeros[recruit].recruited && recruit != alreadyInSelection) return recruit;
            }
            return -1;
        }
        static int FindUpgrade()
        {
            int attempts = 0;
            while (attempts < 900)
            {
                attempts++;
                int upgradeable = RandomUtil.Next(0, heros.Count);
                if (heros[upgradeable].upgrade != null) return upgradeable;
            }
            return -1;
        }
        public static void WaitForUser()
        {
            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();
            Console.WriteLine("");
        }
        static void StartGameMessage()
        {

            Console.WriteLine(@" ____             _             _                            ___ ");
            Console.WriteLine(@"| __ )  ___  __ _(_)_ __       | | ___  _ __ _ __   ___ _   |__ \");
            Console.WriteLine(@"|  _ \ / _ \/ _` | | '_ \   _  | |/ _ \| '__| '_ \ / _ \ | | |/ /");
            Console.WriteLine(@"| |_) |  __/ (_| | | | | | | |_| | (_) | |  | | | |  __/ |_| |_|");
            Console.WriteLine(@"|____/ \___|\__, |_|_| |_|  \___/ \___/|_|  |_| |_|\___|\__, (_) ");
            Console.WriteLine(@"            |___/                                       |___/    ");
        }
        static void LoseGameMessage()
        {
            Console.WriteLine(@" ____  _    _ _ _      ___                    ");
            Console.WriteLine(@"/ ___|| | _(_) | |    |_ _|___ ___ _   _  ___ ");
            Console.WriteLine(@"\___ \| |/ / | | |     | |/ __/ __| | | |/ _ \");
            Console.WriteLine(@" ___) |   <| | | |     | |\__ \__ \ |_| |  __/");
            Console.WriteLine(@"|____/|_|\_\_|_|_|    |___|___/___/\__,_|\___|");
        }
        public static void WinGameMessage()
        {
            Console.WriteLine(@"__        ___                       _ _ ");
            Console.WriteLine(@"\ \      / (_)_ __  _ __   ___ _ __| | |");
            Console.WriteLine(@" \ \ /\ / /| | '_ \| '_ \ / _ \ '__| | |");
            Console.WriteLine(@"  \ V  V / | | | | | | | |  __/ |  |_|_|");
            Console.WriteLine(@"   \_/\_/  |_|_| |_|_| |_|\___|_|  (_|_)");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(@"A new challenger approches! Press V at the start for a new campagin!");
        }
        public static void CampMessage()
        {
            Console.WriteLine(@"  ____                      ");
            Console.WriteLine(@" / ___|__ _ _ __ ___  _ __  ");
            Console.WriteLine(@"| |   / _` | '_ ` _ \| '_ \ ");
            Console.WriteLine(@"| |__| (_| | | | | | | |_) |");
            Console.WriteLine(@" \____\__,_|_| |_| |_| .__/ ");
            Console.WriteLine(@"                     |_|    ");
        }
        public static void BossMessage()
        {
            Console.WriteLine(@" _____ _             _   ____                _ ");
            Console.WriteLine(@"|  ___(_)_ __   __ _| | | __ )  ___  ___ ___| |");
            Console.WriteLine(@"| |_  | | '_ \ / _` | | |  _ \ / _ \/ __/ __| |");
            Console.WriteLine(@"|  _| | | | | | (_| | | | |_) | (_) \__ \__ \_|");
            Console.WriteLine(@"|_|   |_|_| |_|\__,_|_| |____/ \___/|___/___(_)");
        }
        public static string[] EvenColumns(int desiredWidth, IEnumerable<IEnumerable<string>> lists)
        {
            return EvenColumns(desiredWidth, true, lists).ToArray();
        }

        public static IEnumerable<string> EvenColumns(int desiredWidth, bool rightOrLeft, IEnumerable<IEnumerable<string>> lists)
        {
            return lists.Select(o => EvenColumns(desiredWidth, rightOrLeft, o.ToArray()));
        }

        public static string EvenColumns(int desiredWidth, bool rightOrLeftAlignment, string[] list, bool fitToItems = false)
        {
            // right alignment needs "-X" 'width' vs left alignment which is just "X" in the `string.Format` format string
            int columnWidth = (rightOrLeftAlignment ? -1 : 1) *
                                // fit to actual items? this could screw up "evenness" if
                                // one column is longer than the others
                                // and you use this with multiple rows
                                (fitToItems
                                    ? Math.Max(desiredWidth, list.Select(o => o.Length).Max())
                                    : desiredWidth
                                );

            // make columns for all but the "last" (or first) one
            string format = string.Concat(Enumerable.Range(rightOrLeftAlignment ? 0 : 1, list.Length - 1).Select(i => string.Format("{{{0},{1}}}", i, columnWidth)));

            // then add the "last" one without Alignment
            if (rightOrLeftAlignment)
            {
                format += "{" + (list.Length - 1) + "}";
            }
            else
            {
                format = "{0}" + format;
            }

            return string.Format(format, list);
        }
    }
}
