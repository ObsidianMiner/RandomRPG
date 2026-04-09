using System;

namespace RandomRPG
{
    public static class Messages
    {
        public static void StartGameMessage()
        {
            Console.WriteLine(@" ____             _             _                            ___ ");
            Console.WriteLine(@"| __ )  ___  __ _(_)_ __       | | ___  _ __ _ __   ___ _   |__ \");
            Console.WriteLine(@"|  _ \ / _ \/ _` | | '_ \   _  | |/ _ \| '__| '_ \ / _ \ | | |/ /");
            Console.WriteLine(@"| |_) |  __/ (_| | | | | | | |_| | (_) | |  | | | |  __/ |_| |_|");
            Console.WriteLine(@"|____/ \___|\__, |_|_| |_|  \___/ \___/|_|  |_| |_|\___|\__, (_) ");
            Console.WriteLine(@"            |___/                                       |___/    ");
        }
        public static void LoseGameMessage()
        {
            Console.WriteLine(@" ____  _    _ _ _      ___                    ");
            Console.WriteLine(@"/ ___|| | _(_) | |    |_ _|___ ___ _   _  ___ ");
            Console.WriteLine(@"\___ \| |/ / | | |     | |/ __/ __| | | |/ _ \");
            Console.WriteLine(@" ___) |   <| | | |     | |\__ \__ \ |_| |  __/");
            Console.WriteLine(@"|____/|_|\_\_|_|_|    |___|___/___/\__,_|\___|");
        }
        public static void WinGameMessage()
        {
            Console.WriteLine(@"__        ___                       _ _ ");
            Console.WriteLine(@"\ \      / (_)_ __  _ __   ___ _ __| | |");
            Console.WriteLine(@" \ \ /\ / /| | '_ \| '_ \ / _ \ '__| | |");
            Console.WriteLine(@"  \ V  V / | | | | | | | |  __/ |  |_|_|");
            Console.WriteLine(@"   \_/\_/  |_|_| |_|_| |_|\___|_|  (_|_)");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(@"A new challenger approches! Press V at the start for a new campagin!");
        }
        public static void CampMessage()
        {
            Console.WriteLine(@"  ____                      ");
            Console.WriteLine(@" / ___|__ _ _ __ ___  _ __  ");
            Console.WriteLine(@"| |   / _` | '_ ` _ \| '_ \ ");
            Console.WriteLine(@"| |__| (_| | | | | | | |_) |");
            Console.WriteLine(@" \____\__,_|_| |_| |_| .__/ ");
            Console.WriteLine(@"                     |_|    ");
        }
        public static void BossMessage()
        {
            Console.WriteLine(@" _____ _             _   ____                _ ");
            Console.WriteLine(@"|  ___(_)_ __   __ _| | | __ )  ___  ___ ___| |");
            Console.WriteLine(@"| |_  | | '_ \ / _` | | |  _ \ / _ \/ __/ __| |");
            Console.WriteLine(@"|  _| | | | | | (_| | | | |_) | (_) \__ \__ \_|");
            Console.WriteLine(@"|_|   |_|_| |_|\__,_|_| |____/ \___/|___/___(_)");
        }
    }
}
