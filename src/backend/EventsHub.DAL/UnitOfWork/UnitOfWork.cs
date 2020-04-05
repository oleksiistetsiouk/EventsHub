using EventsHub.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventsHub.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        private DbContext Context { get; }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(Context.Set<TEntity>());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
