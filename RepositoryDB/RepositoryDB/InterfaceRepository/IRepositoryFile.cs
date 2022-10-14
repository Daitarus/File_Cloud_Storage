namespace RepositoryDB
{
    public interface IRepositoryFile : IRepository<FileC>
    {
        public FileC? GetToPath(string path);
    }
}
