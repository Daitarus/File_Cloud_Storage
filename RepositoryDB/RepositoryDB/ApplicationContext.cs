using Microsoft.EntityFrameworkCore;

namespace RepositoryDB
{
    public class ApplicationContext : DbContext
    {
        private readonly string ConnectionString;
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientFile> ClientFiles { get; set; }
        public DbSet<History> Histories { get; set; }
        
        public ApplicationContext(string connectionString)
        {
            ConnectionString = connectionString;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}
