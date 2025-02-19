using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG.Entities
{
    public abstract class Entity
    {
        public float maxHP;
        public float hp { get; protected set; }
        public string name { get; protected set; }
        public float defence = 0;
        public bool stuned;
        public bool stunImmune;
        public MultiplierList damageTakenMult = MultiplierList.empty;
        public virtual void OnSpawn()
        {
            hp = maxHP;
        }

        public virtual void TakeDamage(float damage, bool waitToKill = false)
        {
            hp -= Math.Max((damage * damageTakenMult) - defence, 0);
        }
        public virtual float GetDamageDelt(float damage) => Math.Max((damage * damageTakenMult) - defence, 0);
        public virtual void Heal(float health)
        {
            hp += health;
            if (hp > maxHP) hp = maxHP;
        }
        public virtual void PrintStatus()
        {
            Console.WriteLine($"{name}'s HP is {hp}/{maxHP}");
        }
        public virtual string HPStatus()
        {
            return $"{hp}/{maxHP}{(stunImmune ? "✱" : "") + (stuned && !stunImmune ? "*" : string.Empty)} {(defence > 0 ? "🛡 " + defence : string.Empty)}";
        }
        public virtual void DoTurn()
        {

        }
        public virtual void TryTurn()
        {
            if (stuned && !stunImmune)
            {
                Console.WriteLine($"{name} is stunned!");
                stuned = false;
            }
            else
            {
                DoTurn();
            }
        }
    }
}
