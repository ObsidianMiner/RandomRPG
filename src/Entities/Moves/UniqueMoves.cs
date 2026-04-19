namespace RandomRPG.Entities
{
    public class GlowMove : Move
    {
        public GlowMove(string name)
        {
            this.name = name;
            hasTarget = false;
            description = $"Gain 2 lamp oil, stuns the user.";
        }
        public override bool DoMove(Entity target)
        {
            owner.stuned = true;
            MagicContent.lampOil += 2;
            return true;
        }
    }
    public class ConvertMove : Move
    {
        public ConvertMove(string name)
        {
            this.name = name;
            hasTarget = true;
            description = $"Turn an enemy that is stunned into a {name}.";
        }
        public override bool DoMove(Entity target)
        {
            if (target.stuned)
            {
                target.name = name;
                int index = RPG.enemies.IndexOf(target as Enemy);
                float maxHealth = target.maxHP;
                float health = target.hp;
                RPG.enemies.Remove(target as Enemy);
                RPG.enemies.Insert(index, new Enemy(maxHealth, name, 0f));
                RPG.enemies[index].SetHP(health);
            }
            return true;
        }
    }
    public class SecretTechniqueMove : Move
    {
        bool hasDownside;
        public SecretTechniqueMove(string name, bool hasDownside = true)
        {
            this.name = name;
            this.hasDownside = hasDownside;
            heroTarget = true;
            description = $"Select an ally that does not know the secret technique, they get to use a turn 3 times" + (hasDownside ? ", and this character gets stunned." : ".");
        }
        public override bool DoMove(Entity target)
        {
            if (hasDownside) owner.stuned = true;
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
            Enemy enemy = RPG.enemies[RandomUtil.Next(0, RPG.enemies.Count)];
            switch (roll)
            {
                case 20:
                    Hero hero = RPG.heros[RandomUtil.Next(0, RPG.heros.Count)];
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
                    for (int i = 0; i < RPG.enemies.Count; i++)
                    {
                        RPG.enemies[i].TakeDamage(dmg, i != RPG.enemies.Count - 1);
                    }
                    float heal = 4f;
                    Console.WriteLine($"{owner.name} healed all team members for {heal}!");
                    for (int i = 0; i < RPG.heros.Count; i++)
                    {
                        RPG.heros[i].Heal(heal);
                    }
                    break;
                case 18:
                    Console.WriteLine($"{owner.name} cloaned itself!");
                    Hero clone = new Hero(owner.maxHP, owner.name + "Clone", owner.moves);
                    RPG.heros.Add(clone);
                    clone.OnSpawn();
                    break;
                case int n when (n == 17 || n == 16):
                    heal = 7f;
                    Console.WriteLine($"{owner.name} healed all team members for {heal}!");
                    for (int i = 0; i < RPG.heros.Count; i++)
                    {
                        RPG.heros[i].Heal(heal);
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
                    RPG.enemies.Add(timeKeeper);
                    timeKeeper.OnSpawn();
                    break;
                case 1:
                    Console.WriteLine($"{owner.name} glitched onto the enemy side!");
                    Enemy enemyTable = new Enemy(owner.maxHP, "Table!!!!!", 30f);
                    RPG.enemies.Add(enemyTable);
                    enemyTable.OnSpawn();
                    RPG.heros.Remove(owner);
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
            if (target.hp <= 0)
            {
                //Program.RecruitHero(new Hero(target.maxHP * 2, "Corrupted " + target.name));
            }
            return true;
        }
    }
    public class StunAllMove : Move
    {
        public int uses;
        public StunAllMove(string name, int uses)
        {
            this.name = name;
            this.uses = uses;
            this.hasTarget = false;
            description = $"Stun ALL enemies. Can be used a max of {uses} time(s).";
        }
        public override bool DoMove(Entity target)
        {
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                RPG.enemies[i].stuned = true;
            }
            return true;
        }
    }
    public class AllTargetsIncludingHerosDamage : AllTargetsDamage
    {
        public AllTargetsIncludingHerosDamage(string name, float damage) : base(name, damage)
        {
            description = $"Does {dmg} to all targets INCLUDING HEROS\n" +
                $"very usefull if stabalized";
        }
        public override bool DoMove(Entity target)
        {
            if (!base.DoMove(target)) return false;
            for (int i = 0; i < RPG.heros.Count; i++)
            {
                RPG.heros[i].TakeDamage(dmg, true);
            }
            return true;
        }
    }
}
