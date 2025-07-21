using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG.Entities
{
    public abstract class Move
    {
        public string name;
        public string description;
        public bool hasTarget = true;
        public bool heroTarget = false;
        public Hero owner;

        public static Move[] basicMoveset => new Move[] { new DamageMove("Punch", 4f) };
        /// <summary>
        /// Move Logic
        /// </summary>
        /// <param name="target"></param>
        /// <returns>If Move Was Sucsessful</returns>
        public abstract bool DoMove(Entity target);
    }
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
            if (Battle.battleStartTurn != RPG.turnNum)
            {
                Console.WriteLine("You can't sneak attack past the first turn!");
                return false;
            }
            Console.WriteLine($"{name} sneak attacked {target.name} for {target.GetDamageDelt(dmg)}");
            target.TakeDamage(dmg);
            return true;
        }
    }
    public class WeakenMove : Move
    {
        public WeakenMove(string name)
        {
            this.name = name;
            description = $"Weakeneds the opponent to take 50% more damage. (Can Only Be Applied Once, but stacks with other weaknessess)";
        }
        public override bool DoMove(Entity target)
        {
            if (target.damageTakenMult.HasMult(name))
            {
                Console.WriteLine($"{name} has already weakened {target.name}!");
                return false;
            }
            Console.WriteLine($"{name} weakened {target.name} by 50%.");
            target.damageTakenMult.AddMult(name, 1.5f);
            return true;
        }
    }
    public class UselessMove : Move
    {
        public UselessMove(string name)
        {
            this.name = name;
            description = "Absolutly Pointless";
            hasTarget = false;
        }
        public override bool DoMove(Entity target)
        {
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
                RPG.enemies[i].TakeDamage(dmg, i != RPG.enemies.Count - 1);
            }
            return true;
        }
    }
    public class HealMove : Move
    {
        public float hp;
        public int uses;
        public string baseName;
        public HealMove(string name, float hp, int uses)
        {
            this.baseName = name;
            this.name = $"{baseName} ({uses})";
            this.hp = hp;
            this.uses = uses;
            description = $"Heals {hp}hp. Can be used a max of {uses} time(s).";
            heroTarget = true;
        }
        public override bool DoMove(Entity target)
        {
            if (uses <= 0)
            {
                Console.WriteLine($"{name} is out of uses!");
                return false;
            }
            Console.WriteLine($"{name} healed {target.name} by {hp}hp");
            target.Heal(hp);
            uses--;
            name = $"{baseName} ({uses})";
            return true;
        }
    }
    public class SelfHealMove : Move
    {
        public float hp;
        public int uses;
        public string baseName;
        public SelfHealMove(string name, float hp, int uses)
        {
            this.baseName = name;
            this.name = $"{baseName} ({uses})";
            this.hp = hp;
            this.uses = uses;
            description = $"Heals {hp}hp. Can be used a max of {uses} time(s).";
            hasTarget = false;
        }
        public override bool DoMove(Entity target)
        {
            if (uses <= 0)
            {
                Console.WriteLine($"{name} is out of uses!");
                return false;
            }
            Console.WriteLine($"{name} healed themself by {hp}hp");
            owner.Heal(hp);
            uses--;
            name = $"{baseName} ({uses})";
            return true;
        }
    }
    public class DefendMove : Move
    {
        public float defence;
        public bool defendAll;
        public DefendMove(string name, float defence, bool defendAll = true)
        {
            this.name = name;
            this.defence = defence;
            if (defendAll) description = $"Defends all party members for {defence}hp from all incoming attacks for 1 turn.";
            else description = $"Defends themself for {defence}hp from all incoming attacks for 1 turn.";
            this.hasTarget = false;
            this.defendAll = defendAll;
        }
        public override bool DoMove(Entity target)
        {
            Console.WriteLine($"{owner.name} is defending all party members for {defence}hp.");
            if(defendAll)
            {
                for (int i = 0; i < RPG.heros.Count; i++)
                {
                    RPG.heros[i].defence += defence;
                }
            }
            else
            {
                owner.defence += defence;
            }
            return true;
        }
    }
    public class TargetedDefendMove : Move
    {
        public float defence;
        public TargetedDefendMove(string name, float defence, bool defendAll = true)
        {
            this.name = name;
            this.defence = defence;
            description = $"Defend the target for {defence} defence";
            this.heroTarget = true;
        }
        public override bool DoMove(Entity target)
        {
            Console.WriteLine($"{name} is defending all party members for {defence}hp.");
            target.defence += defence;
            return true;
        }
    }
    public class StunningMove : Move
    {
        public float dmg;
        public float chance;
        public StunningMove(string name, float damage, float chance)
        {
            this.name = name;
            this.dmg = damage;
            this.chance = chance;
            description = $"Deals {dmg}dmg; has {chance * 100}% chance to stun an enemy for 1 turn.";
        }
        public override bool DoMove(Entity target)
        {
            Console.WriteLine($"{name} attacked {target.name} for {target.GetDamageDelt(dmg)}");
            if(RandomUtil.NextDouble() < chance)
            {
                Console.WriteLine($"{name} stunned {target.name}!");
                target.stuned = true;
            }
            target.TakeDamage(dmg);
            return true;
        }
    }
    public class RecoveringMove : Move
    {
        public RecoveringMove(string name)
        {
            this.name = name;
            description = $"Recovers selected VR(🥽) enemy from there brainwashing.";
        }
        public override bool DoMove(Entity target)
        {
            if(target is RecoverableEnemy recoverableEnemy)
            {
                Hero newHero = recoverableEnemy.recoverableHero;
                if (!recoverableEnemy.corrupted)
                {
                    if (!newHero.recruited) RPG.RecruitHero(newHero);
                    Console.WriteLine($"{newHero.name} was recovered from zuckerberg! {newHero.name} is now a valuable member of the team!");
                    newHero.PrintHeroDescription();
                    if (newHero.HasDefenceErrasingMove())
                    {
                        RPG.heros.Remove(newHero);
                        RPG.heros.Insert(0, newHero);
                    }
                    RPG.enemies.Remove((Enemy)target);
                }
                else
                {
                    Console.WriteLine($"{newHero.name} is corrupted, and died in the process of recovering them!");
                    RPG.enemies.Remove((Enemy)target);
                }
                return true;
            }
            return false;
        }
    }
    public class SacrificeMove : Move
    {
        public float dmg;
        public float cost;
        public SacrificeMove(string name, float damage, float cost)
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
    public class SuperStunMove : Move
    {
        public SuperStunMove(string name)
        {
            this.name = name;
            description = $"Stuns an opponent and themself.";
        }
        public override bool DoMove(Entity target)
        {
            Console.WriteLine($"{name} stuned {target.name}!");
            owner.stuned = true;
            target.stuned = true;
            return true;
        }
    }
    public class DefenceErrasingMove : Move
    {
        public float dmg;
        public DefenceErrasingMove(string name, float damage)
        {
            this.name = name;
            this.dmg = damage;
            description = $"Deals {dmg}dmg; melts through, and destroys the targets defence.";
        }
        public override bool DoMove(Entity target)
        {
            Console.WriteLine($"{name} attacked {target.name} for {target.GetDamageDelt(dmg)}");
            Console.Write($"{name} melted through {target.name}'s defence!");
            target.TakeDamage(dmg);
            target.defence = 0f;
            return true;
        }
    }
}
