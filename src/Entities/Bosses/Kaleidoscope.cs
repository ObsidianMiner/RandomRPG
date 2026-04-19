namespace RandomRPG.Entities
{
    public class Kaleidoscope : Enemy
    {
        int turnNum = 0;
        public Kaleidoscope(float hp, string name, float dmg) : base(hp, name, dmg)
        {
            Messages.ColoredWriteLine("Behold Kaleidoscope, the absorber of light, and your holy end.", ConsoleColor.Yellow);
            Messages.ColoredWriteLine("✨ enemies will disappear after dealing damage", ConsoleColor.Yellow);
        }
        void Summon(TemporaryEnemy summon)
        {
            Messages.ColoredWriteLine($"The Kaleidoscope rotates to reveal {summon.name}.", summon.nameColor);
            summon.OnSpawn();
            summon.stuned = true;
            RPG.enemies.Add(summon);
            summon.PrintStatus();
        }
        public override void DoTurn()
        {
            base.DoTurn();
            switch (turnNum % 3)
            {
                case 0:
                    nameColor = ConsoleColor.Red;
                    Summon(new TemporaryEnemy(32f, "Queen Bee", 21f, ConsoleColor.Yellow));
                    Summon(new TemporaryEnemy(13f, "Plantera", 22f, ConsoleColor.Green));
                    Summon(new TemporaryEnemy(15f, "Moon Lord", 30f, ConsoleColor.Cyan));
                    Summon(new TemporaryEnemy(11f, "Duke Fishron", 8f, ConsoleColor.Blue));
                    break;
                case 1:
                    nameColor = ConsoleColor.Magenta;
                    Summon(new TemporaryEnemy(16f, "Crimson Mesenger", 28f, ConsoleColor.Red));
                    Summon(new TemporaryEnemy(4f, "Burning Trunker", 32f, ConsoleColor.Yellow));
                    break;
                case 2:
                    nameColor = ConsoleColor.Green;
                    Summon(new TemporaryEnemy(11f, "Wandering Vangerant", 28f, ConsoleColor.Cyan));
                    Summon(new TemporaryEnemy(40f, "Overloading Worm", 40f, ConsoleColor.Blue));
                    Summon(new TemporaryEnemy(12f, "Void Devistator", 40f, ConsoleColor.Magenta));
                    break;
                default:
                    break;
            }
            turnNum++;
        }
    }
}
