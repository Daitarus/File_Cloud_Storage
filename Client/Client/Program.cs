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
            Console.WriteLine();
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
            bool mainCycle = true;
            string system_message;

            while (mainCycle)
            {

                //enter num action (Type Message)
                int com = EnterNumAction();

                switch (com)
                {
                    //get file list
                    case 1:
                        {
                            system_message = pccClient.SendMessage(new byte[] { (byte)TypeMessage.GET_FILES_LIST });
                            PrintFileList(Encoding.UTF8.GetString(pccClient.GetMessage()));
                            if (system_message[0]=='F')
                            {
                                mainCycle = false;
                            }
                            break;
                        }
                    //get file
                    case 2:
                        {
                            pccClient.SendMessage(new byte[] { (byte)TypeMessage.GET_FILE });
                            mainCycle = GetFile(pccClient);
                            break;
                        }
                    default:
                        break;
                }
            }           
        }

        static bool GetFile(PccClient pccClient)
        {
            string system_message;

            //enter path
            string? path;
            do
            {
                PrintMessage.PrintSM("Please, enter directory for save file: ", ConsoleColor.White, false);
                path = Console.ReadLine();
                if ((path == null) || (path == ""))
                {
                    PrintMessage.PrintSM("Error: empty directory !!!", ConsoleColor.Red, true);
                }
            } while ((path == null) || (path == ""));
            if (path[path.Length-1]!='\\')
            {
                path += '\\';
            }
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

            system_message = pccClient.GetFile(path);
            if((system_message[0] != 'I') && (system_message[0] != 'W'))
            {
                return false;
            }

            //print
            PrintSystemMessage(system_message);

            return true;
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

        static int EnterNumAction()
        {
            bool errorEnter = false;
            int com = 0;

            while (!errorEnter)
            {
                PrintMessage.PrintSM("Choose comand:", ConsoleColor.Magenta, true);
                PrintMessage.PrintSM("1. Get files list", ConsoleColor.Yellow, true);
                PrintMessage.PrintSM("2. Get file", ConsoleColor.Yellow, true);
                errorEnter = int.TryParse(Console.ReadLine(), out com);
                if (errorEnter)
                {
                    if (!((com >= 1) && (com <= 2)))
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