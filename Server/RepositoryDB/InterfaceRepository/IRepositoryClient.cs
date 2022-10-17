﻿namespace RepositoryDB
{
    public interface IRepositoryClient : IRepository<Client>
    {
        public Client? SelectForHash(byte[] hash);
    }
}
