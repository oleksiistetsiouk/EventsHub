using EventsHub.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventsHub.DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, FilterParams filterParams = null);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entity);
    }
}
