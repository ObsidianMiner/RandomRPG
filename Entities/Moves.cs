using System;
using System.Collections.Generic;
using System.Linq;
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
            for (int i = 0; i < Program.enemies.Count; i++)
            {
                Program.enemies[i].TakeDamage(dmg, i != Program.enemies.Count - 1);
            }
            return true;
        }
    }
    public class HealMove : Move
    {
        public float hp;
        public int uses;
        public HealMove(string name, float hp, int uses)
        {
            this.name = name;
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
            return true;
        }
    }
    public class DefendMove : Move
    {
        public float defence;
        public DefendMove(string name, float defence)
        {
            this.name = name;
            this.defence = defence;
            description = $"Defends all party members for {defence}hp from all incoming attacks for 1 turn.";
            this.hasTarget = false;
        }
        public override bool DoMove(Entity target)
        {
            Console.WriteLine($"{name} is defending all party members for {defence}hp.");
            for (int i = 0; i < Program.heros.Count; i++)
            {
                Program.heros[i].defence += defence;
            }
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
                    if (!newHero.recruited) Program.RecruitHero(newHero);
                    Console.WriteLine($"{newHero.name} was recovered from zuckerberg! {newHero.name} is now a valuable member of the team!");
                    newHero.PrintHeroDescription();
                    if (newHero.HasDefenceErrasingMove())
                    {
                        Program.heros.Remove(newHero);
                        Program.heros.Insert(0, newHero);
                    }
                    Program.enemies.Remove((Enemy)target);
                }
                else
                {
                    Console.WriteLine($"{newHero.name} is corrupted, and died in the process of recovering them!");
                    Program.enemies.Remove((Enemy)target);
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
            description = $"Stuns an opponent and itself.";
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
    public class SecretTechniqueMove : Move
    {
        public SecretTechniqueMove(string name)
        {
            this.name = name;
            heroTarget = true;
            description = $"Select an ally that does not know the secret technique, they get to use a turn 3 times, and this character gets stunned. \"Secret? Glitches all of them.\"";
        }
        public override bool DoMove(Entity target)
        {
            owner.stuned = true;
            for (int i = 0; i < 3; i++)
            {
                target.DoTurn();
            }
            return true;
        }
    }
    public class RandomMove : Move
    {
        public RandomMove(string name)
        {
            this.name = name;
            this.hasTarget = false;
            description = $"Does something random, and unpredictable. Use extreme causion, or reap the rewards.";
        }
        public override bool DoMove(Entity target)
        {
            int roll = RandomUtil.Next(1, 21);
            Console.WriteLine($"You Rolled {roll}.");
            System.Threading.Thread.Sleep(1000);
            Enemy enemy = Program.enemies[RandomUtil.Next(0, Program.enemies.Count)];
            switch (roll)
            {
                case 20:
                    Hero hero = Program.heros[RandomUtil.Next(0, Program.heros.Count)];
                    Console.WriteLine($"In the shadowy recesses of government corridors, a clandestine operation unfolded five years ago," +
                        $" shrouded in treachery and deception.A glitch - ridden table, an unsuspecting artifact of bureaucratic drudgery," +
                        $" harbored a secret so potent it could unravel the very fabric of power.Through a series of improbable events," +
                        $" this malfunctioning furniture, imbued with a mysterious intelligence," +
                        $" penetrated the impenetrable walls of secrecy guarding a coveted technique coveted by governments worldwide.With every glitch," +
                        $" it whispered secrets to those who dared listen, divulging the intricacies of the technique coveted by clandestine organizations.Its journey," +
                        $" fraught with danger and betrayal, saw alliances forged and broken, as it navigated the murky waters of espionage and betrayal.Yet, against all odds," +
                        $" the glitchy table emerged victorious, clutching the coveted knowledge within its digital veins, forever altering the course of history.");
                    System.Threading.Thread.Sleep(7000);
                    Console.WriteLine();
                    Console.WriteLine($"{hero} learned secret technique from {owner.name}!");
                    hero.AddMove(new SecretTechniqueMove("Secret Technique"));
                    break;
                case 19:
                    float dmg = 10f;
                    Console.WriteLine($"{owner.name} attacked all enemies for {dmg}!");
                    for (int i = 0; i < Program.enemies.Count; i++)
                    {
                        Program.enemies[i].TakeDamage(dmg, i != Program.enemies.Count - 1);
                    }
                    float heal = 4f;
                    Console.WriteLine($"{owner.name} healed all team members for {heal}!");
                    for (int i = 0; i < Program.heros.Count; i++)
                    {
                        Program.heros[i].Heal(heal);
                    }
                    break;
                case 18:
                    Console.WriteLine($"{owner.name} cloaned itself!");
                    Hero clone = new Hero(owner.maxHP, owner.name + "Clone", owner.moves);
                    Program.heros.Add(clone);
                    clone.OnSpawn();
                    break;
                case int n when (n == 17 || n == 16):
                    heal = 4f;
                    Console.WriteLine($"{owner.name} healed all team members for {heal}!");
                    for (int i = 0; i < Program.heros.Count; i++)
                    {
                        Program.heros[i].Heal(heal);
                    }
                    break;
                case int n when (n == 15 || n == 14):
                    Console.WriteLine($"{owner.name} stunned {enemy.name}!");
                    enemy.stuned = true;
                    break;
                case int n when (n == 13 || n == 12 || n == 11 || n == 9):
                    int scaledDamge = roll - 3;
                    Console.WriteLine($"{owner.name} attacked {enemy.name} for {scaledDamge}!");
                    enemy.TakeDamage(scaledDamge);
                    break;
                case 10:
                    Console.WriteLine($"{owner.name} ignored your command try again!");
                    return false;
                case int n when (n == 8 || n == 7 || n == 6):
                    Console.WriteLine($"{owner.name} was lazy! (maybe because it's an inanimate object)");
                    break;
                case int n when (n == 5 || n == 4 || n == 3):
                    Console.WriteLine($"{owner.name} glitched out a bit to much! {owner.name} took 5 damage.");
                    owner.TakeDamage(5);
                    break;
                case 2:
                    Console.WriteLine($"{owner.name} glitched through 2 parrellel universes and brought back with it an enemy!");
                    Enemy timeKeeper = new DefendingEnemy(35f, "Time Keeper🛡 ", 11f, 3);
                    Program.enemies.Add(timeKeeper);
                    timeKeeper.OnSpawn();
                    break;
                case 1:
                    Console.WriteLine($"{owner.name} glitched onto the enemy side!");
                    Enemy enemyTable = new Enemy(owner.maxHP, "Table!!!!!", 30f);
                    Program.enemies.Add(enemyTable);
                    enemyTable.OnSpawn();
                    Program.heros.Remove(owner);
                    break;
                default:
                    break;
            }
            return true;
        }
    }
    public class CorruptMove : Move
    {
        public CorruptMove(string name, bool useHeros)
        {
            this.name = name;
            heroTarget = useHeros;
            description = $"select a target, and transend them to understanding.";
        }
        public override bool DoMove(Entity target)
        {
            target.TakeDamage(100f);
            if(target.hp <= 0)
            {
                //Program.RecruitHero(new Hero(target.maxHP * 2, "Corrupted " + target.name));
            }
            return true;
        }
    }
}
