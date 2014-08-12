using System;
using System.Linq;
using EntitiesCoreFramework.Data;

namespace EntitiesCoreFramework.Repository
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Truncate();
        IQueryable<T> Table { get; }
    }
}
