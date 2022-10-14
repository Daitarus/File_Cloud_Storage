namespace RepositoryDB
{
    public class RepositoryClient : Repository<Client>, IRepositoryClient
    {

        public Client? SelectForHash(byte[] hash)
        {
            IQueryable<Client> clients = db.Clients.Where(client => client.Hash.Equals(hash));
            if (clients.Count() > 0)
            {
                return clients.First();
            }
            else
            {
                return null;
            }
        }
    }
}
