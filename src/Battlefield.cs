using RandomRPG.Campaigns;


namespace RandomRPG.Entities
{
    public static class Battlefield
    {
        public static int turnBattleCycleStartedOn = 1;
        public static bool skipDefaultGenerating;
        public static void PrintField()
        {
            Console.WriteLine(new string('#', RPG.windowWidth));
            PrintSide(RPG.heros.ToEntities());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            PrintSide(RPG.enemies.ToEntities());
            Console.WriteLine(new string('#', RPG.windowWidth));
        }
        public static void PrintSide(List<Entity> side)
        {
            int spacing = RPG.windowWidth / (side.Count + 1);
            string[] names = new string[side.Count];
            ConsoleColor[] colors = new ConsoleColor[side.Count];
            string[] status = new string[side.Count];
            string[][] sideInfo = new string[][] { names, status };
            for (int i = 0; i < side.Count; i++)
            {
                colors[i] = side[i].nameColor;
                names[i] = side[i].name;
                status[i] = side[i].HPStatus();
            }

            PrintEvenColumns(sideInfo, [colors]);

            Console.WriteLine();
        }
        public static void SpawnRandomEncounter(Enemy[][] minibossess)
        {
            Enemy[] miniBoss = minibossess[RandomUtil.Next(0, minibossess.Length)];
            SpawnEncounter(miniBoss);
        }
        public static void SpawnEncounter(Enemy[] enemies)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (!(enemies[i] is RecoverableEnemy recoverableEnemy) || !recoverableEnemy.recoverableHero.recruited) RPG.enemies.Add(enemies[i].Clone());
            }
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                RPG.enemies[i].OnSpawn();
            }
        }
        public static void GenerateEnemies()
        {
            skipDefaultGenerating = false;
            turnBattleCycleStartedOn = RPG.turnNum + 1;
            if (RPG.techCampaign)
            {
                TechContent.Events();
                if (!skipDefaultGenerating) TechContent.GenerateEnemies();
            }
            if (RPG.magicCapaign)
            {
                MagicEvents.Events();
                if (!skipDefaultGenerating) DarkContent.GenerateEnemies();
            }

            if (!skipDefaultGenerating)
            {
                //Always spawn 1 easy, and 1 medium enemy
                RPG.enemies.Add(RPG.possibleEasyEnemies[RandomUtil.Next(0, RPG.possibleEasyEnemies.Length)].Clone());
                RPG.enemies.Add(RPG.possibleMediumEnemies[RandomUtil.Next(0, RPG.possibleMediumEnemies.Length)].Clone());


                //Sometimes spawn an additional enemy past turn 6
                if (RandomUtil.NextDouble() < 0.5f && RPG.turnNum > 6) RPG.enemies.Add(RPG.possibleMediumEnemies[RandomUtil.Next(0, RPG.possibleMediumEnemies.Length)].Clone());

                if (RandomUtil.NextDouble() < 0.5f)
                {
                    //Swarm
                    int swarmEnemyCount = (RPG.turnNum / 8) + 1;
                    if (RPG.turnNum > 20) RPG.enemies.Add(RPG.possibleHardEnemies[RandomUtil.Next(0, RPG.possibleHardEnemies.Length)].Clone());

                    //Magic campaign compresses enemies more
                    if (RPG.magicCapaign && RPG.turnNum > 17)
                    {
                        swarmEnemyCount -= 1;
                        RPG.enemies.Add(RPG.possibleEasyEnemies[RandomUtil.Next(0, RPG.possibleMediumEnemies.Length)].Clone());
                    }

                    for (int i = 0; i < swarmEnemyCount; i++)
                    {
                        RPG.enemies.Add(RPG.possibleEasyEnemies[RandomUtil.Next(0, RPG.possibleEasyEnemies.Length)].Clone());
                    }
                }
                else
                {
                    //Hard enemies

                    //If too early with some variance spawn an extra medium enemy
                    if (RPG.turnNum < (RPG.techCampaign || RPG.magicCapaign ? 17 : 25) + RandomUtil.Next(-6, 7)) RPG.enemies.Add(RPG.possibleMediumEnemies[RandomUtil.Next(0, RPG.possibleMediumEnemies.Length)].Clone());
                    //Then attempt to spawn a hard enemy, or if it rolls even higher, spawn 2.
                    else if (RPG.turnNum < (RPG.techCampaign | RPG.magicCapaign ? 20 : 30) + RandomUtil.Next(-6, 13)) RPG.enemies.Add(RPG.possibleHardEnemies[RandomUtil.Next(0, RPG.possibleHardEnemies.Length)].Clone());
                    else
                    {
                        RPG.enemies.Add(RPG.possibleHardEnemies[RandomUtil.Next(0, RPG.possibleHardEnemies.Length)].Clone());
                        RPG.enemies.Add(RPG.possibleHardEnemies[RandomUtil.Next(0, RPG.possibleHardEnemies.Length)].Clone());
                    }
                }
            }


            InitBattle();
        }
        public static void InitBattle()
        {
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                RPG.enemies[i].OnSpawn();
            }
        }
        public static bool PlayerHasRecoveringMove()
        {
            for (int i = 0; i < RPG.heros.Count; i++)
            {
                if (RPG.heros[i].HasRecoveryMove()) return true;
            }
            return false;
        }
        public static void KillDeadPlayers()
        {
            for (int i = 0; i < RPG.heros.Count; i++)
            {
                if (RPG.heros[i].hp <= 0)
                {
                    Messages.ColoredWriteLine($"{RPG.heros[i].name} Died! Get Your Act Together!", ConsoleColor.Red);
                    RPG.heros[i].OnDeath();
                }
            }
            RPG.heros.RemoveAll(hero => hero.hp <= 0);
        }
        public static void KillDeadEnemies()
        {
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                if (RPG.enemies[i].hp <= 0)
                {
                    if (RPG.enemies[i].hp < -4) Console.WriteLine($"You Obliterated {RPG.enemies[i].name}!");
                    else Console.WriteLine($"You Killed {RPG.enemies[i].name}!");
                    RPG.enemies[i].OnDeath();
                }
            }
            RPG.enemies.RemoveAll(enemy => enemy.hp <= 0);
        }

        //------------------
        //    Display
        //------------------

        static int GetConsoleLength(string s)
        {
            return s.EnumerateRunes().Count();
        }
        public static void PrintEvenColumns(string[][] lists, ConsoleColor[][] colors)
        {
            //Validation
            for (int i = 0; i < lists.Length - 1; i++)
            {
                if (lists[i].Length != lists[i + 1].Length)
                {
                    throw new ApplicationException("PrintEvenColumns: lists must all be of the same length");
                }
            }
            int columnCount = lists[0].Length;
            if (columnCount == 0) { Console.WriteLine(); return; }

            //Get Required Widths
            int totalWidth = RPG.windowWidth;
            int[] columnRequiredWidths = new int[lists[0].Length];
            for (int column = 0; column < columnCount; column++)
            {
                for (int i = 0; i < lists.Length; i++)
                {
                    columnRequiredWidths[column] = Math.Max(columnRequiredWidths[column], GetConsoleLength(lists[i][column]));
                }
            }
            int totalTextWidth = columnRequiredWidths.Sum();
            int remainingSpace = Math.Max(0, totalWidth - totalTextWidth);

            string paddingText = new string(' ', remainingSpace / (columnCount + 1));

            for (int i = 0; i < lists.Length; i++)
            {
                Console.Write(paddingText);
                for (int column = 0; column < columnCount; column++)
                {
                    if (i < colors.Length)
                    {
                        Console.ForegroundColor = colors[i][column];
                    }
                    Console.Write(lists[i][column]);
                    Console.ResetColor();


                    Console.Write(new string(' ', columnRequiredWidths[column] - GetConsoleLength(lists[i][column])));
                    Console.Write(paddingText);
                }
                Console.WriteLine();
            }
        }
    }
}
