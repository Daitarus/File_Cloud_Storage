namespace RepositoryDB
{
    public interface IRepositoryClientFile : IRepository<ClientFile>
    {
        List<int> IdFileForClient(int id);
    }
}
