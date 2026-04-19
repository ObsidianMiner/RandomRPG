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
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                RPG.enemies[i].defence += defenceStrength;
            }
        }
        public override void OnSpawn()
        {
            base.OnSpawn();
            Console.WriteLine($"{name} is protecting all enemies for {defenceStrength}hp.");
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                RPG.enemies[i].defence += defenceStrength;
            }
        }
        public override void OnDeath()
        {
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                RPG.enemies[i].defence -= defenceStrength;
            }
        }
        public override Enemy Clone()
        {
            return new DefendingEnemy(maxHP, name, dmg, defenceStrength);
        }
    }
    public class AdvertisementEnemy : Enemy
    {
        bool hasAdvertised = false;
        public AdvertisementEnemy(float hp, string name, float dmg) : base(hp, name, dmg)
        {

        }
        public override void DoTurn()
        {
            base.DoTurn();
            if (!hasAdvertised)
            {
                System.Diagnostics.Process.Start("https://noobyest.itch.io");
                hasAdvertised = true;
            }
        }
        public override Enemy Clone()
        {
            return new AdvertisementEnemy(hp, name, dmg);
        }
    }
    public class RegeneratingEnemy : Enemy
    {
        public float regen;
        public RegeneratingEnemy(float hp, string name, float dmg, float regen) : base(hp, name, dmg)
        {
            this.regen = regen;
        }
        public override void DoTurn()
        {
            Console.WriteLine($"{name} heals itself for {regen}hp.");
            Heal(regen);
            base.DoTurn();
        }
        public override string HPStatus()
        {
            return base.HPStatus() + $" 💚{regen}";
        }
        public override Enemy Clone()
        {
            return new RegeneratingEnemy(maxHP, name, dmg, regen);
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
            if (turns == 0)
            {
                Console.WriteLine("Obliteration in");
                System.Threading.Thread.Sleep(1000);
                for (int i = 10; i-- > 0;)
                {
                    Console.WriteLine(i);
                    System.Threading.Thread.Sleep(1000);
                    if (RandomUtil.NextDouble() < 0.07f)
                    {
                        System.Threading.Thread.Sleep(3000);
                        Console.WriteLine($"{name} malfunctioned!");
                        System.Threading.Thread.Sleep(1000);
                        return;
                    }
                }
                for (int i = 0; i < RPG.heros.Count; i++)
                {
                    RPG.heros[i].TakeDamage(99);
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
                RPG.enemies.Add(enemyDrops[i]);
                enemyDrops[i].OnSpawn();
                enemyDrops[i].stuned = true;
            }
            RPG.enemies.Remove(this);
        }
        public override void OnSpawn()
        {
            base.OnSpawn();
            defence = 99f;
        }
    }
    public class TemporaryEnemy : Enemy
    {
        public TemporaryEnemy(float hp, string name, float dmg, ConsoleColor color = ConsoleColor.White) : base(hp, name, dmg)
        {
            this.nameColor = color;
        }
        public override void DoTurn()
        {
            base.DoTurn();
            TakeDamage(9999f, true);
        }
        public override string HPStatus()
        {
            return base.HPStatus() + "✨";
        }
        public override Enemy Clone()
        {
            return new TemporaryEnemy(maxHP, name, dmg, nameColor);
        }
    }
}
