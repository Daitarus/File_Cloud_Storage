

namespace RepositoryDB
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected ApplicationContext context;

        public Repository(string connectionString)
        {
            context = new ApplicationContext(connectionString);
        }
        public T? SelcetId(int id)
        {
            return context.Find<T>(id);
        }
        public void Insert(T entity)
        {
            context.Add(entity);
        }
        public void Remove(T entity)
        {
            context.Remove(entity);
        }
        public IQueryable<T> SelectAll()
        {
            return context.Set<T>();
        }
        public void SaveChange()
        {
            context.SaveChanges();
        }
    }
}
