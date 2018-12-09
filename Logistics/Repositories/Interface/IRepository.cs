using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Logistics.Repositories.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(object id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Save(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
    }
}
