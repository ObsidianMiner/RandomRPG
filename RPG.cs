using RandomRPG.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomRPG
{
    public static class RPG
    {
        public static List<Hero> heros = new List<Hero>();
        public static List<Enemy> enemies = new List<Enemy>();
        public static int windowWidth = 115;
        public static int turnNum;
        public static int waveNum;
        public static int herosRecruited = 0;
        public static bool gameRunning = true;
        public static bool techCampaign = false;
        public static bool magicCapaign = false;
        public static int recruitsTillBoss = 6;
        public static bool bossSpawned = false;
        public static string[] possibleGenericHeroNames = new string[]
        {
            "TheeThyThoe",
            "Solider #384",
            "John Smith",
            "Rodrick",
            "Man Of Pringlesvile"
        };

        //Loaded Content
        public static Hero[] possibleHeros = new Hero[] { };
        public static Enemy[] possibleEasyEnemies = new Enemy[] { };
        public static Enemy[] possibleMediumEnemies = new Enemy[] { };
        public static Enemy[] possibleHardEnemies = new Enemy[] { };

        static void SetupWindow()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetWindowSize(4, 4);
            Console.WindowHeight = 2;
            Console.WindowHeight++;
        }

        static void Main(string[] args)
        {
            SetupWindow();
            BeginJourneyScreen();
            MainLoop();
            WaitForUser();
        }
        public static void MainLoop()
        {
            while (gameRunning)
            {
                waveNum++;
                BattleLoop();
                if (heros.Count == 0 || MagicContent.lampOil < 0 || (magicCapaign && !heros.Any(h => h.name == "The Lamp")))
                {
                    gameRunning = false;
                    Messages.LoseGameMessage();
                    break;
                }
                if (herosRecruited >= recruitsTillBoss && bossSpawned)
                {
                    Messages.WinGameMessage();
                    break;
                }
                else Camp();
                Battle.GenerateEnemies();
            }
        }
        public static void BattleLoop()
        {
            if (herosRecruited >= recruitsTillBoss && !TechContent.voidCall)
            {
                Messages.BossMessage();
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
                if (magicCapaign)
                {
                    MagicContent.lampOil--;
                    if (MagicContent.lampOil < 0) return;
                    Console.WriteLine($"💡{MagicContent.lampOil} lamp oil left...");
                }
                for (int i = 0; i < heros.Count; i++)
                {
                    Battle.PrintField();
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
                if (heros.Count == 0 || MagicContent.lampOil < 0 || (magicCapaign && !heros.Any(h => h.name == "The Lamp"))) return;
                WaitForUser();
            }
        }
        static void BeginJourneyScreen()
        {
            Messages.StartGameMessage();

            Console.WriteLine("Press Any Key To Continue");
            char selectedCampaign = Console.ReadKey().KeyChar;

            switch (selectedCampaign)
            {
                case 'v':
                    techCampaign = true;
                    TechContent.SetupContent();
                    break;
                case 'm':
                    magicCapaign = true;
                    MagicContent.SetupContent();
                    break;
                default:
                    StartingContent.Setup();
                    break;
            }
        }
        static void Camp()
        {
            Messages.CampMessage();

            int firstRecruit = FindNewRecruit(-1);
            int secondRecruit = FindNewRecruit(firstRecruit);
            int upgrade = FindUpgrade();

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
                if (magicCapaign)
                {
                    tips = new string[]
                    {
                        "TIP: Enemies get harder with each turn, not just wave! Make your battles short.",
                        "TIP: Don't let the lamp go out! You can get more lamp oil from events.",
                        "TIP: Be ready for something to happen on turn 22!",
                        "TIP: It's Tyler's fault."
                    };
                }
                Console.WriteLine(tips[Math.Min(waveNum - 1, tips.Length - 1)]);
                Console.WriteLine("Recruits at camp:");
                Console.WriteLine();
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


                if (waveNum <= 3) Console.WriteLine("Upgrading Characters will be unlcoked at wave 4.");
                else
                {
                    if (upgrade != -1)
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

        public static bool GetUserYN(string question)
        {
            Console.WriteLine(question);
            bool answered = false;
            bool answer = false;
            while (!answered)
            {
                ConsoleKey key = Console.ReadKey().Key;
                Console.WriteLine();
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
                    Console.WriteLine(question + " (y/n)");
                }
            }
            return answer;
        }
        public static int HeroOption(Hero[] options)
        {
            int selected = -1;
            while (selected == -1)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"{i}.{options[i].name}");
                }
                if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int sel))
                {
                    selected = sel;
                }
            }
            return selected;
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
                if (!possibleHeros[recruit].recruited && recruit != alreadyInSelection) return recruit;
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

    }
}
