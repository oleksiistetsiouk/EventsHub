using EventsHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventsHub.DAL.Repositories
{
    public class UserRepository : Repository<User>
    {
        private readonly DbSet<User> dbSet;

        public UserRepository(DbSet<User> dbSet) : base(dbSet)
        {
            this.dbSet = dbSet;
        }

        public override async Task<User> Get(Expression<Func<User, bool>> predicate = null)
        {
            return await dbSet
                .Include(x => x.Role)
                .FirstOrDefaultAsync(predicate);
        }
    }
}
