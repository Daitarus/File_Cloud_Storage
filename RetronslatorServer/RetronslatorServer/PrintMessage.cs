using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetronslatorServer
{
    internal class PrintMessage
    {
        public static void PrintSM(string message, ConsoleColor consoleColor, bool ifNewLine)
        {
            Console.ForegroundColor = consoleColor;
            if (ifNewLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
            Console.ResetColor();
        }
    }
}
