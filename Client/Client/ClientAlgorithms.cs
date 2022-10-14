using ProtocolCryptographyC;
using System.Text;

namespace Client
{
    internal static class ClientAlgorithms
    {
        public static void MainClientWork(PccClient pccClient)
        {
            bool mainCycle = true;

            while (mainCycle)
            {
                switch (EnterData.EnterNumAction(new List<string>() { "Get files list", "Get file" }))
                {
                    case 1:
                        {
                            mainCycle = GetFileList(pccClient);
                            break;
                        }
                    case 2:
                        {             
                            mainCycle = GetFile(pccClient);
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private static bool GetFileList(PccClient pccClient)
        {
            PccSystemMessage systemMessage = pccClient.messageTransport.SendMessage(new byte[] { (byte)TypeMessage.GET_FILES_LIST });
            if (systemMessage.Key == PccSystemMessageKey.INFO)
            {
                byte[] buffer;
                systemMessage = pccClient.messageTransport.GetMessage(out buffer);
                if (systemMessage.Key == PccSystemMessageKey.INFO)
                {
                    PrintMessage.PrintFileList(Encoding.UTF8.GetString(buffer));
                    return true;
                }
            }

            return false;
        }

        private static bool GetFile(PccClient pccClient)
        {
            PccSystemMessage systemMessage = pccClient.messageTransport.SendMessage(new byte[] { (byte)TypeMessage.GET_FILE });
            if (systemMessage.Key == PccSystemMessageKey.INFO)
            {
                //enter path
                string? path;
                do
                {
                    PrintMessage.PrintColorMessage("Please, enter directory for save file: ", ConsoleColor.White, false);
                    path = Console.ReadLine();
                    if ((path == null) || (path == ""))
                    {
                        PrintMessage.PrintColorMessage("Error: empty directory !!!", ConsoleColor.Red, true);
                    }
                } while ((path == null) || (path == ""));
                if (path[path.Length - 1] != '\\')
                {
                    path += '\\';
                }

                //enter fileName
                string? fileName;
                do
                {
                    PrintMessage.PrintColorMessage("Please, enter file name: ", ConsoleColor.White, false);
                    fileName = Console.ReadLine();
                    if ((fileName == null) || (fileName == ""))
                    {
                        PrintMessage.PrintColorMessage("Error: empty file name !!!", ConsoleColor.Red, true);
                    }
                } while ((fileName == null) || (fileName == ""));


                systemMessage = pccClient.fileTransport.SendFileInfo(fileName);
                if (systemMessage.Key == PccSystemMessageKey.INFO)
                {
                    systemMessage = pccClient.fileTransport.GetFile(path);
                    if ((systemMessage.Key == PccSystemMessageKey.INFO) || (systemMessage.Key == PccSystemMessageKey.WARRNING))
                    {
                        PrintMessage.PrintSystemMessage(systemMessage);
                        return true;
                    }
                }
            }
            PrintMessage.PrintSystemMessage(systemMessage);

            return false;
        }
    }
}
