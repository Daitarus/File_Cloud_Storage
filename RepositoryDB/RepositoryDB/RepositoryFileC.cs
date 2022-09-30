namespace RepositoryDB
{
    public class RepositoryFileC : Repository<FileC>, IRepositoryFileC
    {
        public RepositoryFileC(string connectionString) : base(connectionString) { }

        public FileC? GetToPath(string path)
        {
            IQueryable<FileC> fileC = context.Files.Where(file => file.Path.Equals(path));
            if (fileC.Count() > 0)
            {
                return fileC.First<FileC>();
            }
            else
            {
                return null;
            }
        }
    }
}
