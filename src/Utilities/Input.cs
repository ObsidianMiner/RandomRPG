using System;

namespace RandomRPG
{
    public static class Input
    {
        public static void WaitForUser()
        {
            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();
            Console.WriteLine("");
        }
        public static bool GetUserYN(string question)
        {
            Console.WriteLine(question);
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
    }
}
