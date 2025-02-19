using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG.Entities
{
    public class Boss : Enemy
    {
        public static Enemy[] summons = new Enemy[]
        {
            new Enemy(30f, "The IRS!!!", 13f),
            new Enemy(25f, "Joe Biden!!", 12f),
            new Enemy(22f, "FBI!!!", 19f),
            new Enemy(35f, "Homeland Security!!!!", 20f),
            new TimeBomb(15f, "Balistic Missile💀", 9f, 2),
            new Enemy(20f, "CIA!!!", 13f)
        };
        int turnNum;
        public Boss(float hp, string name, float dmg) : base(hp, name, dmg)
        {

        }
        void Summon(Enemy summon)
        {
            Console.WriteLine($"Prepare yourself, The Government sends {summon.name}.");
            summon.OnSpawn();
            Program.enemies.Add(summon);
            summon.PrintStatus();
            Program.enemies[1].PrintStatus();
        }
        public override void DoTurn()
        {
            if(turnNum == 0)
            {
                Summon(summons[0]);
                Summon(summons[1]);
            }
            else if(turnNum == 1)
            {
                Summon(summons[2]);
                Summon(summons[3]);
            }
            else if (turnNum == 3)
            {
                Summon(summons[4]);
                Summon(Program.possibleEasyEnemies[RandomUtil.Next(0, Program.possibleEasyEnemies.Length)]);
            }
            else if (turnNum == 4)
            {
                Summon(summons[5]);
                Summon(Program.possibleEasyEnemies[RandomUtil.Next(0, Program.possibleEasyEnemies.Length)]);
            }
            else
            {
                Summon(Program.possibleMediumEnemies[RandomUtil.Next(0, Program.possibleMediumEnemies.Length)]);
            }
            turnNum++;
        }
    }
}
