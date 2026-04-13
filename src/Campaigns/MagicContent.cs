using RandomRPG.Entities;

namespace RandomRPG
{
    public static class MagicContent
    {
        const int doorTurn = 30;
        public static int lampOil = 15;
        public static Hero[] startingHeros = new Hero[]
        {
            new Hero(30, "Forest Giant", new Move[]{ new DamageMove("NOOM NOOM, TASTES GOOD", 7f)}),
            new Hero(28, "R2D2", new Move[]{ new AllTargetsDamage("Chain Zap", 3f), new RecoveringMove("Recover")}),
            new Hero(23, "007", new Move[]{ new StealthMove("Assasinate", 13f), new StunningMove("Drive a car into the enemy", 0f, 0.5f), new SuperStunMove("Stun Grenade")}),
            new Hero(34, "Steel Rod", new Move[]{ new DefendMove("Block the way!", 3), new DamageMove("Drop on", 5f)})
        };
        public static Hero[] possibleHeros = new Hero[]
        {
            new Hero(17f, "Jeff", new Move[]{new HealMove("Mysterious Water Jet", 6f, 999), new DefendMove("Hide", 7, false)}),
            new Hero(12f, "Huntsman", new Move[]{ new StealthMove("Snipe", 11f), new UselessMove("Fall Over")}),
            new Hero(25f, "The Mugger", new Move[]{ new ConvertMove("Mug"), new UselessMove("Smug")}),
            new Hero(26f, "Batman", new Move[]{new DamageMove("Batarang", 5f), new StunningMove("Crash Batmobile into", 0f, 0.5f)}),
            new Hero(27f, "Bungus", new Move[]{ new HealMove("Bung Others", 5f, 5), new SelfHealMove("Bung Yourself", 5f, 999) },
                new Hero(52f, "Wungus", [new HealMove("Wung Others", 8f, 99), new StunAllMove("Spore Explosion", 1)])),
            new Hero(2f, "Task Manager", new Move[]{ new DamageMove("Kill Process", 30f)},
                new Hero(8f, "Task Master", [new DamageMove("Erasure", 40f)])),
            new Hero(24f, "Frozone", new Move[]{ new StunningMove("Ice Spray", 3f, 0.6f), new DefendMove("Dodge", 10f, false), new SelfDamageMove("Ice Ray", 15f, 15f)}),
        };
        public static Enemy[] possibleEasyEnemies = new Enemy[]
        {
            new Enemy(3f, "Bedbugs", 5),
            new Enemy(2f, "Plug Snake", 5),
            new Enemy(5f, "Your Shadow", 7),
            new DefendingEnemy(2f, "Cobweb🛡", 0, 2),
            new Enemy(4, "Pillbug", 4),
            new Enemy(1, "Bad Breath", 12f)
        };
        public static Enemy[] possibleMediumEnemies = new Enemy[]
        {
            new Enemy(9f, "Darkri", 14),
            new Enemy(27f, "Dremmur", 4),
            new Enemy(15f, "Ghost", 10),
            new Enemy(20f, "Ari", 11),
            new DefendingEnemy(30f, "Concrete Wall🛡", 0.5f, 1),
            new Enemy(21f, "Hank Hill", 8f),
            new Enemy(13f, "Woody", 9f),
            new Enemy(11f, "Paralysis Demon", 14f)
        };
        public static Enemy[] possibleHardEnemies = new Enemy[]
        {
            new Enemy(34f, "𐌍𐌵𐌋𐌋", 12f),
            new Enemy(27f, "Herobrine", 14f),
            new Enemy(19, "Eyeless Dog", 11),
            new RecoverableEnemy(40f, "Nokia Phone🥽", 3f, new Hero(40f, "Nokia Phone", new Move[]{ new DefendMove("Harden", 4)})),
            new Enemy(25f, "The Guitar Hero", 15f)
        };
        public static Enemy[][] miniBossess = new Enemy[][]
        {
            new Enemy[] {
                new Enemy(40f, "Crimson Moon!!", 8f),
                new Enemy(12f, "Warewolf!!!!", 16f)
            },
            new Enemy[] {
                new Enemy(10f, "Your Shadow", 7f),
                new Enemy(12f, "Imp", 5f),
                new Enemy(12f, "Imp", 5f),
                new Enemy(12f, "Slime", 5f),
                new AdvertisementEnemy(16f, "Sentiant Flying Sword", 15f)
            }
        };
        public static Enemy[][] bossess = new Enemy[][]
        {
            new Enemy[]
            {
                new Enemy(20f, "TESTBOSS", 10f)
            }
        };
        public static Enemy[] labPossibleEasyEnemies = new Enemy[]
        {
            new Enemy(12f, "Little Guy", 23f)
        };
        public static Enemy[] labPossibleMediumEnemies = new Enemy[]
        {
            new Enemy(26f, "Gordon Freeman", 18f)
        };
        public static Enemy[] labPossibleHardEnemies = new Enemy[]
        {
            new Enemy(12f, "HARD ENEMy", 2f)
        };

        public static string[] tips =
        {
            "TIP: Enemies get harder with each turn, not just wave! Make your battles short.",
            "TIP: Don't let the lamp go out! You can get more lamp oil from events.",
            "TIP: Be ready for something to happen on turn 22!",
            "TIP: It's Tyler's fault."
        };

        public static void GenerateEnemies()
        {
            if (RPG.turnNum > doorTurn - 6)
            {
                Battle.SpawnRandomEncounter(miniBossess);
                Battle.skipDefaultGenerating = true;
                return;
            }

            RPG.enemies.Add(RPG.possibleEasyEnemies[RandomUtil.Next(0, RPG.possibleEasyEnemies.Length)].Clone());
            if (RPG.turnNum > 10) RPG.enemies.Add(RPG.possibleEasyEnemies[RandomUtil.Next(0, RPG.possibleEasyEnemies.Length)].Clone());
            Battle.skipDefaultGenerating = false;
        }
        public static void SetupContent()
        {
            StartMessage();
            RPG.possibleHeros = possibleHeros;
            RPG.possibleEasyEnemies = possibleEasyEnemies;
            RPG.possibleMediumEnemies = possibleMediumEnemies;
            RPG.possibleHardEnemies = possibleHardEnemies;

            RPG.tips = tips;

            RPG.recruitsTillBoss += 2;
            Console.WriteLine(new string('#', RPG.windowWidth));
            Console.WriteLine("Select a starting hero...");
            for (int i = 0; i < startingHeros.Length; i++)
            {
                startingHeros[i].PrintHeroDescription();
            }
            RPG.heros.Add(startingHeros[Input.HeroOption(startingHeros)]);
            RPG.heros.Add(new Hero(30f, "The Lamp", new Move[] { new DamageMove("Smack", 6f) }));

            Console.WriteLine("The darkness is encroching in, whispering all around you...");
            Console.WriteLine("Keep the latern alive, and don't let it run out of oil!");

            Console.WriteLine(new string('#', RPG.windowWidth));
            for (int i = 0; i < RPG.heros.Count; i++)
            {
                RPG.heros[i].OnSpawn();
                RPG.heros[i].PrintHeroDescription();
            }
            Battle.GenerateEnemies();
        }
        public static void LabEnterance()
        {
            Console.WriteLine("A door sudenly appears before you somewhat like you just needed to wait till turn 22.");
            Input.WaitForUser();
            Console.WriteLine("A figure blocks the way");
            Input.WaitForUser();
            Battle.SpawnRandomEncounter(bossess);
        }
        static void StartMessage()
        {
            Console.WriteLine(@"  _______ _            _____             _          ");
            Console.WriteLine(@" |__   __| |          |  __ \           | |         ");
            Console.WriteLine(@"    | |  | |__   ___  | |  | | __ _ _ __| | __      ");
            Console.WriteLine(@"    | |  | '_ \ / _ \ | |  | |/ _` | '__| |/ /      ");
            Console.WriteLine(@"    | |  | | | |  __/ | |__| | (_| | |  |   < _ _ _ ");
            Console.WriteLine(@"    |_|  |_| |_|\___| |_____/ \__,_|_|  |_|\_(_|_|_)");
            Console.WriteLine("All you hear is howling wind...");
            Console.WriteLine();
        }
    }
}
