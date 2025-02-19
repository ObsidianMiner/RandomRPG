using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG.Entities
{
    public class Hero : Entity
    {
        public Dictionary<Move, string> moveNames = new Dictionary<Move, string>();
        public Move[] moves;
        public Hero upgrade;
        public bool recruited = false;
        public Hero(float hp, string name, Move[] moves, Hero upgrade = null)
        {
            this.maxHP = hp;
            this.name = name;
            this.moves = moves;
            this.upgrade = upgrade;
        }
        public void Upgrade()
        {
            maxHP = upgrade.maxHP;
            hp = maxHP;
            name = upgrade.name;
            moves = upgrade.moves;
            upgrade = upgrade.upgrade;
        }
        public override void OnSpawn()
        {
            base.OnSpawn();
            recruited = true;
            for (int i = 0; i < moves.Length; i++)
            {
                moves[i].owner = this;
            }
        }
        public override void TakeDamage(float damage, bool waitToKill = false)
        {
            base.TakeDamage(damage, waitToKill);
            if (!waitToKill) Program.KillDeadPlayers();
        }
        public override void DoTurn()
        {
            Console.WriteLine($"Pick a move for {name} to use.");
            bool moveDone = false;
            while (!moveDone)
            {
                moveDone = TryPickMove();
            }
        }
        bool TryPickMove()
        {
            for (int i = 0; i < moves.Length; i++)
            {
                Console.WriteLine($"{i}.{moves[i].name}");
            }
            if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out int pickedMove)) return false;
            if (pickedMove >= 0 && pickedMove < moves.Length)
            {
                if (moves[pickedMove].hasTarget) return TryPickTarget(pickedMove, moves[pickedMove].heroTarget ? Program.heros.ToEntities() : Program.enemies.ToEntities());
                else return moves[pickedMove].DoMove(null);
            }
            return false;
        }
        bool TryPickTarget(int pickedMove, List<Entity> side)
        {
            Console.WriteLine($"Pick a target to use {moves[pickedMove].name} on");
            for (int i = 0; i < side.Count; i++)
            {
                Console.WriteLine($"{i}.{side[i].name}");
            }
            if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out int pickedTarget)) return false;
            if (pickedTarget >= 0 && pickedTarget < side.Count)
            {
                return moves[pickedMove].DoMove(side[pickedTarget]);
            }
            return false;
        }
        public void AddMove(Move move)
        {
            moves.Append(move);
        }
        public void PrintHeroDescription()
        {
            Console.WriteLine($"{name}:");
            for (int i = 0; i < moves.Length; i++)
            {
                Console.WriteLine($"{moves[i].name}: {moves[i].description}");
            }
        }
        public bool HasRecoveryMove()
        {
            for (int i = 0; i < moves.Length; i++)
            {
                if (moves[i] is RecoveringMove) return true;
            }
            return false;
        }
        public bool HasDefenceErrasingMove()
        {
            for (int i = 0; i < moves.Length; i++)
            {
                if (moves[i] is DefenceErrasingMove) return true;
            }
            for (int k = 0; k < moves.Length; k++)
            {

            }
            foreach (Move move in moves)
            {
                moveNames.Add(move, "JKELSAJDFLKSJD");
            }
            return false;
        }
    }
}
