using RepositoryDB;
using System.Configuration;

namespace AdministratorDB
{
    internal class WorkFile
    {
        static string connectionString = ConfigurationManager.AppSettings["connectionString"];
        public static void AddFile()
        {
            bool errorEnter = false;
            string name = "", path = "", fullpath = "";

            //enter name
            while (!errorEnter)
            {
                Console.Write("Enter file name: ");
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
                    Console.WriteLine("Name is empty !\n");
                }
            }
            errorEnter = false;

            //enter path
            while (!errorEnter)
            {
                Console.Write("Enter file path: ");
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
                    Console.WriteLine("Path is empty !\n");
                }
            }
            errorEnter = false;

            //enter fullpath
            while (!errorEnter)
            {
                Console.Write("Enter file full path: ");
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
                    Console.WriteLine("Full path is empty !\n");
                }
            }
            errorEnter = false;

            //add file
            RepositoryFile fileR = new RepositoryFileC(connectionString);
            FileC file = new FileC(name, path, fullpath);
            fileR.Insert(file);
            fileR.SaveChange();

            Console.WriteLine("\nFile was add !\n");
        }

        public static void DeleteFile()
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
                    Console.WriteLine("Invalid file id !\n");
                }
            }

            //delete file
            RepositoryFile fileR = new RepositoryFileC(connectionString);
            FileC? file = fileR.SelcetId(id);
            if (file != null)
            {
                fileR.Remove(file);
                fileR.SaveChange();
                Console.WriteLine("\nFile was delete !\n");
            }
            else
            {
                Console.WriteLine("\nFile not found !\n");
            }
        }
    }
}
