using ProtocolCryptographyC;
using System.Net;
using System.Text;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {

            PccClient pccClient = new PccClient(EnterData.EnterIpEndPoint(), EnterData.EnterAuthorization());
            PccSystemMessage systemMessage = pccClient.Connect();

            if(systemMessage.Key==PccSystemMessageKey.INFO)
            {
                PrintMessage.PrintColorMessage(systemMessage.Message, ConsoleColor.Cyan, true);
                Console.WriteLine();
                ClientAlgorithms.MainClientWork(pccClient);
                systemMessage = pccClient.Disconnect();
                PrintMessage.PrintColorMessage(systemMessage.Message, ConsoleColor.Cyan, true);
            }
            else
            {
                PrintMessage.PrintColorMessage(systemMessage.Message, ConsoleColor.Red, true);
            }

            Console.ReadLine();
        }  
    }
}