using ProtocolCryptographyC;
using RepositoryDB;
using System.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace RetronslatorServer
{
    internal class Program
    {
        static PccServer pccServer;
        static string connectionString = ConfigurationManager.AppSettings["connectionString"];
        static Log log = new Log();

        static void Main(string[] args)
        {
            IPAddress? ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            IPEndPoint serverEndPoint = new IPEndPoint(ip, port);

            //check starting data
            bool checkData = false;
            if (args.Length == 2)
            {
                try
                {
                    ip = IPAddress.Parse(args[0]);
                    port = Convert.ToInt32(args[1]);
                    serverEndPoint = new IPEndPoint(ip, port);
                    checkData = true;
                }
                catch { }
            }
            if (!checkData)
            {
                //enter starting data
                serverEndPoint = WriteStartingData();
            }

            //start server
            PrintMessage.PrintSM("Server start...", ConsoleColor.Yellow, true);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            pccServer = new PccServer(serverEndPoint, rsa);
            pccServer.Start(Authorization, Algorithm, PrintSystemMessage);
        }





        //for one client
        private static bool Authorization(byte[] hash)
        {
            IRepositoryClient ClientR = new RepositoryClient(connectionString);
            Client? client = ClientR.SelectForHash(hash);
            return client != null;
        }

        private static void Algorithm(ClientInfo clientInfo)
        {
            string system_message, logString;

            IRepositoryClient ClientR = new RepositoryClient(connectionString);
            IRepositoryClientFile ClientFileR = new RepositoryClientFile(connectionString);
            Client? client = ClientR.SelectForHash(clientInfo.Hash);
            ClientFile? clientFile;
            if (client != null)
            {
                if (client.Id_Files != null)
                {
                    do
                    {
                        //get file info
                        system_message = pccServer.GetFileInfo(clientInfo.aes);
                        if (system_message[0] == 'F')
                        {
                            logString = $"{clientInfo.Ip}:{clientInfo.Port} - {system_message}";
                            log.LogWriter(system_message[0], logString);
                            break;
                        }
                        //check file in db
                        clientFile = ClientFileR.GetToFullName(system_message);
                        if (clientFile != null)
                        {
                            for (int i = 0; i < client.Id_Files.Length; i++)
                            {
                                if (client.Id_Files[i] == clientFile.Id)
                                {
                                    //send file
                                    system_message = pccServer.SendFile(system_message, clientInfo.aes);
                                    logString = $"{clientInfo.Ip}:{clientInfo.Port} - {system_message}";
                                    log.LogWriter(system_message[0], logString);
                                    break;
                                }
                            }
                        }
                    } while ((system_message[0] == 'W') || (system_message[0] == 'I'));
                }
            }
        }

        private static void PrintSystemMessage(string systemMessage)
        {
            log.LogWriter(systemMessage);
        }

        //servise methods
        static IPEndPoint WriteStartingData()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            bool errorEnter = false;

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
            errorEnter = false;

            return new IPEndPoint(ip, port);
        }

        private static byte[] GetHash(string authoriazationString)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                return sha1.ComputeHash(Encoding.UTF8.GetBytes(authoriazationString));
            }
        }
    }
}