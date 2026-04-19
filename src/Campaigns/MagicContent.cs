using RandomRPG.Entities;

namespace RandomRPG
{
    public static class MagicContent
    {
        public static int lampOil = 15;
        public static Hero[] startingHeros = new Hero[]
        {
            new Hero(32, "Forest Giant", new Move[]{ new DamageMove("NOOM NOOM, TASTES GOOD", 6f)}),
            new Hero(30, "R2D2", new Move[]{ new AllTargetsDamage("Chain Zap", 3f), new DamageMove("Ram", 4f)}),
            new Hero(29, "007", new Move[]{ new StealthMove("Assasinate", 13f), new StunningMove("Drive a car into the enemy", 0f, 0.5f), new SuperStunMove("Stun Grenade")}),
            new Hero(38, "Steel Rod", new Move[]{ new DefendMove("Block the way!", 3), new DamageMove("Drop on", 5f)})
        };
        public static Hero[] possibleHeros = new Hero[]
        {
            new Hero(18f, "Jeff", new Move[]{new HealMove("Mysterious Water Jet", 6f, 999), new DefendMove("Hide", 5, false)}),
            new Hero(12f, "Huntsman", new Move[]{ new StealthMove("Snipe", 13f), new UselessMove("Fall Over")}),
            new Hero(25f, "The Mugger", new Move[]{ new ConvertMove("Mug"), new UselessMove("Smug")}),
            new Hero(26f, "Batman", new Move[]{new DamageMove("Batarang", 6f), new StunningMove("Crash Batmobile into", 0f, 0.5f)}),
            new Hero(24f, "Bungus", new Move[]{ new HealMove("Bung Others", 5f, 5), new SelfHealMove("Bung Yourself", 5f, 999) },
                new Hero(52f, "Wungus", [new HealMove("Wung Others", 8f, 99), new StunAllMove("Spore Explosion", 1)])),
            new Hero(2f, "Task Manager", new Move[]{ new DamageMove("Kill Process", 20f)},
                new Hero(8f, "Task Master", [new DamageMove("Erasure", 30f)])),
            new Hero(24f, "Frozone", new Move[]{ new StunningMove("Ice Spray", 3f, 0.6f), new DefendMove("Dodge", 10f, false), new SelfDamageMove("Ice Ray", 15f, 15f)}),
            new Hero(18f, "Braken", [new WeakenMove("Stare at"), new GuiotineMove("Snap Neck", 6f)]),
            new Hero(26f, "Loader", [new DamageMove("Bash", 6f)],
                new Hero(36f, "True Form Loader", [new DamageMove("GRAND SLAM", 13f)])),
            new Hero(27f, "Mac", [new WeakenMove("Wave Stick"), new DamageMove("BBQ Blaster", 4f)]),
        };
        public static Enemy[] possibleEasyEnemies = new Enemy[]
        {
            new Enemy(3f, "Bedbugs", 5),
            new Enemy(7f, "Plug Snake", 4),
            new Enemy(5f, "Your Shadow", 7),
            new DefendingEnemy(3f, "Cobweb🛡", 0, 2),
            new Enemy(6f, "Pillbug", 5),
            new Enemy(2, "Bad Breath", 12f)
        };
        public static Enemy[] possibleMediumEnemies = new Enemy[]
        {
            new Enemy(16f, "Darkri", 14),
            new Enemy(30f, "Dremmur", 5f),
            new Enemy(18f, "Ghost", 12),
            new Enemy(26f, "Ari", 14),
            new DefendingEnemy(30f, "Concrete Wall🛡", 0.5f, 2),
            new Enemy(25f, "Hank Hill", 8f),
            new Enemy(21f, "Woody", 9f),
            new Enemy(13f, "Paralysis Demon", 19f),
            new Enemy(19f, "Fireball Mage", 16f)
        };
        public static Enemy[] possibleHardEnemies = new Enemy[]
        {
            new Enemy(61f, "𐌍𐌵𐌋𐌋", 17f),
            new Enemy(36f, "Herobrine", 14f),
            new Enemy(44f, "Eyeless Dog", 11),
            new RecoverableEnemy(60f, "Nokia Phone🥽", 3f, new Hero(40f, "Nokia Phone", new Move[]{ new DefendMove("Harden", 4)})),
            new Enemy(32f, "The Guitar Hero", 15f),
        };
        public static Enemy[][] miniBossess = new Enemy[][]
        {
            new Enemy[] {
                new Enemy(50f, "Crimson Moon!!", 12f),
                new Enemy(12f, "Warewolf!!!!", 19f),
                new Enemy(12f, "Warewolf!!!!", 19f),
                new Enemy(12f, "Warewolf!!!!", 19f)
            },
            new Enemy[] {
                new Enemy(5f, "Your Shadow", 7),
                new Enemy(19f, "Imp", 6),
                new AdvertisementEnemy(30f, "Advertisement", 2),
                new RegeneratingEnemy(24f, "Slime", 6, 5),
                new Enemy(27f, "Sentiant Flying Sword", 18)
            },
            new Enemy[]
            {
                new Enemy(14f, "Tar Slime", 7f),
                new RegeneratingEnemy(45f, "Clay Dunestrider", 18f, 18f),
                new Enemy(14f, "Tar Slime", 7f)
            },
            new Enemy[]
            {
                new Enemy(28f, "Anxiety", 16),
                new DefendingEnemy(23f, "Guilt🛡", 15, 3),
                new Enemy(35f, "Despair", 9),
                new Enemy(26f, "Heartache", 9)
            }
        };

        public static string[] tips =
        {
            "TIP: Enemies get harder with each turn, not just wave! Make your battles short.",
            "TIP: Don't let the lamp go out! You can get more lamp oil from events.",
            "TIP: It's Tyler's fault."
        };

        public static void GenerateEnemies()
        {
            if (RPG.turnNum > 22)
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

            RPG.recruitsTillBoss += 4;
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

            RPG.enemies.Add(new Enemy(2, "Shadow Ant Bro", 10f));
            RPG.enemies.Add(new Enemy(10f, "Flying Sword", 5f));
            RPG.enemies.Add(new Enemy(6f, "Rubber Duck", 2f));
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                RPG.enemies[i].OnSpawn();
            }
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
