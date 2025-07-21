using RandomRPG.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG
{
    public static class TechContent
    {
        public static int voidCallTurn = 0;
        public static bool cacheAccsessed = false;
        public static bool voidCall = false;
        public static string[] possibleGenericHeroNames = new string[]
        {
            "Yellow Lantern",
            "Gregory the 3rd",
            "Patrick",
            "Man Of Pringlesvile"
        };
        public static Hero[] possibleHeros = new Hero[]
        {
            new Hero(32f, "Gun", new Move[]{new DamageMove("Shoot", 6f)},
                new Hero(75f, "Railcannon", new Move[] { new StunningMove("Blasting Obliteration", 12f, 0.2f), new WeakenMove("Ominously Stand Still")})),
            new Hero(28f, "Skateboarder", new Move[]{new UselessMove("Kickflip"), new UselessMove("Dragon Flip"), new UselessMove("360 Spin")},
                new Hero(19f, "RAD Skater", new Move[]{ new RecoveringMove("Knock Out"), new SacrificeMove("Hospital Flip", 15f, 5f), new UselessMove("360 Spin")})),
            new Hero(16f, "Flappy Bird", new Move[]{ new StunningMove("Slam", 4f, 0.5f)}),
            new Hero(39f, "Gordon Ramsay", new Move[]{ new WeakenMove("Idiot Sandwich"), new HealMove("Cook", 7f, 20), new UselessMove("Nothing")}),
            new Hero(30f, "Minecraft Dog", new Move[]{new DamageMove("Bite", 7f), new HealMove("Feed Rotten Flesh", 5f, 64)},
                new Hero(60f, "Dog's Ghost", new Move[]{new WeakenMove("Haunt"), new DefendMove("Bestow Luck", 4f)})),
            new Hero(20f, "Multi-Explosive", new Move[]{ new RecoveringMove("EMP Blast"), new AllTargetsDamage("Press Big Red Button", 3f), new DefendMove("Insta-Wall", 1)}),
            new Hero(35f, "Chomper", new Move[]{ new StunningMove("NOM", 5f, 0.2f), new SacrificeMove("Furosious NNOOMM", 20f, 15f)}),
            new Hero(25f, "Just A Bomb", new Move[]{ new SacrificeMove("Blow Up", 40f, 99f), new UselessMove("Make Bomb Beeps")},
                new Hero(45f, "Just A Thermonuclear Warhead", new Move[]{new SacrificeMove("Blow Up", 80f, 99f), new WeakenMove("Threatening Beeps"), new UselessMove("Make Bomb Beeps") }))
        };
        public static Enemy[] possibleEasyEnemies = new Enemy[]
        {
            new Enemy(9f, "Toaster!", 7f),
            new Enemy(4f, "Dislike!", 10f),
            new Enemy(17f, "Reditor", 3f),
            new Enemy(4f, "That Fly", 4f),
            new Enemy(4f, "LittleBadGuy", 4f),
            new Enemy(5f, @"( ͡° ͜ʖ ͡°)", 2f)
        };
        public static Enemy[] possibleMediumEnemies = new Enemy[]
        {
            new DefendingEnemy(15f, "Phycic🛡 ", 3f, 2f),
            new DefendingEnemy(24f, "The Wall 2.0!🛡 ", 3f, 2f),
            new Enemy(18f, "Nigerian Prince!!", 15f),
            new Enemy(8f, "2020!!!", 20f),
            new Enemy(24f, "The Dealer!!", 11f),
            new Enemy(18f, "Clown!", 9f),
            new Enemy(30f, "Discord Mod", 4f)
        };
        public static RecoverableEnemy[] recoverableEnemies = new RecoverableEnemy[]
        {
            new RecoverableEnemy(20f, "Callie!🥽 ", 5f,
                new Hero(20f, "Callie", new Move[]{new DamageMove("Scratch", 5f), new WeakenMove("Meow")},
                    new Hero(75f, "Mecha-Callie", new Move[] { new DamageMove("Missiles", 8f), new WeakenMove("Meow"), new AllTargetsDamage("TailSwipe", 3f) }))),
            new RecoverableEnemy(20f, "Trunker!🥽 ", 6f,
                new Hero(20f, "Trunker", new Move[]{new DamageMove("Hacksaw", 6f), new AllTargetsDamage("Shotgun", 3f)})),
            new RecoverableEnemy(20f, "Fruit Ninja!🥽 ", 7f,
                new Hero(20f, "Fruit Ninja", new Move[]{new AllTargetsDamage("Slice", 3f)},
                    new Hero(40f, "Soda Samari", new Move[]{ new AllTargetsDamage("Ultra Gamer Attack", 5f), new HealMove("Healing Dew", 20f, 3)}))),
            new RecoverableEnemy(28f, "Fruit Punch!🥽 ", 7f,
                new Hero(28f, "Fruit Punch", new Move[]{new DamageMove("Punch", 7f)})),
            new RecoverableEnemy(32f, "Baby Shark🥽 ", 0f,
                new Hero(32f, "Baby Shark", new Move[]{new UselessMove("Cry")}))
        };
        public static Enemy[] possibleHardEnemies = new Enemy[]
        {
            new Enemy(44f, "Astro Guardian!!", 13f)
        };
        public static Enemy[][] miniBossess = new Enemy[][]
        {
            new Enemy[]
            {
                new Enemy(18f, "Bomb-omb!!!", 18f),
                new TimeBomb(35f, "Atomic Bomb💀 ", 4f, 2),
                new DefendingEnemy(66f, "Oppenheimer🛡 ", 5f, 4f),
                new Enemy(18f, "Bomb-omb!!!", 18f)
            },
            new Enemy[]
            {
                new TimeBomb(300f, "Black Hole💀 ", 4f, 3)
            },
            new Enemy[]
            {
                new Enemy(9f, "Flying Pizza!", 7f),
                new Enemy(9f, "Flying Pizza!", 7f),
                new TimeBomb(100f, "Grim Reaper💀 ", 4f, 4),
                new DefendingEnemy(33f, "Gigantic Pizza🛡 ", 5f, 5f),
                new Enemy(25f, "Rigitoni Cannon!!", 16f),
                new Enemy(5f, "Glowstick!", 7f),
                new Enemy(5f, "Glowstick!", 7f),
                new Enemy(5f, "Glowstick!", 7f)
            },
            new Enemy[]
            {
                new RecoverableEnemy(17f, "Goblin Tinkerer🥽 ", 7f,
                    new Hero(17f, "Goblin Tinkerer", new Move[]{ new DefendMove("Tinker", 2), new StunningMove("Hammer Time", 2f, 0.5f)})),
                new TimeBomb(170f, "Moon Lord💀 ", 12f, 5),
                new Enemy(40f, "Lunitic Cultist!!!", 22f),
                new Enemy(30f, "Cultist!", 9f)
            }
        };
        public static void SetupContent()
        {
            TechCampaignStartMessage();
            RPG.possibleGenericHeroNames = possibleGenericHeroNames;
            RPG.possibleHeros = possibleHeros;
            RPG.possibleEasyEnemies = possibleEasyEnemies;
            RPG.possibleMediumEnemies = possibleMediumEnemies;
            RPG.possibleHardEnemies = possibleHardEnemies;
            RPG.recruitsTillBoss += 2;
            voidCall = false;
            RPG.heros.Add(new Hero(20f, possibleGenericHeroNames[RandomUtil.Next(0, possibleGenericHeroNames.Length)], new Move[] { new DamageMove("Uppercut", 7f), new StunningMove("Slam", 4f, 0.5f) }));
            RPG.heros.Add(possibleHeros[RandomUtil.Next(0, possibleHeros.Length)]);
            for (int i = 0; i < RPG.heros.Count; i++)
            {
                RPG.heros[i].OnSpawn();
                RPG.heros[i].PrintHeroDescription();
            }
            RPG.enemies.Add(new Enemy(14f, "Meta Officer!", 6f));
            RPG.enemies.Add(new Enemy(7f, "Theif!!", 9f));
            RPG.enemies.Add(new Enemy(12f, "Living Boomerang", 4f));
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                RPG.enemies[i].OnSpawn();
            }
            //if (RandomUtil.Next(0, 4) == 0) Program.voidCall = true;
        }
        public static void Events()
        {
            if (RPG.turnNum < 18 && RPG.turnNum > 10 && RandomUtil.NextDouble() < 0.5f && Battle.PlayerHasRecoveringMove() && !cacheAccsessed)
            {
                GenerateCache();
            }
        }
        public static void GenerateCache()
        {
            Console.WriteLine();
            Console.WriteLine("A mysterious cache appears in front of you.");

            if (RPG.GetUserYN("Do you want to approch it? (y/n)"))
            {
                cacheAccsessed = true;
                RPG.enemies.Add(new Cache(100f, "Meta Cache", 0f, new Enemy[] {
                            new RecoverableEnemy(55f, "Bilbert🥽", 0f,
                                new Hero(55f, "Bilbert", new Move[]{ new SuperStunMove("Laugh")})),
                            new RecoverableEnemy(25f, "Mac!!!🥽", 14f,
                                new Hero(25f, "Mac", new Move[]{ new DefenceErrasingMove("BBQ Blaster", 5f), new HealMove("Eat Mac&Cheese", 20f, 1)},
                                    new Hero(50f, "Mac & Cheese", new Move[]{ new HealMove("Eat Mac&Cheese", 20f, 4), new DefenceErrasingMove("Scolding Sticky Cheese", 9f)}))),
                            new RecoverableEnemy(30f, "Table!!!!!!🥽", 90f,
                                new Hero(30f, "Table", new Move[]{ new RandomMove("Break Reality"), new DefendMove("Table Shield", 2), new UselessMove("Table Flight")})),
                            new Enemy(45f, "Cache Protector!!!", 17f)}));
                for (int i = 0; i < RPG.enemies.Count; i++)
                {
                    RPG.enemies[i].OnSpawn();
                }
                return;
            }
        }
        public static bool VoidCallUpdate()
        {
            switch (voidCallTurn)
            {
                case 1:
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine();
                    Console.WriteLine("...");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("A rumble can be felt in the distance, something is off.");
                    RPG.WaitForUser();
                    break;
                case 2:
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine();
                    Console.WriteLine("...");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("A strange rumble can be felt in the distance, something is o̸̠̐̂f̷̢́f̵̜͠.");
                    RPG.WaitForUser();
                    break;
                case 3:
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine();
                    Console.WriteLine("...");
                    System.Threading.Thread.Sleep(2000);
                    if(RPG.GetUserYN("W̵͔̽o̶̢͂ư̸̯l̵̠̅d̷̯̅ ̷͉͗y̶̤͗ô̵̯ṵ̴̆ ̷̛̤l̵̦̎ī̴̼k̸̛̹e̴̘͑ ̸̢̒a̵̮͐ ̵̤̒d̶̰͋e̶͈͝a̴̯͊l̶̩̓?̵̪̋ ̸̱̀(̷͉̋ẙ̴ͅ/̵̭̑n̵͎͘)̶͎͗"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine();
                        Console.WriteLine("...");
                        System.Threading.Thread.Sleep(2000);
                        if (RPG.GetUserYN("D̵̠̑ȏ̸͓ ̴̪̆ÿ̵̠́ő̵̘ũ̴̞ ̵̰̆à̵̼ḡ̷̞ŕ̵͚é̴ͅe̸͇̕ ̸̪͋t̷̟̅o̷̫͠ ̷̟͋s̸̪͠é̸͍r̸͎̒v̷̨̀é̷̢?̷̗̾ ̷̠͠(̶͉͘ÿ̵̞/̵̧̐n̵͌ͅ)̵̼͑"))
                        {
                            Hero voidServent = new Hero(120f, "Void Servent", new Move[]{ new CorruptMove("Convert Hero", true), new CorruptMove("Convert Enemy", false)});
                            RPG.RecruitHero(voidServent);
                        }
                        else voidCall = false;
                    }
                    else voidCall = false;
                    break;
                default:
                    break;
            }
            voidCallTurn++;
            return false;
        }
        public static void GenerateEnemies()
        {
            Battle.skipDefaultGenerating = true;
            if (RPG.waveNum % 2 == 0)
            {
                RecoverableEnemy enemy = TechContent.recoverableEnemies[RandomUtil.Next(0, TechContent.recoverableEnemies.Length)];
                if (!enemy.recoverableHero.recruited) RPG.enemies.Add(enemy);
            }
            if (RPG.turnNum >= 18 && RandomUtil.NextDouble() < 0.7f)
            {
                Battle.SpawnRandomEncounter(miniBossess);
                return;
            }

            Battle.skipDefaultGenerating = false;
        }
        static void TechCampaignStartMessage()
        {
            Console.WriteLine(@" __  __      _           ___                  _   _ ");
            Console.WriteLine(@"|  \/  | ___| |_ __ _   / _ \ _   _  ___  ___| |_| |");
            Console.WriteLine(@"| |\/| |/ _ \ __/ _` | | | | | | | |/ _ \/ __| __| |");
            Console.WriteLine(@"| |  | |  __/ || (_| | | |_| | |_| |  __/\__ \ |_|_|");
            Console.WriteLine(@"|_|  |_|\___|\__\__,_|  \__\_\\__,_|\___||___/\__(_)");
            Console.WriteLine("After defeating the government, nothing is in the way of the alien: MARK ZUCKERBERG. Mark has immediatly started to meta everything including the so " +
                "called \"heros\" who took over the government.");
        }
        public static void Sacrifice()
        {

        }
    }
}
