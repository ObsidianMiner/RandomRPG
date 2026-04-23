namespace RandomRPG
{
    public static class Messages
    {
        public static void ColoredWriteLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
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
            if (!RPG.magicCapaign) Console.WriteLine($"A new challenger approches! Press {(RPG.techCampaign ? "m" : "v")} at the start for a new campagin!");
            else Console.WriteLine("Congrats on beating the hardest campaign!");
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
        public static void DarknessConsumesMessage()
        {
            Console.WriteLine(@"     _            _                        ");
            Console.WriteLine(@"    | |          | |                       ");
            Console.WriteLine(@"  __| | __ _ _ __| | ___ __   ___  ___ ___ ");
            Console.WriteLine(@" / _` |/ _` | '__| |/ / '_ \ / _ \/ __/ __|");
            Console.WriteLine(@"| (_| | (_| | |  |   <| | | |  __/\__ \__ \");
            Console.WriteLine(@" \__,_|\__,_|_|  |_|\_\_| |_|\___||___/___/");
            Console.WriteLine(@"  ___ ___  _ __  ___ _   _ _ __ ___   ___  ___ ");
            Console.WriteLine(@" / __/ _ \| '_ \/ __| | | | '_ ` _ \ / _ \/ __|");
            Console.WriteLine(@"| (_| (_) | | | \__ \ |_| | | | | | |  __/\__ \");
            Console.WriteLine(@" \___\___/|_| |_|___/\__,_|_| |_| |_|\___||___/");
        }
    }
}
