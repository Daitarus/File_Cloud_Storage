using RepositoryDB;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Registrator
{
    internal class Program
    {
        static string connectionString = ConfigurationManager.AppSettings["connectionString"];
        static void Main(string[] args)
        {
            while (true)
            {
                int com = Menu();
                switch (com)
                {
                    case 1:
                        AddClient();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            }
        }

        static int Menu()
        {
            bool errorEnter = false;
            int com = 0;

            while (!errorEnter)
            {
                Console.WriteLine("Choose comand:");
                Console.WriteLine("1. Add client");
                Console.WriteLine("2. Upgrade client");
                Console.WriteLine("3. Delete client");
                errorEnter = int.TryParse(Console.ReadLine(), out com);
                if(errorEnter)
                {
                    if(!((com >= 1) && (com <= 3)))
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

        static void AddClient()
        {
            bool errorEnter = false;
            string login = "", password = "";

            //enter login
            while (!errorEnter)
            {
                Console.Write("Enter your login: ");
                login = Console.ReadLine();
                if (login != null)
                {
                    if(login != "")
                    {
                        errorEnter = true;
                    }
                }
                if(!errorEnter)
                {
                    Console.WriteLine("Login is empty !");
                }
            }
            errorEnter = false;

            //enter password
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
                Console.Write("Enter array file's id: ");
                stringId = Console.ReadLine();
                if (stringId != null)
                {
                    arrayStringId = stringId.Split(' ');
                    if(arrayStringId.Length > 0)
                    {
                        errorEnter = true;
                        for(int i=0;i<arrayStringId.Length;i++)
                        {
                            errorEnter = int.TryParse(arrayStringId[i], out fileId);
                            if(errorEnter)
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
                if(!errorEnter)
                {
                    Console.WriteLine("Invalid file's id !");
                }
            }

            //create hash
            byte[] hash;
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                hash =  sha1.ComputeHash(Encoding.UTF8.GetBytes(login + password));
            }
            //add Client
            RepositoryClient clientR = new RepositoryClient(connectionString);
            Client client = new Client(hash, login, arrayFileId.ToArray());
            clientR.Insert(client);
            clientR.SaveChange();

            Console.WriteLine("Client was add !");
        }
    }
}