namespace RepositoryDB
{
    public interface IRepositoryFileC : IRepository<FileC>
    {
        public FileC? GetToPath(string path);
    }
}
