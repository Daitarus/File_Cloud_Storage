using RepositoryDB;
using System.Configuration;

namespace AdministratorDB
{
    internal class Program
    {
        static string connectionString = ConfigurationManager.AppSettings["connectionString"];
        static void Main(string[] args)
        {
            //check DB
            try
            {
                using (DB context = new DB(connectionString)) { }
                while (true)
                {
                    int com = Menu.MainMenu();
                    switch (com)
                    {
                        case 1:
                            AlgClient();
                            break;
                        case 2:
                            AlgFile();
                            break;
                        case 3:
                            AlgAttachment();
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
            }
            catch
            {
                Console.WriteLine("Error connect to DB !");
                Console.ReadKey();
            }
        }

        

        static void AlgClient()
        {
            int com = Menu.MenuClient();
            switch (com)
            {
                case 1:
                    WorkClient.AddClient();
                    break;
                case 2:
                    WorkClient.DeleteClient();
                    break;
                default:
                    break;
            }
        }

        static void AlgFile()
        {
            int com = Menu.MenuFile();
            switch (com)
            {
                case 1:
                    WorkFile.AddFile();
                    break;
                case 2:
                    WorkFile.DeleteFile();
                    break;
                default:
                    break;
            }
        }    

        static void AlgAttachment()
        {
            int com = Menu.MenuAttachment();
            switch (com)
            {
                case 1:
                    WorkAttach.AddAttach();
                    break;
                case 2:
                    WorkAttach.DeleteAttach();
                    break;
                default:
                    break;
            }
        }

        
    }
}