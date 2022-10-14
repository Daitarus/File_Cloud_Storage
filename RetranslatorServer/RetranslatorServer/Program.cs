using ProtocolCryptographyC;
using RepositoryDB;
using CryptL;

namespace RetranslatorServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!DB.CheckDB())
            {
                PrintMessageServer.PrintColorMessage("Error connect DB", ConsoleColor.Red, true);
            }
            else
            {
                CryptRSA cryptRSA = new CryptRSA();
                PccServer pccServer = new PccServer(EnterData.GetIpEndPoint(args), cryptRSA);
                PrintMessageServer.PrintColorMessage("Server start...", ConsoleColor.Yellow, true);
                pccServer.Start(ServerAlgorithms.Authorization, ServerAlgorithms.ServerMainAlgorithm, PrintMessageServer.PrintSystemMessage);
            }
            Console.WriteLine();
        }      
       
    }
}