using RandomRPG.Entities;

namespace RandomRPG
{
    public static class RPG
    {
        public static List<Hero> heros = new List<Hero>();
        public static List<Enemy> enemies = new List<Enemy>();
        public static int windowWidth = 135;
        public static int turnNum;
        public static int waveNum;

        public static int herosRecruited = 0;
        public static int recruitsTillBoss = 6;
        public static bool bossSpawned = false;

        public static bool techCampaign = false;
        public static bool magicCapaign = false;

        //Loaded Content
        public static Hero[] possibleHeros = { };
        public static Enemy[] possibleEasyEnemies = { };
        public static Enemy[] possibleMediumEnemies = { };
        public static Enemy[] possibleHardEnemies = { };

        public static string[] tips = { };

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            BeginJourneyScreen();
            MainLoop();
            Input.WaitForUser();
        }
        public static void MainLoop()
        {
            bool gameRunning = true;
            while (gameRunning)
            {
                waveNum++;
                RunBattle();
                bool lampLoss = (magicCapaign && (MagicContent.lampOil < 0 || !heros.Any(h => h.name == "The Lamp")));
                if (heros.Count == 0 || lampLoss)
                {
                    gameRunning = false;

                    if (lampLoss) Messages.DarknessConsumesMessage();
                    else Messages.LoseGameMessage();

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
        public static void DisplayLampOil() => Console.WriteLine($"💡{MagicContent.lampOil} lamp oil left...");
        public static void SpawnBoss()
        {
            Messages.BossMessage();
            bossSpawned = true;
            if (magicCapaign)
            {
                enemies.Clear();
                enemies.Add(new Kaleidoscope(999f, "Kaleidoscope", 8f));
                enemies[0].OnSpawn();
            }
            else if (techCampaign)
            {
                enemies.Clear();
                enemies.Add(new Mark(600f, "Mark Zuckerberg", 12f));
                enemies[0].OnSpawn();
            }
            else
            {
                enemies.Clear();
                enemies.Add(new Boss(200f, "The Government!!!!", 13f));
                enemies[0].OnSpawn();
            }
        }
        public static void RunBattle()
        {
            if (herosRecruited >= recruitsTillBoss)
            {
                SpawnBoss();
            }
            while (turnNum < 1000)
            {
                turnNum++;
                Messages.ColoredWriteLine($"Turn {turnNum}", ConsoleColor.Yellow);
                if (magicCapaign)
                {
                    MagicContent.lampOil--;
                    if (MagicContent.lampOil < 0) return;
                    DisplayLampOil();
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
                Input.WaitForUser();
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
            string upgradingCharacterName = "";
            if (upgrade >= 0) upgradingCharacterName = heros[upgrade].name; //Used to fix reordering to change the upgrade.

            bool gotAid = false;
            while (!gotAid)
            {
                if (upgrade > -1) upgrade = heros.FindIndex(h => h.name == upgradingCharacterName);
                Console.WriteLine("Wellcome back to the cozy campsite. Your heros have each healed by 10. There are recruits waiting pick one.");

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

                Console.WriteLine("o.");
                Console.WriteLine("Reorder party.");
                Console.WriteLine();

                string key = Console.ReadKey().KeyChar.ToString();

                if (key == "0")
                {
                    RecruitHero(possibleHeros[firstRecruit]);
                    gotAid = true;
                }
                else if (key == "1")
                {
                    RecruitHero(possibleHeros[secondRecruit]);
                    gotAid = true;
                }
                else if (key == "2")
                {
                    heros[upgrade].Upgrade();
                    gotAid = true;
                }
                else if (key == "o")
                {
                    ReOrderParty();
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
        public static void ReOrderParty()
        {
            Console.WriteLine("Select each character in the order you want them");
            List<Hero> orderedHeros = new List<Hero>();
            while (heros.Count > 0)
            {
                for (int i = 0; i < heros.Count; i++)
                {
                    Console.WriteLine($"{i}. {heros[i].name}");
                }
                if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int indexPicked) && indexPicked >= 0 && indexPicked < heros.Count)
                {
                    orderedHeros.Add(heros[indexPicked]);
                    heros.RemoveAt(indexPicked);
                }
                Console.WriteLine("");
            }
            heros = orderedHeros;
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

    }
}
