

namespace RepositoryDB
{
    public interface IRepository<T>
    {
        T? SelcetId(int id);
        void Insert(T entity);
        void Remove(T entity);
        IQueryable<T> SelectAll();
        void SaveChange();
    }
}
