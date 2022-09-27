

namespace RepositoryDB
{
    public interface IRepository<T>
    {
        T? SelcetId(int id);
        void Insert(T entity);
        void Remove(int id);
        IQueryable<T> SelectAll();
        void SaveChange();
    }
}
