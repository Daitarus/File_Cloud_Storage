namespace RepositoryDB
{
    public interface IRepositoryClientFile : IRepository<ClientFile>
    {
        public ClientFile? GetToFullName(string name);
    }
}
