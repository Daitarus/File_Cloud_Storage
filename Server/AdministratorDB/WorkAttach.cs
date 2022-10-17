using RepositoryDB;
using System.Configuration;

namespace AdministratorDB
{
    internal class WorkAttach
    {

        public static void AddAttach()
        {
            int clientId = 0, fileId = 0;
            bool errorEnter = false;
            IRepositoryClient repositoryClient = new RepositoryClient();
            IRepositoryFile repositoryFileC = new RepositoryFile();

            //enter client id
            while (!errorEnter)
            {
                Console.Write("Enter client id: ");
                errorEnter = int.TryParse(Console.ReadLine(), out clientId);
                if (!errorEnter)
                {
                    Console.WriteLine("Invalid client id !\n");
                }
                else {
                    //check in DB
                    Client client = repositoryClient.SelcetId(clientId);
                    if (client == null)
                    {
                        errorEnter = false;
                        Console.WriteLine("Client not found !\n");
                    }
                }
            }
            errorEnter = false;

            //enter file id
            while (!errorEnter)
            {
                Console.Write("Enter file id: ");
                errorEnter = int.TryParse(Console.ReadLine(), out fileId);
                if (!errorEnter)
                {
                    Console.WriteLine("Invalid file id !\n");
                }
                else
                {
                    //check in DB
                    FileC fileC = repositoryFileC.SelcetId(fileId);
                    if (fileC == null)
                    {
                        errorEnter = false;
                        Console.WriteLine("File not found !\n");
                    }
                }
            }

            //add in DB
            IRepositoryClientFile repositoryFileAttach = new RepositoryClientFile();
            ClientFile fileAttach = new ClientFile(clientId, fileId);
            repositoryFileAttach.Insert(fileAttach);
            repositoryFileAttach.SaveChange();

            Console.WriteLine("\nFile attachment was add !\n");
        }


        public static void DeleteAttach()
        {
            //enter id
            bool errorEnter = false;
            int id = 0;
            string stringId;

            while (!errorEnter)
            {
                Console.Write("Enter file attachment id: ");
                stringId = Console.ReadLine();
                errorEnter = int.TryParse(stringId, out id);
                if (!errorEnter)
                {
                    Console.WriteLine("Invalid id !\n");
                }
            }

            //delete attach
            RepositoryClientFile fileAttachR = new RepositoryClientFile();
            ClientFile fileAttach = fileAttachR.SelcetId(id);
            if (fileAttach != null)
            {
                fileAttachR.Remove(fileAttach);
                fileAttachR.SaveChange();
                Console.WriteLine("\nFile attachment was delete !\n");
            }
            else
            {
                Console.WriteLine("\nFile attachment not found !\n");
            }
        }
    }
}
