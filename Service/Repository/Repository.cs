using Data;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public ShopDbContext ShopDbContext { get; }

        public Repository(ShopDbContext shopDbContext)
        {
            ShopDbContext = shopDbContext;
        }
        public T Find(string id)
        {
            var item = (T)ShopDbContext.Find(typeof(T), id);
            return item;
        }
        public void Insert(T entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            ShopDbContext.Add(entity);
            ShopDbContext.SaveChanges();
        }
        public void Delete(string id)
        {
            var item = ShopDbContext.Find(typeof(T), id);
            ShopDbContext.Remove(item);
            ShopDbContext.SaveChanges();
        }
        public void Update(T entity) {
            ShopDbContext.Update(entity);
            ShopDbContext.SaveChanges();
        }
    }
}
