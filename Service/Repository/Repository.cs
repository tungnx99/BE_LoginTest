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
        public T Find(string id) {
            return (T)ShopDbContext.Find(typeof(T), id);
        }
        public T Insert(T entity) {
            ShopDbContext.Add(entity);
            ShopDbContext.SaveChanges();
            return entity;
        }
        public void Delete(string id) { }
        public void Update(T entity) { }
    }
}
