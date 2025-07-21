using RandomRPG.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG.Campaigns
{
    public static class MagicEvents
    {
        public static void Events()
        {
            if(RPG.turnNum > 22)
            {
                return;
            }
            int rand = RandomUtil.Next(0, 10);
            if (rand < 6) Console.WriteLine();
            if (MagicContent.lampOil <= 5)
            {
                if (rand < 2) rand = 0;
                else if (rand < 4) rand = 4;
                else rand = 5;
            }
            switch (rand)
            {
                case 0:
                    SpiderEvent();
                    break;
                case 1:
                    BlackHoleEvent();
                    break;
                case 2:
                case 3:
                    FruitPunchEvent();
                    break;
                case 4:
                    AntiSpaghettiSquadEvent();
                    break;
                case 5:
                    HouseInTheVoidEvent();
                    break;
                case 6:
                    AntBrosEvent();
                    break;
                default:
                    break;
            }
        }
        public static void AntBrosEvent()
        {
            Battle.skipDefaultGenerating = true;
            RPG.enemies.Add(new Enemy(2f, "Ant Bro", 6f));
            RPG.enemies.Add(new Enemy(2f, "Ant Bro", 6f));
            RPG.enemies.Add(new Enemy(2f, "Ant Bro", 6f));
            RPG.enemies.Add(new Enemy(2f, "Ant Bro", 6f));
            RPG.enemies.Add(new Enemy(2f, "Ant Bro", 6f));
        }
        public static void FruitPunchEvent()
        {
            Console.WriteLine("You spot a lonley fruit punch sitting in the void.");
            Console.WriteLine("It has probably been there for days.");
            if (RPG.GetUserYN("Drink it?"))
            {
                Console.WriteLine("Who should drink it?");
                int pickedHero = RPG.HeroOption(RPG.heros.ToArray());
                if (RandomUtil.NextBool(1, 3))
                {
                    Console.WriteLine($"{RPG.heros[pickedHero].name} put their mouth to the fruit punch to drink it, but then it smacked them in the face.");
                    RPG.heros[pickedHero].TakeDamage(7);
                    RPG.WaitForUser();
                    Battle.skipDefaultGenerating = true;
                    RPG.enemies.Add(new Enemy(35, "Living Fruit Punch", 10));
                }
                else if (RandomUtil.NextBool(1, 3))
                {
                    Console.WriteLine("The fruit punch was not fruit punch...");
                    Console.WriteLine($"{RPG.heros[pickedHero].name} took 5 damage, and was stunned.");
                    RPG.heros[pickedHero].TakeDamage(5);
                    RPG.heros[pickedHero].stuned = true;
                    RPG.WaitForUser();
                }
                else
                {
                    Console.WriteLine($"{RPG.heros[pickedHero].name} was refreshed by the fruit punch");
                    Console.WriteLine($"{RPG.heros[pickedHero].name} gained 15 Max HP");
                    RPG.heros[pickedHero].maxHP += 15;
                    RPG.heros[pickedHero].Heal(15);
                    RPG.WaitForUser();
                }
            }
        }
        public static void SpiderEvent()
        {
            Console.WriteLine("You find a village of dark figures slowly crawling around in the dark.");
            Console.WriteLine("One comes up to your crew");
            RPG.WaitForUser();
            Console.WriteLine("We desperatly crave... fooooooood.");
            Console.WriteLine("The figure gesters to some lamp oil behind them.");
            if (RPG.GetUserYN("Offer a sacrifice?"))
            {
                int heroNum = RPG.HeroOption(RPG.heros.ToArray());
                Console.WriteLine("Thank you... looks like my children will be eating well tonight");
                while (RPG.heros[heroNum].hp > 0)
                {
                    RPG.heros[heroNum].TakeDamage(1, true);
                    System.Threading.Thread.Sleep(30);
                    RPG.heros[heroNum].TakeDamage(1, true);
                    System.Threading.Thread.Sleep(30);
                    RPG.heros[heroNum].TakeDamage(1, true);
                    System.Threading.Thread.Sleep(30);
                    RPG.heros[heroNum].TakeDamage(9, true);
                    System.Threading.Thread.Sleep(300);
                }
                Battle.KillDeadPlayers();
                MagicContent.lampOil += 12;
            }
            else
            {
                Battle.skipDefaultGenerating = true;
                RPG.enemies.Add(new Enemy(35, "Spider", 11));
                RPG.enemies.Add(new Enemy(5, "Baby Spider", 1));
                RPG.enemies.Add(new Enemy(5, "Baby Spider", 1));
                RPG.enemies.Add(new Enemy(5, "Baby Spider", 1));
            }
        }
        public static void HouseInTheVoidEvent()
        {
            Console.WriteLine("A suburban house lights up in the distance.");
            Console.WriteLine("As you get closer you hear music coming from the inside.");
            if (RPG.GetUserYN("A man greets you at the enterance. Hey we are selling lamp oil, want some? I'll trade you for some work. (It will take 5 turns)"))
            {
                RPG.turnNum += 5;
                MagicContent.lampOil += 10;
            }
        }
        public static void AntiSpaghettiSquadEvent()
        {
            Console.WriteLine("People swing down from ropes above surrounding you.");
            Console.WriteLine("A little gremlin steps out of the shadows, it looks like he wants to talk.");
            if (RPG.GetUserYN("Hear them out? (They look pretty dangerous)"))
            {
                Console.WriteLine("We need your help to beat the spaghetti monsters.");
                Console.WriteLine("They have overcome 'Japan', and we need your help reclaiming it from them. It would be a difficult battle, but we would fight with eachother side by side.");
                if (RPG.GetUserYN("Will you join us in battle?"))
                {
                    Battle.skipDefaultGenerating = true;
                    MagicContent.lampOil += 25;
                    RPG.enemies.Add(new TimeBomb(50, "Spaghetti God", 13, 7));
                    RPG.enemies.Add(new DefendingEnemy(15, "Spaghettius MK.2🛡", 8, 7));
                    RPG.enemies.Add(new Enemy(20, "Meatballer", 9));
                    RPG.enemies.Add(new Enemy(13, "Spagettera", 10));
                    RPG.enemies.Add(new Enemy(6, "Noodles", 4));
                    RPG.enemies.Add(new Enemy(6, "Noodler", 6));
                    RPG.enemies.Add(new Enemy(6, "Noodlest", 4));
                    RPG.heros.Add(new Hero(15, "Gremlin", new Move[] { new WeakenMove("Pee on them"), new StunningMove("Slam em'", 9, 0.3f) }));
                    RPG.heros[RPG.heros.Count - 1].OnSpawn();
                    RPG.heros.Add(new Hero(20f, "Fruit Ninja", new Move[] { new AllTargetsDamage("Slice", 4f) }));
                    RPG.heros[RPG.heros.Count - 1].OnSpawn();
                    RPG.heros.Add(new Hero(25f, "Ninja #132", new Move[] { new StealthMove("Stealh Strike", 14f), new AllTargetsDamage("Ninja Slice", 8f), new DefendMove("Hide", 10) }));
                    RPG.heros[RPG.heros.Count - 1].OnSpawn();
                    RPG.heros.Add(new Hero(35f, "Golem", new Move[] { new TargetedDefendMove("Golem Block", 8), new StunningMove("Big Fat Punch", 4f, 0.2f) }));
                    RPG.heros[RPG.heros.Count - 1].OnSpawn();
                }
                else
                {
                    Console.WriteLine("We wish you best of luck on your travels.");
                    if (MagicContent.lampOil <= 5)
                    {
                        Console.WriteLine("Here is some lamp oil, I see you are struggling for it");
                        Console.WriteLine("Gained 5 lamp oil");
                        MagicContent.lampOil += 5;
                    }
                    RPG.WaitForUser();
                }
            }
            else
            {
                Battle.skipDefaultGenerating = true;
                RPG.enemies.Add(new Enemy(9, "Gremlin", 11));
                RPG.enemies.Add(new Enemy(6, "Fruit Ninja", 6));
                RPG.enemies.Add(new Enemy(5, "Ninja #132", 5));
                RPG.enemies.Add(new DefendingEnemy(16, "Golem🛡", 6, 4));
            }
        }
        public static void BlackHoleEvent()
        {
            Console.WriteLine("You stumble upon a black hole.");
            Console.WriteLine("Don't question it.");
            RPG.WaitForUser();
            if (RPG.GetUserYN("Touch it?"))
            {
                if (RPG.GetUserYN("Are you sure?"))
                {
                    if (RPG.GetUserYN("Are you sure you are sure?"))
                    {
                        RPG.heros.Add(new Hero(20f, "Black Hole", new Move[] { new GuiotineMove("Suck in", 4f) }));
                        RPG.heros[RPG.heros.Count - 1].OnSpawn();
                    }
                }
            }
        }
    }
}
