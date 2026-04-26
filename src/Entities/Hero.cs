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
            OnSpawn();
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
            if (!waitToKill) Battlefield.KillDeadPlayers();
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
            int selectedMove = Input.TryOptions("", moves.Select(m => m.name).ToArray(), true);

            if (selectedMove == -1) return false;

            const int showInfoIndex = -2;
            if (selectedMove == showInfoIndex)
            {
                PrintHeroDescription();
                return false;
            }
            if (moves[selectedMove].hasTarget) return TryPickTarget(selectedMove, moves[selectedMove].heroTarget ? RPG.heros.ToEntities() : RPG.enemies.ToEntities());
            else return moves[selectedMove].DoMove(null);
        }
        bool TryPickTarget(int pickedMove, List<Entity> side)
        {
            int selectedTarget = Input.TryOptions($"Pick a target to use {moves[pickedMove].name} on", side.Select(e => e.name).ToArray());
            if (selectedTarget == -1) return false;

            return moves[pickedMove].DoMove(side[selectedTarget]);
        }
        public void AddMove(Move move)
        {
            moves.Append(move);
        }
        public void PrintHeroDescription()
        {
            Console.WriteLine($"{name}:");
            Console.WriteLine($"\tMax HP {maxHP}");
            for (int i = 0; i < moves.Length; i++)
            {
                Console.WriteLine($"\t{moves[i].name}: {moves[i].description}");
            }
            Console.WriteLine();
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
            return false;
        }
        public void IncreaseMaxHP(int increase)
        {
            hp += increase;
            maxHP += increase;
            if (upgrade != null) upgrade.maxHP += increase;
        }
        public Hero Clone()
        {
            return new Hero(maxHP, name, moves, upgrade);
        }
    }
}
