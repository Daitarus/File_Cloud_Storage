using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDB
{
    public class RepositoryHistory : Repository<History> , IRepositoryHistory
    {
        public RepositoryHistory(string connectionString) : base(connectionString) { }
    }
}
