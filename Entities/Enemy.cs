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
            if(!waitToKill) Battle.KillDeadEnemies();
        }
        public override string HPStatus()
        {
            return base.HPStatus() + $" {dmg}";
        }
        public override void DoTurn()
        {
            int pickedPlayer = RandomUtil.Next(0, RPG.heros.Count);
            Console.WriteLine($"{name} attacked {RPG.heros[pickedPlayer].name} for {RPG.heros[pickedPlayer].GetDamageDelt(dmg)}");
            RPG.heros[pickedPlayer].TakeDamage(dmg);
        }

        public virtual Enemy Clone()
        {
            return new Enemy(maxHP, name, dmg);
        }
    }
}
