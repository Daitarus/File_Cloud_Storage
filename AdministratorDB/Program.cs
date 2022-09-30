using RepositoryDB;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

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
                using (ApplicationContext context = new ApplicationContext(connectionString)) { }
                while (true)
                {
                    int com = MainMenu();
                    switch (com)
                    {
                        case 1:
                            AlgClient();
                            break;
                        case 2:
                            AlgFile();
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

        static int MainMenu()
        {
            return Menu(new string[] { "Work with clients", "Work with files" });
        }
        static int MenuClient()
        {
            return Menu(new string[] { "Add client", "Delete client" });
        }
        static int MenuFile()
        {
            return Menu(new string[] { "Add file", "Delete file" });
        }
        static int Menu(string[] strCom)
        {
            bool errorEnter = false;
            int com = 0;

            while (!errorEnter)
            {
                Console.WriteLine("Choose comand:");
                for(int i=1;i<=strCom.Length;i++)
                {
                    Console.WriteLine($"{i}. {strCom[i - 1]}");
                }
                errorEnter = int.TryParse(Console.ReadLine(), out com);
                if (errorEnter)
                {
                    if (!((com >= 1) && (com <= strCom.Length)))
                    {
                        errorEnter = false;
                        Console.WriteLine("Error: Invalid command!");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid command!");
                }
            }

            return com;
        }

        static void AlgClient()
        {
            int com = MenuClient();
            switch (com)
            {
                case 1:
                    AddClient();
                    break;
                case 2:
                    DeleteClient();
                    break;
                default:
                    break;
            }
        }

        static void AlgFile()
        {
            int com = MenuFile();
            switch (com)
            {
                case 1:
                    AddFile();
                    break;
                case 2:
                    DeleteFile();
                    break;
                default:
                    break;
            }
        }


        static void AddClient()
        {
            bool errorEnter = false;
            string login = "", password = "";

            //enter name
            while (!errorEnter)
            {
                Console.Write("Enter login: ");
                login = Console.ReadLine();
                if (login != null)
                {
                    if (login != "")
                    {
                        errorEnter = true;
                    }
                }
                if (!errorEnter)
                {
                    Console.WriteLine("Login is empty !");
                }
            }
            errorEnter = false;

            //enter path
            while (!errorEnter)
            {
                Console.Write("Enter password: ");
                password = Console.ReadLine();
                if (password != null)
                {
                    if (password != "")
                    {
                        errorEnter = true;
                    }
                }
                if (!errorEnter)
                {
                    Console.WriteLine("Password is empty !");
                }
            }
            errorEnter = false;

            //enter array file's id
            string stringId = "";
            string[] arrayStringId;
            int fileId = 0;
            List<int> arrayFileId = new List<int>();

            while (!errorEnter)
            {
                Console.Write("Enter array client id: ");
                stringId = Console.ReadLine();
                if (stringId != null)
                {
                    arrayStringId = stringId.Split(' ');
                    if (arrayStringId.Length > 0)
                    {
                        errorEnter = true;
                        for (int i = 0; i < arrayStringId.Length; i++)
                        {
                            errorEnter = int.TryParse(arrayStringId[i], out fileId);
                            if (errorEnter)
                            {
                                arrayFileId.Add(fileId);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                if (!errorEnter)
                {
                    Console.WriteLine("Invalid client id !");
                }
            }

            //create hash
            byte[] hash;
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(login + password));
            }
            //add Client
            RepositoryClient clientR = new RepositoryClient(connectionString);
            Client client = new Client(hash, login, arrayFileId.ToArray());
            clientR.Insert(client);
            clientR.SaveChange();

            Console.WriteLine("Client was add !");
        }

        static void DeleteClient()
        {
            //enter id
            bool errorEnter = false;
            int id = 0;
            string stringId;

            while (!errorEnter)
            {
                Console.Write("Enter file's id: ");
                stringId = Console.ReadLine();
                errorEnter = int.TryParse(stringId, out id);
                if (!errorEnter)
                {
                    Console.WriteLine("Invalid id !");
                }
            }

            //delete file
            RepositoryClient clientR = new RepositoryClient(connectionString);
            Client? client = clientR.SelcetId(id);
            if (client != null)
            {
                clientR.Remove(client);
                clientR.SaveChange();
                Console.WriteLine("Client was delete !");
            }
            else
            {
                Console.WriteLine("Client not found !");
            }
        }

        static void AddFile()
        {
            bool errorEnter = false;
            string name = "", path = "", fullpath = "";

            //enter name
            while (!errorEnter)
            {
                Console.Write("Enter name: ");
                name = Console.ReadLine();
                if (name != null)
                {
                    if (name != "")
                    {
                        errorEnter = true;
                    }
                }
                if (!errorEnter)
                {
                    Console.WriteLine("Name is empty !");
                }
            }
            errorEnter = false;

            //enter path
            while (!errorEnter)
            {
                Console.Write("Enter path: ");
                path = Console.ReadLine();
                if (path != null)
                {
                    if (path != "")
                    {
                        errorEnter = true;
                    }
                }
                if (!errorEnter)
                {
                    Console.WriteLine("Path is empty !");
                }
            }
            errorEnter = false;

            //enter fullpath
            while (!errorEnter)
            {
                Console.Write("Enter full path: ");
                fullpath = Console.ReadLine();
                if (fullpath != null)
                {
                    if (fullpath != "")
                    {
                        errorEnter = true;
                    }
                }
                if (!errorEnter)
                {
                    Console.WriteLine("Full path is empty !");
                }
            }
            errorEnter = false;

            //add file
            RepositoryFileC fileR = new RepositoryFileC(connectionString);
            FileC file = new FileC(name, path, fullpath);
            fileR.Insert(file);
            fileR.SaveChange();

            Console.WriteLine("File was add !");
        }

        static void DeleteFile()
        {
            //enter id
            bool errorEnter = false;
            int id = 0;
            string stringId;

            while (!errorEnter)
            {
                Console.Write("Enter file id: ");
                stringId = Console.ReadLine();
                errorEnter = int.TryParse(stringId, out id);
                if (!errorEnter)
                {
                    Console.WriteLine("Invalid id !");
                }
            }

            //delete file
            RepositoryFileC fileR = new RepositoryFileC(connectionString);
            FileC? file = fileR.SelcetId(id);
            if (file != null)
            {
                fileR.Remove(file);
                fileR.SaveChange();
                Console.WriteLine("File was delete !");
            }
            else
            {
                Console.WriteLine("File not found !");
            }
        }
    }
}