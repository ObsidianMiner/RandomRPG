using RandomRPG.Entities;

namespace RandomRPG
{
    public static class Camp
    {
        public static void VistCamp()
        {
            Messages.CampMessage();

            int firstRecruit = FindNewRecruit(-1);
            int secondRecruit = FindNewRecruit(firstRecruit);
            int upgrade = FindUpgrade();
            string upgradingCharacterName = "";
            if (upgrade >= 0) upgradingCharacterName = RPG.heros[upgrade].name; //Used to fix reordering to change the upgrade.

            bool gotAid = false;
            while (!gotAid)
            {
                if (upgrade > -1) upgrade = RPG.heros.FindIndex(h => h.name == upgradingCharacterName);
                Console.WriteLine("Wellcome back to the cozy campsite. Your heros have each healed by 10. There are recruits waiting pick one.");

                Console.WriteLine(RPG.tips[Math.Min(RPG.waveNum - 1, RPG.tips.Length - 1)]);
                Console.WriteLine("Recruits at camp:");
                Console.WriteLine();
                if (firstRecruit != -1)
                {
                    Console.Write("0.");
                    RPG.possibleHeros[firstRecruit].PrintHeroDescription();
                    Console.WriteLine();
                }
                if (secondRecruit != -1)
                {
                    Console.Write("1.");
                    RPG.possibleHeros[secondRecruit].PrintHeroDescription();
                    Console.WriteLine();
                }


                if (RPG.waveNum <= 3) Console.WriteLine("Upgrading Characters will be unlcoked at wave 4.");
                else
                {
                    if (upgrade != -1)
                    {
                        Console.WriteLine("Upgrade of the day:");
                        Console.WriteLine($"Upgrade {RPG.heros[upgrade].name} to");
                        Console.Write("2.");
                        RPG.heros[upgrade].upgrade.PrintHeroDescription();
                        Console.WriteLine();
                    }
                }

                Console.WriteLine("o.");
                Console.WriteLine("Reorder party.");
                Console.WriteLine();

                string key = Console.ReadKey().KeyChar.ToString();

                if (key == "0")
                {
                    RPG.RecruitHero(RPG.possibleHeros[firstRecruit]);
                    gotAid = true;
                }
                else if (key == "1")
                {
                    RPG.RecruitHero(RPG.possibleHeros[secondRecruit]);
                    gotAid = true;
                }
                else if (key == "2")
                {
                    RPG.heros[upgrade].Upgrade();
                    gotAid = true;
                }
                else if (key == "o")
                {
                    ReOrderParty();
                }
            }
            for (int i = 0; i < RPG.heros.Count; i++)
            {
                RPG.heros[i].Heal(10f);
            }
        }
        public static void ReOrderParty()
        {
            Console.WriteLine("Select each character in the order you want them");
            List<Hero> orderedHeros = new List<Hero>();
            while (RPG.heros.Count > 0)
            {
                for (int i = 0; i < RPG.heros.Count; i++)
                {
                    Console.WriteLine($"{i}. {RPG.heros[i].name}");
                }
                if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int indexPicked) && indexPicked >= 0 && indexPicked < RPG.heros.Count)
                {
                    orderedHeros.Add(RPG.heros[indexPicked]);
                    RPG.heros.RemoveAt(indexPicked);
                }
                Console.WriteLine("");
            }
            RPG.heros = orderedHeros;
        }
        static int FindNewRecruit(int alreadyInSelection)
        {
            int attempts = 0;
            while (attempts < 900)
            {
                attempts++;
                int recruit = RandomUtil.Next(0, RPG.possibleHeros.Length);
                if (!RPG.possibleHeros[recruit].recruited && recruit != alreadyInSelection) return recruit;
            }
            return -1;
        }
        static int FindUpgrade()
        {
            int attempts = 0;
            while (attempts < 900)
            {
                attempts++;
                int upgradeable = RandomUtil.Next(0, RPG.heros.Count);
                if (RPG.heros[upgradeable].upgrade != null) return upgradeable;
            }
            return -1;
        }
    }
}
