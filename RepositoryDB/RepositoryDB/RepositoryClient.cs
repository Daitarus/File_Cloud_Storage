namespace RepositoryDB
{
    public class RepositoryClient : Repository<Client> , IRepositoryClient 
    {
        public RepositoryClient(string connectionString) : base(connectionString) { }

        public Client? SelectForHash(byte[] hash)
        {
            IQueryable<Client> clients = context.Clients.Where(Clients => Clients.Hash.Equals(hash));
            if (clients.Count() > 0)
            {
                return clients.First<Client>();
            }
            else
            {
                return null;
            }
        }
    }
}
