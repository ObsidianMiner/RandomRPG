using RandomRPG.Campaigns;

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
                bool lampLoss = (magicCapaign && (DarkContent.lampOil < 0 || !heros.Any(h => h.name == "The Lamp")));
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
                else Camp.VistCamp();
                Battlefield.GenerateEnemies();
            }
        }
        public static void DisplayLampOil() => Console.WriteLine($"💡{DarkContent.lampOil} lamp oil left...");
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
                    DarkContent.lampOil--;
                    if (DarkContent.lampOil < 0) return;
                    DisplayLampOil();
                }
                for (int i = 0; i < heros.Count; i++)
                {
                    Battlefield.PrintField();
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
                Battlefield.KillDeadEnemies();
                for (int i = 0; i < heros.Count; i++)
                {
                    heros[i].defence = 0;
                }
                if (heros.Count == 0 || DarkContent.lampOil < 0 || (magicCapaign && !heros.Any(h => h.name == "The Lamp"))) return;
                Input.WaitForUser();
            }
        }
        public static void RecruitHero(Hero hero)
        {
            RPG.heros.Add(hero);
            hero.OnSpawn();
            RPG.herosRecruited++;
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
                    DarkContent.SetupContent();
                    break;
                default:
                    StartingContent.Setup();
                    break;
            }
        }

    }
}
