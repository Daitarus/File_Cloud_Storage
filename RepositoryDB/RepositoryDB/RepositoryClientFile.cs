using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDB
{
    public class RepositoryClientFile : Repository<ClientFile>, IRepositoryClientFile
    {
        public RepositoryClientFile(string connectionString) : base(connectionString) { }

        public ClientFile? GetToFullName(string name)
        {
            IQueryable<ClientFile> clientFiles = context.ClientFiles.Where(clientFile => clientFile.Name.Equals(name));
            if (clientFiles.Count() > 0)
            {
                return clientFiles.First<ClientFile>();
            }
            else
            {
                return null;
            }
        }
    }
}
