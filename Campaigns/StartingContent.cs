using RandomRPG.Entities;

namespace RandomRPG
{
    public class StartingContent
    {
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

        public static void Setup()
        {
            RPG.possibleHeros = possibleHeros;
            RPG.possibleEasyEnemies = possibleEasyEnemies;
            RPG.possibleMediumEnemies = possibleMediumEnemies;
            RPG.possibleHardEnemies = possibleHardEnemies;

            RPG.heros.Add(new Hero(20f, RPG.possibleGenericHeroNames[RandomUtil.Next(0, RPG.possibleGenericHeroNames.Length)], Move.basicMoveset));
            RPG.heros.Add(RPG.possibleHeros[RandomUtil.Next(0, RPG.possibleHeros.Length)]);
            for (int i = 0; i < RPG.heros.Count; i++)
            {
                RPG.heros[i].OnSpawn();
                RPG.heros[i].PrintHeroDescription();
            }
            RPG.enemies.Add(new Enemy(2f, "LittleBadGuy", 3f));
            RPG.enemies.Add(new Enemy(20f, "BigBadGuy!", 6f));
            RPG.enemies.Add(new Enemy(8f, "Theif!!", 9f));
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                RPG.enemies[i].OnSpawn();
            }
        }
    }
}
