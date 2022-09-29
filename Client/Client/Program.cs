using ProtocolCryptographyC;
using System.Net;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = EnterEndPoint();
            string authorizationString = EnterAuthorization();

            //start client
            PccClient pccClient = new PccClient(serverEndPoint, authorizationString);
            string system_message = pccClient.Connect();

            if (system_message[0] == 'I')
            {
                PrintMessage.PrintSM(system_message, ConsoleColor.Cyan, true);

                //get filename
                string? fileName;
                do
                {
                    //enter fileName
                    do
                    {
                        PrintMessage.PrintSM("Please, enter file name: ", ConsoleColor.White, false);
                        fileName = Console.ReadLine();
                        if ((fileName == null) || (fileName == ""))
                        {
                            PrintMessage.PrintSM("Error: empty file name !!!", ConsoleColor.Red, true);
                        }
                    } while ((fileName == null) || (fileName == ""));
                    system_message = pccClient.SendFileInfo(fileName);
                    if (system_message[0] == 'F')
                    {
                        PrintMessage.PrintSM(system_message, ConsoleColor.Red, true);
                        break;
                    }
                    system_message = pccClient.GetFile();

                    //print
                    if (system_message[0] == 'I')
                    {
                        PrintMessage.PrintSM(system_message, ConsoleColor.White, true);
                    }
                    if (system_message[0] == 'W')
                    {
                        PrintMessage.PrintSM(system_message, ConsoleColor.Yellow, true);
                    }
                    if ((system_message[0] == 'E') || (system_message[0] == 'F'))
                    {
                        PrintMessage.PrintSM(system_message, ConsoleColor.Red, true);
                    }
                } while ((system_message[0] == 'I') || (system_message[0] == 'W'));

                //disconnect
                PrintMessage.PrintSM(pccClient.Disconnect(), ConsoleColor.Yellow, true);
            }
            else
            {
                PrintMessage.PrintSM(system_message, ConsoleColor.Red, true);
            }
            Console.ReadLine();
        }


        //enter starting data
        private static IPEndPoint EnterEndPoint()
        {
            bool errorEnter = false;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;

            //enter ip
            while (!errorEnter)
            {
                PrintMessage.PrintSM("Please, enter your ip: ", ConsoleColor.White, false);
                errorEnter = IPAddress.TryParse(Console.ReadLine(), out ip);
                if (!errorEnter)
                {
                    PrintMessage.PrintSM("Error: Incorrect data !!!", ConsoleColor.Red, true);
                }
            }
            errorEnter = false;

            //enter port
            while (!errorEnter)
            {
                PrintMessage.PrintSM("Please, enter tcp port: ", ConsoleColor.White, false);
                errorEnter = int.TryParse(Console.ReadLine(), out port);
                if (!errorEnter)
                {
                    PrintMessage.PrintSM("Error: Incorrect data !!!", ConsoleColor.Red, true);
                }
            }
            
            return new IPEndPoint(ip, port);
        }
        private static string EnterAuthorization()
        {
            bool errorEnter = false;
            string login = "", password = "";

            //enter login
            while (!errorEnter)
            {
                PrintMessage.PrintSM("Please, enter your login: ", ConsoleColor.White, false);
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
                    PrintMessage.PrintSM("Login is empty !", ConsoleColor.Red, true);
                }
            }
            errorEnter = false;

            //enter password
            while (!errorEnter)
            {
                PrintMessage.PrintSM("Please, enter password: ", ConsoleColor.White, false);
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
                    PrintMessage.PrintSM("Password is empty !", ConsoleColor.Red, true);
                }
            }

            return login + password;
        }
    }
}