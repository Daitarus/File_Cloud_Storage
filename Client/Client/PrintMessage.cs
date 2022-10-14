using ProtocolCryptographyC;

namespace Client
{
    internal static class PrintMessage
    {
        public static void PrintColorMessage(string message, ConsoleColor consoleColor, bool ifNewLine)
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
        public static void PrintFileList(string fileList)
        {
            PrintColorMessage("Your files:", ConsoleColor.Magenta, true);
            PrintColorMessage(fileList, ConsoleColor.Yellow, true);
        }

        public static void PrintSystemMessage(PccSystemMessage systemMessage)
        {
            switch(systemMessage.Key)
            {
                case PccSystemMessageKey.INFO:
                    {
                        PrintMessage.PrintColorMessage(systemMessage.Message, ConsoleColor.White, true);
                        break;
                    }
                case PccSystemMessageKey.WARRNING:
                    {
                        PrintMessage.PrintColorMessage(systemMessage.Message, ConsoleColor.Yellow, true);
                        break;
                    }
                default:
                    {
                        PrintMessage.PrintColorMessage(systemMessage.Message, ConsoleColor.Red, true);
                        break;
                    }
            }
        }
    }
}
