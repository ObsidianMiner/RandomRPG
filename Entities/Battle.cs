using RandomRPG.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace RandomRPG.Entities
{
    public static class Battle
    {
        public static int battleStartTurn = 1;
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
            string[] status = new string[side.Count];
            string[][] sideInfo = new string[][] { names, status };
            for (int i = 0; i < side.Count; i++)
            {
                names[i] = side[i].name;
                status[i] = side[i].HPStatus();
            }
            string[] rows = EvenColumns(spacing, sideInfo);
            for (int i = 0; i < rows.Length; i++)
            {
                Console.WriteLine(rows[i].PadLeft(rows[i].Length + spacing));
            }

            Console.WriteLine();
        }
        public static void SpawnRandomEncounter(Enemy[][] minibossess)
        {
            Enemy[] miniBoss = minibossess[RandomUtil.Next(0, minibossess.Length)];
        }
        public static void SpawnEncounter(Enemy[] enemies)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (!(enemies[i] is RecoverableEnemy recoverableEnemy) || !recoverableEnemy.recoverableHero.recruited) RPG.enemies.Add(miniBoss[i].Clone());
            }
            for (int i = 0; i < RPG.enemies.Count; i++)
            {
                RPG.enemies[i].OnSpawn();
            }
        }
        public static void GenerateEnemies()
        {
            skipDefaultGenerating = false;
            battleStartTurn = RPG.turnNum + 1;
            if (RPG.techCampaign)
            {
                TechContent.Events();
                if(!skipDefaultGenerating) TechContent.GenerateEnemies();
            }
            if(RPG.magicCapaign)
            {
                MagicEvents.Events();
                if (!skipDefaultGenerating) MagicContent.GenerateEnemies();
            }

            if (!skipDefaultGenerating)
            {
                RPG.enemies.Add(RPG.possibleEasyEnemies[RandomUtil.Next(0, RPG.possibleEasyEnemies.Length)].Clone());
                RPG.enemies.Add(RPG.possibleMediumEnemies[RandomUtil.Next(0, RPG.possibleMediumEnemies.Length)].Clone());
                if (RandomUtil.NextDouble() < 0.5f && RPG.turnNum > 6) RPG.enemies.Add(RPG.possibleMediumEnemies[RandomUtil.Next(0, RPG.possibleMediumEnemies.Length)].Clone());
                if (RandomUtil.NextDouble() < 0.5f)
                {
                    int swarmEnemyCount = (RPG.turnNum / 8) + 1;
                    if (RPG.turnNum > 20) RPG.enemies.Add(RPG.possibleHardEnemies[RandomUtil.Next(0, RPG.possibleHardEnemies.Length)].Clone());
                    for (int i = 0; i < swarmEnemyCount; i++)
                    {
                        RPG.enemies.Add(RPG.possibleEasyEnemies[RandomUtil.Next(0, RPG.possibleEasyEnemies.Length)].Clone());
                    }
                }
                else
                {
                    if (RPG.turnNum < (RPG.techCampaign ? 18 : 26) + RandomUtil.Next(-6, 10)) RPG.enemies.Add(RPG.possibleMediumEnemies[RandomUtil.Next(0, RPG.possibleMediumEnemies.Length)].Clone());
                    else if (RPG.turnNum < (RPG.techCampaign || RPG.magicCapaign ? 20 : 30) + RandomUtil.Next(-6, 20)) RPG.enemies.Add(RPG.possibleHardEnemies[RandomUtil.Next(0, RPG.possibleHardEnemies.Length)].Clone());
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
                    Console.WriteLine($"{RPG.heros[i].name} Died! Get Your Act Together!");
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
                }
            }
            RPG.enemies.RemoveAll(enemy => enemy.hp <= 0);
        }
        public static string[] EvenColumns(int desiredWidth, IEnumerable<IEnumerable<string>> lists)
        {
            return EvenColumns(desiredWidth, true, lists).ToArray();
        }

        public static IEnumerable<string> EvenColumns(int desiredWidth, bool rightOrLeft, IEnumerable<IEnumerable<string>> lists)
        {
            return lists.Select(o => EvenColumns(desiredWidth, rightOrLeft, o.ToArray()));
        }

        public static string EvenColumns(int desiredWidth, bool rightOrLeftAlignment, string[] list, bool fitToItems = false)
        {
            // right alignment needs "-X" 'width' vs left alignment which is just "X" in the `string.Format` format string
            int columnWidth = (rightOrLeftAlignment ? -1 : 1) *
                                // fit to actual items? this could screw up "evenness" if
                                // one column is longer than the others
                                // and you use this with multiple rows
                                (fitToItems
                                    ? Math.Max(desiredWidth, list.Select(o => o.Length).Max())
                                    : desiredWidth
                                );
            if (list.Length == 0) return "";
            // make columns for all but the "last" (or first) one
            string format = string.Concat(Enumerable.Range(rightOrLeftAlignment ? 0 : 1, list.Length - 1).Select(i => string.Format("{{{0},{1}}}", i, columnWidth)));

            // then add the "last" one without Alignment
            if (rightOrLeftAlignment)
            {
                format += "{" + (list.Length - 1) + "}";
            }
            else
            {
                format = "{0}" + format;
            }

            return string.Format(format, list);
        }
    }
}
