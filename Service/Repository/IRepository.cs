using Domain.Entities;

namespace Service.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Delete(string id);
        T Find(string id);
        void Insert(T entity);
        void Insert(T entity, string id);
        void Update(T entity);
    }
}