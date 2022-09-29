using ProtocolCryptographyC;
using System.Net;
using System.Text;

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
                PrintMessage.PrintSM('\n' + system_message + '\n', ConsoleColor.Cyan, true);

                //Main client work
                MainClientWork(pccClient);

                //disconnect
                PrintMessage.PrintSM('\n' + pccClient.Disconnect(), ConsoleColor.Cyan, true);
            }
            else
            {
                PrintMessage.PrintSM(system_message, ConsoleColor.Red, true);
            }
            Console.ReadLine();
        }

        static void MainClientWork(PccClient pccClient)
        {
            string system_message;

            //get list files
            PrintFileList(Encoding.UTF8.GetString(pccClient.GetMessage()));

            //get files
            do
            {
                //enter fileName
                string? fileName;
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
                }
                system_message = pccClient.GetFile();

                //print
                PrintSystemMessage(system_message);

            } while ((system_message[0] == 'W') || (system_message[0] == 'I'));
        }

        //print
        static void PrintFileList(string fileList)
        {
            PrintMessage.PrintSM("Your files:", ConsoleColor.Magenta, true);
            PrintMessage.PrintSM(fileList, ConsoleColor.Yellow, true);
        }
        static void PrintSystemMessage(string system_message)
        {
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