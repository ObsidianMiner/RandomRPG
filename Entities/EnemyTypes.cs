using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG.Entities
{
    public class DefendingEnemy : Enemy
    {
        public float defenceStrength;
        public DefendingEnemy(float hp, string name, float dmg, float defence) : base(hp, name, dmg)
        {
            this.defenceStrength = defence;
        }
        public override void DoTurn()
        {
            Console.WriteLine($"{name} is protecting all enemies for {defenceStrength}hp.");
            for (int i = 0; i < Program.enemies.Count; i++)
            {
                Program.enemies[i].defence += defenceStrength;
            }
        }
        public override void OnSpawn()
        {
            base.OnSpawn();
            Console.WriteLine($"{name} is protecting all enemies for {defenceStrength}hp.");
            for (int i = 0; i < Program.enemies.Count; i++)
            {
                Program.enemies[i].defence += defenceStrength;
            }
        }
        public override Enemy Clone()
        {
            return new DefendingEnemy(maxHP, name, dmg, defenceStrength);
        }
    }
    public class RecoverableEnemy : Enemy
    {
        public Hero recoverableHero;
        public bool corrupted;
        public RecoverableEnemy(float hp, string name, float dmg, Hero hero, bool corrupted = false) : base(hp, name, dmg)
        {
            this.recoverableHero = hero;
            this.corrupted = corrupted;
        }
        public override Enemy Clone()
        {
            return new RecoverableEnemy(maxHP, name, dmg, recoverableHero, corrupted);
        }
    }
    public class TimeBomb : Enemy
    {
        public int turns;
        public TimeBomb(float hp, string name, float dmg, int turns) : base(hp, name, dmg)
        {
            this.turns = turns;
        }
        public override void DoTurn()
        {
            turns--;
            if(turns == 0)
            {
                Console.WriteLine("Obliteration in");
                System.Threading.Thread.Sleep(1000);
                for (int i = 10; i --> 0;)
                {
                    Console.WriteLine(i);
                    System.Threading.Thread.Sleep(1000);
                    if(RandomUtil.NextDouble() < 0.07f)
                    {
                        System.Threading.Thread.Sleep(3000);
                        Console.WriteLine($"{name} malfunctioned!");
                        System.Threading.Thread.Sleep(1000);
                        return;
                    }
                }
                for (int i = 0; i < Program.heros.Count; i++)
                {
                    Program.heros[i].TakeDamage(99);
                }
            }
            else
            {
                Console.WriteLine($"{name} is charging up. {turns} turns until you are absolutly and utterly obliterated.");
            }

        }
        public override void OnSpawn()
        {
            base.OnSpawn();
            Console.WriteLine($"{name} is charging up. {turns} turns until you are absolutly and utterly obliterated.");
        }
        public override string HPStatus()
        {
            return base.HPStatus() + $" ({turns})";
        }
        public override Enemy Clone()
        {
            return new TimeBomb(maxHP, name, dmg, turns);
        }
    }
    public class Cache : Enemy
    {
        Enemy[] enemyDrops;

        public Cache(float hp, string name, float dmg, Enemy[] enemyDrops) : base(hp, name, dmg)
        {
            stunImmune = true;
            this.enemyDrops = enemyDrops;
        }
        public override void DoTurn()
        {
            for (int i = 0; i < enemyDrops.Length; i++)
            {
                Program.enemies.Add(enemyDrops[i]);
                enemyDrops[i].OnSpawn();
                enemyDrops[i].stuned = true;
            }
            Program.enemies.Remove(this);
        }
        public override void OnSpawn()
        {
            base.OnSpawn();
            defence = 99f;
        }
    }
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
            Program.enemies.Add(summon);
            summon.OnSpawn();
            summon.PrintStatus();
        }
        public override void DoTurn()
        {
            base.DoTurn();
            if(turnNum == 0)
            {
                Console.WriteLine("Mark summons his team!");
                int numSpawned = 0;
                for (int i = 0; i < TechContent.recoverableEnemies.Length; i++)
                {
                    if(!TechContent.recoverableEnemies[i].recoverableHero.recruited && numSpawned < 3)
                    {
                        numSpawned++;
                        Summon(TechContent.recoverableEnemies[i].Clone());
                    }
                }
                Hero zuckerbot = new Hero(35f, "Zuckerbot", new Move[] { new RecoveringMove("Sign Out"), new SacrificeMove("Self Destruct", 50f, 50f), new UselessMove("Take a Selfie") });
                if (!Program.PlayerHasRecoveringMove())
                {
                    Program.heros.Add(zuckerbot);
                    zuckerbot.OnSpawn();
                    Console.WriteLine("A malfunctioning zuckerbot joined your team!");
                }
                else Summon(new RecoverableEnemy(35f, "Zuckerbot!!🥽", 14f, zuckerbot));
            }
            if(turnNum == 1 || (turnNum % 5 == 0 && turnNum > 4))
            {
                Console.WriteLine("Mark Defends his team!");
                for (int i = 0; i < Program.enemies.Count; i++)
                {
                    Program.enemies[i].defence += 3;
                }
            }
            if(turnNum == 3)
            {
                Console.WriteLine("Mark summons a ban hammer!");
                Summon(new TimeBomb(3f, "Ban Hammer💀", 3f, 2));
            }
            if(turnNum == 4)
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
            Hero heroToConvert = Program.heros[RandomUtil.Next(0, Program.heros.Count)];
            if (Program.heros.Count > 5 && !heroToConvert.HasRecoveryMove())
            {
                Summon(new RecoverableEnemy(heroToConvert.maxHP, heroToConvert.name + "!!🥽⌬", 12f, heroToConvert, true));
                Program.heros.Remove(heroToConvert);
                Console.WriteLine($"{heroToConvert.name} was brainwashed by zuckerberg!");
            }
            else
            {
                Console.WriteLine($"The team is to focused to be converted by zuckerberg!");
            }
        }
    }
}
