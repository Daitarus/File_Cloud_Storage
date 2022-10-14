using System.Text;
using RepositoryDB;
using ProtocolCryptographyC;

namespace RetranslatorServer
{
    internal static class ServerAlgorithms
    {       

        public static bool Authorization(byte[] hash)
        {
            IRepositoryClient ClientR = new RepositoryClient();
            Client? client = ClientR.SelectForHash(hash);
            return client != null;
        }

        public static void ServerMainAlgorithm(MessageTransport messageTransport, FileTransport fileTransport, ClientInfo clientInfo)
        {
            IRepositoryClient clientR = new RepositoryClient();
            Client client = clientR.SelectForHash(clientInfo.Hash);            

            if (client != null)
            {
                string clientAddress = clientInfo.ClientEndPoint.Address + ":" + clientInfo.ClientEndPoint.Port;

                IRepositoryHistory historyR = new RepositoryHistory();
                IRepositoryClientFile fileAttachR = new RepositoryClientFile();
                History history;

                PccSystemMessage systemMessage;

                bool mainCycle = true;
                byte[] bufferMessage;
                string historyStr = "";

                while (mainCycle)
                {
                    //get num comand (Type Message)
                    systemMessage = messageTransport.GetMessage(out bufferMessage);
                    PrintMessageServer.PrintSystemMessage(systemMessage);

                    if (systemMessage.Key == PccSystemMessageKey.INFO)
                    {
                        switch ((TypeMessage)bufferMessage[0])
                        {
                            case TypeMessage.GET_FILES_LIST:
                                {
                                    historyStr = "request list of files";
                                    mainCycle = SendFileList(client, messageTransport);
                                    break;
                                }
                            case TypeMessage.GET_FILE:
                                {
                                    historyStr = "request file";
                                    mainCycle = SendFile(client, fileTransport);
                                    break;
                                }
                            default:
                                mainCycle = false;
                                break;
                        }

                        //log action in DB (type)
                        history = new History(clientAddress, DateTime.Now, historyStr, client.Id);
                        historyR.Insert(history);
                        historyR.SaveChange();
                    }
                    else
                    {
                        mainCycle = false;
                    }
                }
            }
        }


        private static bool SendFileList(Client client, MessageTransport messageTransport)
        {
            IRepositoryClientFile clientFileR = new RepositoryClientFile();
            List<int> allIdFilesClient = clientFileR.IdFileForClient(client.Id);


            IRepositoryFile fileCR = new RepositoryFile();
            FileC fileC;

            string messageToClient = "";
            for (int i = 0; i < allIdFilesClient.Count; i++)
            {
                fileC = fileCR.SelcetId(allIdFilesClient[i]);
                if (fileC != null)
                {
                    messageToClient += (fileC.Path + "\n");
                }
            }
            if(messageToClient=="")
            {
                messageToClient = "No file list !";
            }
            PccSystemMessage systemMessage = messageTransport.SendMessage(Encoding.UTF8.GetBytes(messageToClient));
            PrintMessageServer.PrintSystemMessage(systemMessage);
            if (systemMessage.Key == PccSystemMessageKey.FATAL_ERROR)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        private static bool SendFile(Client client, FileTransport fileTransport)
        {
            IRepositoryClientFile clientFileR = new RepositoryClientFile();
            List<int> allIdFilesClient = clientFileR.IdFileForClient(client.Id);

            //get file info
            string fileInfo;
            PccSystemMessage systemMessage = fileTransport.GetFileInfo(out fileInfo);
            PrintMessageServer.PrintSystemMessage(systemMessage);
            if (systemMessage.Key != PccSystemMessageKey.FATAL_ERROR)
            {
                //check file in db
                systemMessage = null;
                IRepositoryFile fileCR = new RepositoryFile();
                FileC fileC = fileCR.GetToPath(fileInfo);
                if (fileC != null)
                {
                    foreach (var IdFilesClient in allIdFilesClient)
                    {
                        if (IdFilesClient == fileC.Id)
                        {
                            systemMessage = fileTransport.SendFile(fileC.FullPath);
                            break;
                        }
                    }
                }
                if (systemMessage != null)
                {
                    PrintMessageServer.PrintSystemMessage(systemMessage);
                    if (systemMessage.Key == PccSystemMessageKey.INFO || systemMessage.Key == PccSystemMessageKey.WARRNING)
                    {
                        return true;
                    }
                }
                else
                {
                    //imitation "not found"
                    systemMessage = fileTransport.SendFile(null);
                }
            }

            return false;
        }
    }
}
