namespace RandomRPG.Entities
{
    public class DamageMove : Move
    {
        public float dmg;
        public DamageMove(string name, float damage)
        {
            this.name = name;
            this.dmg = damage;
            description = $"Deals {dmg}dmg.";
        }
        public override bool DoMove(Entity target)
        {
            Console.WriteLine($"{name} attacked {target.name} for {target.GetDamageDelt(dmg)}");
            target.TakeDamage(dmg);
            return true;
        }
    }
    public class StealthMove : Move
    {
        public float dmg;
        public StealthMove(string name, float dmg)
        {
            this.name = name;
            this.dmg = dmg;
            description = $"Sneak attack for {dmg}dmg but can only be used on the first turn.";
        }
        public override bool DoMove(Entity target)
        {
            if (Battle.turnBattleCycleStartedOn != RPG.turnNum)
            {
                Console.WriteLine("You can't sneak attack past the first turn!");
                return false;
            }
            Console.WriteLine($"{name} sneak attacked {target.name} for {target.GetDamageDelt(dmg)}");
            target.TakeDamage(dmg);
            return true;
        }
    }
    public class AllTargetsDamage : Move
    {
        public float dmg;
        public AllTargetsDamage(string name, float damage)
        {
            this.name = name;
            this.dmg = damage;
            this.hasTarget = false;
            description = $"Does {dmg}dmg to all targets.";
        }
        public override bool DoMove(Entity target)
        {
            Console.WriteLine($"{name} attacked all enemies for {dmg}");
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                RPG.enemies[i].TakeDamage(dmg, true);
            }
            Battle.KillDeadEnemies();
            return true;
        }
    }
    public class SelfDamageMove : Move
    {
        public float dmg;
        public float cost;
        public SelfDamageMove(string name, float damage, float cost)
        {
            this.name = name;
            this.dmg = damage;
            this.cost = cost;
            description = $"Deals {dmg}dmg, but costs {cost}hp to use.";
        }
        public override bool DoMove(Entity target)
        {
            Console.WriteLine($"{name} attacked {target.name} for {target.GetDamageDelt(dmg)}");
            target.TakeDamage(dmg);
            owner.TakeDamage(cost);
            return true;
        }
    }
    public class GuiotineMove : Move
    {
        public float healthThreshhold;
        public GuiotineMove(string name, float healthThreshhold)
        {
            this.name = name;
            this.healthThreshhold = healthThreshhold;
            description = $"Kill all enemies with less than {healthThreshhold} health.";
            hasTarget = false;
        }
        public override bool DoMove(Entity target)
        {
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                if (RPG.enemies[i].hp <= healthThreshhold) RPG.enemies[i].TakeDamage(9999, true);
            }
            Battle.KillDeadEnemies();
            return true;
        }
    }
}
