namespace RandomRPG.Entities
{
    public class Boss : Enemy
    {
        public static Enemy[] summons = new Enemy[]
        {
            new Enemy(30f, "The IRS!!!", 8f),
            new Enemy(16f, "Joe Biden!!", 12f),
            new Enemy(16f, "FBI!!!", 19f),
            new Enemy(35f, "Homeland Security!!!!", 20f),
            new TimeBomb(9f, "Balistic Missile💀", 9f, 2),
            new Enemy(20f, "CIA!!!", 8f)
        };
        int turnNum;
        public Boss(float hp, string name, float dmg) : base(hp, name, dmg)
        {

        }
        void Summon(Enemy summon)
        {
            Console.WriteLine($"Prepare yourself, The Government sends {summon.name}.");
            summon.OnSpawn();
            RPG.enemies.Add(summon);
            summon.PrintStatus();
            RPG.enemies[1].PrintStatus();
        }
        public override void DoTurn()
        {
            if (turnNum == 0)
            {
                Summon(summons[0]);
                Summon(summons[1]);
            }
            else if (turnNum == 1)
            {
                Summon(summons[2]);
                Summon(summons[3]);
            }
            else if (turnNum == 3)
            {
                Summon(summons[4]);
                Summon(RPG.possibleEasyEnemies[RandomUtil.Next(0, RPG.possibleEasyEnemies.Length)]);
            }
            else if (turnNum == 4)
            {
                Summon(summons[5]);
                Summon(RPG.possibleEasyEnemies[RandomUtil.Next(0, RPG.possibleEasyEnemies.Length)]);
            }
            else
            {
                Summon(RPG.possibleMediumEnemies[RandomUtil.Next(0, RPG.possibleMediumEnemies.Length)]);
            }
            turnNum++;
        }
    }
}
