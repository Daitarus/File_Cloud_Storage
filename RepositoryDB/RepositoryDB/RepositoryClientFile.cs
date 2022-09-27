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
    }
}
