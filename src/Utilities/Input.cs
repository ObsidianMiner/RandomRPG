using RandomRPG.Entities;

namespace RandomRPG
{
    public static class Input
    {
        public static void WaitForUser()
        {
            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();
            Console.WriteLine("");
            Console.WriteLine("");
        }
        public static bool GetUserYN(string question)
        {
            Console.WriteLine(question + " (y/n)");
            bool answered = false;
            bool answer = false;
            while (!answered)
            {
                ConsoleKey key = Console.ReadKey().Key;
                Console.WriteLine();
                if (key == ConsoleKey.Y)
                {
                    answer = true;
                    answered = true;
                }
                else if (key == ConsoleKey.N)
                {
                    answer = false;
                    answered = true;
                }
                else
                {
                    Console.WriteLine(question + " (y/n)");
                }
            }
            return answer;
        }
        public static string Options(string question, string[] keys, string[] options)
        {
            Console.WriteLine(question);
            string key = "";
            while (true)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"{keys[i]}.{options[i]}");
                }
                key = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine();

                if (keys.Contains(key))
                {
                    return key;
                }
                else
                {
                    Console.WriteLine(question);
                }
            }
        }
        public static int HeroOption(Hero[] options)
        {
            int selected = -1;
            while (selected == -1)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"{i}.{options[i].name}");
                }
                if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int sel))
                {
                    selected = sel;
                }
                Console.WriteLine();
            }
            return selected;
        }
    }
}
