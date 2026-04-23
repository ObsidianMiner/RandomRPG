using RandomRPG.Entities;

namespace RandomRPG
{
    public static class Input
    {
        public static readonly string[] keys =
        {
            "1","2","3","4","5","6","7","8","9","0",
            "q","w","e","r","t","y","u","o","p",
            "a","s","d","f","g","h","j","k","l",
            "z","x","c","v","b","n","m"
        };
        public static void WaitForUser()
        {
            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
        }
        /// <summary>
        /// Cannot be backed out of.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Asks the user for an option and returns -1 if it is invalid.<br></br>
        /// Can be used when any other input acts as a backout. Otherwise use <c>Options</c>.
        /// </summary>
        /// <param name="question"></param>
        /// <param name="options"></param>
        /// <param name="showMoreInfoOption">If true returns -2 if the more info option is selected.</param>
        /// <returns></returns>
        public static int TryOptions(string question, string[] options, bool showMoreInfoOption = false)
        {
            if (question != null && question.Length > 0) Console.WriteLine(question);
            Console.WriteLine();

            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"\t{keys[i]}.{options[i]}");
            if (showMoreInfoOption) Console.WriteLine("\ti.More Info");

            string key = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine();


            if (keys.Contains(key))
            {
                int index = Array.IndexOf(keys, key);
                if (index < options.Length) return index;
            }

            if (key == "i" && showMoreInfoOption) return -2;
            return -1;
        }
        public static int Options(string question, string[] options)
        {
            while (true)
            {
                int pickedOption = TryOptions(question, options);
                if (pickedOption != -1) return pickedOption;
            }
        }
        /// <summary>
        /// Prompts the user to select a hero, returns the index of the hero. It cannot be backed out of.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static int HeroOption(Hero[] options)
        {
            return Options("", options.Select(h => h.name).ToArray());
        }
    }
}
