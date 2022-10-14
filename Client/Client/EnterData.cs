using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal static class EnterData
    {
        public static IPEndPoint EnterIpEndPoint()
        {
            bool errorEnter = false;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;

            //enter ip
            while (!errorEnter)
            {
                PrintMessage.PrintColorMessage("Please, enter your ip: ", ConsoleColor.White, false);
                errorEnter = IPAddress.TryParse(Console.ReadLine(), out ip);
                if (!errorEnter)
                {
                    PrintMessage.PrintColorMessage("Error: Incorrect data !!!", ConsoleColor.Red, true);
                }
            }
            errorEnter = false;

            //enter port
            while (!errorEnter)
            {
                PrintMessage.PrintColorMessage("Please, enter tcp port: ", ConsoleColor.White, false);
                errorEnter = int.TryParse(Console.ReadLine(), out port);
                if (!errorEnter)
                {
                    PrintMessage.PrintColorMessage("Error: Incorrect data !!!", ConsoleColor.Red, true);
                }
            }
            Console.WriteLine();

            return new IPEndPoint(ip, port);
        }

        public static string EnterAuthorization()
        {
            bool errorEnter = false;
            string login = "", password = "";

            //enter login
            while (!errorEnter)
            {
                PrintMessage.PrintColorMessage("Please, enter your login: ", ConsoleColor.White, false);
                login = Console.ReadLine();
                if (login != null)
                {
                    if (login != "")
                    {
                        errorEnter = true;
                    }
                }
                if (!errorEnter)
                {
                    PrintMessage.PrintColorMessage("Login is empty !", ConsoleColor.Red, true);
                }
            }
            errorEnter = false;

            //enter password
            while (!errorEnter)
            {
                PrintMessage.PrintColorMessage("Please, enter password: ", ConsoleColor.White, false);
                password = Console.ReadLine();
                if (password != null)
                {
                    if (password != "")
                    {
                        errorEnter = true;
                    }
                }
                if (!errorEnter)
                {
                    PrintMessage.PrintColorMessage("Password is empty !", ConsoleColor.Red, true);
                }
            }
            Console.WriteLine();

            return login + password;
        }

        public static int EnterNumAction(List<string> comStr)
        {
            bool errorEnter = true;
            int com = 0;

            while (errorEnter)
            {
                PrintMessage.PrintColorMessage("Choose comand:", ConsoleColor.Magenta, true);
                for (int i = 0; i < comStr.Count; i++)
                {
                    PrintMessage.PrintColorMessage($"{i + 1}. {comStr[i]}", ConsoleColor.Yellow, true);
                }
                errorEnter = !int.TryParse(Console.ReadLine(), out com);
                if (!errorEnter)
                {
                    if (!((com >= 1) && (com <= comStr.Count)))
                    {
                        errorEnter = true;
                    }
                }
                if (errorEnter)
                {
                    PrintMessage.PrintColorMessage("Error: Invalid command!", ConsoleColor.Red, true);
                }
            }

            return com;
        }
    }
}
