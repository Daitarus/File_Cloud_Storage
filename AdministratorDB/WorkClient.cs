using RepositoryDB;
using CryptL;
using System.Configuration;
using System.Text;

namespace AdministratorDB
{
    internal static class WorkClient
    {
        static string connectionString = ConfigurationManager.AppSettings["connectionString"];
        public static void AddClient()
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
                    Console.WriteLine("Login is empty !\n");
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
                    Console.WriteLine("Password is empty !\n");
                }
            }
            errorEnter = false;

            //create hash
            byte[] hash = HashSHA256.GetHash(Encoding.UTF8.GetBytes(login+password));

            //add Client
            RepositoryClient clientR = new RepositoryClient(connectionString);
            Client client = new Client(hash, login);
            clientR.Insert(client);
            clientR.SaveChange();

            Console.WriteLine("\nClient was add !\n");
        }

        public static void DeleteClient()
        {
            //enter id
            bool errorEnter = false;
            int id = 0;
            string stringId;

            while (!errorEnter)
            {
                Console.Write("Enter client id: ");
                stringId = Console.ReadLine();
                errorEnter = int.TryParse(stringId, out id);
                if (!errorEnter)
                {
                    Console.WriteLine("Invalid client id !\n");
                }
            }

            //delete file
            RepositoryClient clientR = new RepositoryClient(connectionString);
            Client? client = clientR.SelcetId(id);
            if (client != null)
            {
                clientR.Remove(client);
                clientR.SaveChange();
                Console.WriteLine("\nClient was delete !\n");
            }
            else
            {
                Console.WriteLine("\nClient not found !\n");
            }
        }
    }
}
