namespace AdministratorDB
{
    internal static class Menu
    {
        public static int MainMenu()
        {
            return PatternMenu(new string[] { "Work with clients", "Work with files", "Work with file attachment" });
        }
        public static int MenuClient()
        {
            return PatternMenu(new string[] { "Add client", "Delete client" });
        }
        public static int MenuFile()
        {
            return PatternMenu(new string[] { "Add file", "Delete file" });
        }
        public static int MenuAttachment()
        {
            return PatternMenu(new string[] { "Add file attachment", "Delete file attachment" });
        }
        private static int PatternMenu(string[] strCom)
        {
            bool errorEnter = false;
            int com = 0;

            while (!errorEnter)
            {
                Console.WriteLine("Choose comand:");
                for (int i = 1; i <= strCom.Length; i++)
                {
                    Console.WriteLine($"{i}. {strCom[i - 1]}");
                }
                errorEnter = int.TryParse(Console.ReadLine(), out com);
                if (errorEnter)
                {
                    if (!((com >= 1) && (com <= strCom.Length)))
                    {
                        errorEnter = false;
                        Console.WriteLine("Error: Invalid command!");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid command!");
                }
            }

            return com;
        }
    }
}
