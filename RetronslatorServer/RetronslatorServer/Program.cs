using NLog;
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
                serverEndPoint = WriteIpEndPoint();
            }

            //start server
            PrintMessage.PrintSM("Server Start...", ConsoleColor.Yellow, true);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            pccServer = new PccServer(serverEndPoint, rsa);
            pccServer.Start(Authorization, Algorithm, PrintSystemMessage);
        }

        //for one client
        private static bool Authorization(byte[] hash)
        {
            IRepositoryClient ClientR = new RepositoryClient(connectionString);
            Client? client = ClientR.SelectForHash(hash);
            if(client!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void Algorithm(ClientInfo clientInfo)
        {
            string system_message, logString;

            do
            {
                system_message = pccServer.TransferFile(clientInfo.aes);
                logString = $"{clientInfo.Ip}:{clientInfo.Port} - {system_message}";
                log.LogWriter(system_message[0], logString);
            } while ((system_message[0] == 'W') || (system_message[0] == 'I'));
        }

        private static void PrintSystemMessage(string systemMessage)
        {
            log.LogWriter(systemMessage);
        }

        //servise methods
        static IPEndPoint WriteIpEndPoint()
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