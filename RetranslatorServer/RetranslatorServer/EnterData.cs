using System.Net;

namespace RetranslatorServer
{
    internal static class EnterData
    {

        public static IPEndPoint GetIpEndPoint(string[] args)
        {
            IPEndPoint serverEndPoint;

            if (!ArgsParse(args, out serverEndPoint))
            {
                serverEndPoint = EnterIpEndPoint();
            }

            return serverEndPoint;
        }
        private static IPEndPoint EnterIpEndPoint()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            bool errorEnter = false;

            //enter ip
            while (!errorEnter)
            {
                PrintMessageServer.PrintColorMessage("Please, enter your ip: ", ConsoleColor.White, false);
                errorEnter = IPAddress.TryParse(Console.ReadLine(), out ip);
                if (!errorEnter)
                {
                    PrintMessageServer.PrintColorMessage("Error: Incorrect data !!!", ConsoleColor.Red, true);
                }
            }
            errorEnter = false;

            //enter port
            while (!errorEnter)
            {
                PrintMessageServer.PrintColorMessage("Please, enter tcp port: ", ConsoleColor.White, false);
                errorEnter = int.TryParse(Console.ReadLine(), out port);
                if (!errorEnter)
                {
                    PrintMessageServer.PrintColorMessage("Error: Incorrect data !!!", ConsoleColor.Red, true);
                }
            }
            errorEnter = false;

            return new IPEndPoint(ip, port);
        }
        private static bool ArgsParse(string[] args, out IPEndPoint? serverEndPoint)
        {
            if (args.Length == 2)
            {
                try
                {
                    IPAddress ip = IPAddress.Parse(args[0]);
                    int port = Convert.ToInt32(args[1]);
                    serverEndPoint = new IPEndPoint(ip, port);
                    return true;
                }
                catch
                {
                    serverEndPoint = null;
                    return false;
                }
            }
            else
            {
                serverEndPoint = null;
                return false;
            }
        }
    }
}
