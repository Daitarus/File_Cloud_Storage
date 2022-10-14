namespace RepositoryDB
{
    public class RepositoryFile : Repository<FileC>, IRepositoryFile
    {

        public FileC? GetToPath(string path)
        {
            IQueryable<FileC> fileC = db.Files.Where(file => file.Path.Equals(path));
            if (fileC.Count() > 0)
            {
                return fileC.First();
            }
            else
            {
                return null;
            }
        }
    }
}
