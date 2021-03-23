using Domain.Entities;

namespace Service.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Delete(string id);
        T Find(string id);
        T Insert(T entity);
        void Update(T entity);
    }
}