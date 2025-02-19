using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG.Entities
{
    public class Enemy : Entity
    {
        public float dmg;
        public Enemy(float hp, string name, float dmg)
        {
            this.maxHP = hp;
            this.name = name;
            this.dmg = dmg;
        }
        public override void TakeDamage(float damage, bool waitToKill = false)
        {
            base.TakeDamage(damage, waitToKill);
            if(!waitToKill) Program.KillDeadEnemies();
        }
        public override void DoTurn()
        {
            int pickedPlayer = RandomUtil.Next(0, Program.heros.Count);
            Console.WriteLine($"{name} attacked {Program.heros[pickedPlayer].name} for {Program.heros[pickedPlayer].GetDamageDelt(dmg)}");
            Program.heros[pickedPlayer].TakeDamage(dmg);
        }

        public virtual Enemy Clone()
        {
            return new Enemy(maxHP, name, dmg);
        }
    }
}
