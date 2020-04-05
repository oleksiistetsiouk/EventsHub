using EventsHub.DAL.Repositories;
using System.Threading.Tasks;

namespace EventsHub.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
