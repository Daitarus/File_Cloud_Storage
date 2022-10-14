using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace RepositoryDB
{
    public class DB : DbContext
    {
        private string connectionString = ConfigurationManager.AppSettings["connectionString"];

        public DbSet<Client> Clients { get; set; }
        public DbSet<FileC> Files { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<ClientFile> FilesAttach { get; set; }
        
        public DB()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }

        public static bool CheckDB()
        {
            try
            {
                using (new DB()) { }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
