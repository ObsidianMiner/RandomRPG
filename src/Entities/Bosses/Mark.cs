namespace RandomRPG.Entities
{

    public class Mark : Enemy
    {
        int turnNum;
        bool preparing;
        bool sentSwarm;
        public Mark(float hp, string name, float dmg) : base(hp, name, dmg)
        {
            stunImmune = true;
        }
        void Summon(Enemy summon)
        {
            RPG.enemies.Add(summon);
            summon.OnSpawn();
            summon.PrintStatus();
        }
        public override void DoTurn()
        {
            base.DoTurn();
            if (turnNum == 0)
            {
                Console.WriteLine("Mark summons his team!");
                int numSpawned = 0;
                for (int i = 0; i < TechContent.recoverableEnemies.Length; i++)
                {
                    if (!TechContent.recoverableEnemies[i].recoverableHero.recruited && numSpawned < 3)
                    {
                        numSpawned++;
                        Summon(TechContent.recoverableEnemies[i].Clone());
                    }
                }
                Hero zuckerbot = new Hero(35f, "Zuckerbot", new Move[] { new RecoveringMove("Sign Out"), new SelfDamageMove("Self Destruct", 50f, 50f), new UselessMove("Take a Selfie") });
                if (!Battle.PlayerHasRecoveringMove())
                {
                    RPG.heros.Add(zuckerbot);
                    zuckerbot.OnSpawn();
                    Console.WriteLine("A malfunctioning zuckerbot joined your team!");
                }
                else Summon(new RecoverableEnemy(35f, "Zuckerbot!!🥽", 14f, zuckerbot));
            }
            if (turnNum == 1 || (turnNum % 5 == 0 && turnNum > 4))
            {
                Console.WriteLine("Mark Defends his team!");
                for (int i = 0; i < RPG.enemies.Count; i++)
                {
                    RPG.enemies[i].defence += 3;
                }
            }
            if (turnNum == 3)
            {
                Console.WriteLine("Mark summons a ban hammer!");
                Summon(new TimeBomb(3f, "Ban Hammer💀", 3f, 2));
            }
            if (turnNum == 4)
            {
                Console.WriteLine("Mark summons a ban hammer!");
                Summon(new TimeBomb(3f, "Ban Hammer💀", 3f, 2));
                Console.WriteLine("Mark summons a ban hammer!");
                Summon(new TimeBomb(3f, "Ban Hammer💀", 3f, 2));
            }
            ConvertHero();
            if (turnNum == 6) ConvertHero();
            if (hp < 150 && turnNum % 2 == 0)
            {
                Console.WriteLine("Mark protects himself!");
                defence += 10;
            }
            if (hp < 500 && !sentSwarm)
            {
                if (preparing)
                {
                    Console.WriteLine("Mark summons a Doom Tower!");
                    Summon(new TimeBomb(3f, "Doom Tower💀", 3f, 2));
                    Console.WriteLine("Mark summons a Doom Tower!");
                    Summon(new TimeBomb(3f, "Doom Tower💀", 3f, 2));
                    Console.WriteLine("Mark summons a Doom Tower!");
                    Summon(new TimeBomb(3f, "Doom Tower💀", 3f, 2));
                    Console.WriteLine("Mark summons a Doom Tower!");
                    Summon(new TimeBomb(3f, "Doom Tower💀", 3f, 2));
                    int numSpawned = 0;
                    for (int i = 0; i < TechContent.recoverableEnemies.Length; i++)
                    {
                        if (!TechContent.recoverableEnemies[i].recoverableHero.recruited && numSpawned < 3)
                        {
                            numSpawned++;
                            Summon(TechContent.recoverableEnemies[i].Clone());
                        }
                    }
                    sentSwarm = true;
                }
                else
                {
                    Console.WriteLine("Mark is preparing a final onslaut. Get ready!");
                    preparing = true;
                }
            }
            turnNum++;
        }
        public void ConvertHero()
        {
            Hero heroToConvert = RPG.heros[RandomUtil.Next(0, RPG.heros.Count)];
            if (RPG.heros.Count > 5 && !heroToConvert.HasRecoveryMove())
            {
                Summon(new RecoverableEnemy(heroToConvert.maxHP, heroToConvert.name + "!!🥽⌬", 12f, heroToConvert, true));
                RPG.heros.Remove(heroToConvert);
                Console.WriteLine($"{heroToConvert.name} was brainwashed by zuckerberg!");
            }
            else
            {
                Console.WriteLine($"The team is to focused to be converted by zuckerberg!");
            }
        }
    }
}
