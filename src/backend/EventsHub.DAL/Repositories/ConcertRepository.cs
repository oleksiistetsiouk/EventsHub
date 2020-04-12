using EventsHub.Common.Helpers;
using EventsHub.DAL.Entities.Concert;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventsHub.DAL.Repositories
{
    public class ConcertRepository : Repository<Concert>
    {
        private readonly DbSet<Concert> dbSet;

        public ConcertRepository(DbSet<Concert> dbSet) : base(dbSet)
        {
            this.dbSet = dbSet;
        }

        public override async Task<IEnumerable<Concert>> GetAll(Expression<Func<Concert, bool>> predicate = null, FilterParams filterParams = null)
        {
            return  await dbSet
                .Where(t => t.Price >= filterParams.FromPrice)
                .Where(t => t.Price <= filterParams.ToPrice)
                .Where(t => t.Date >= filterParams.FromDate)
                .Where(t => t.Date <= filterParams.ToDate)
                .Skip(filterParams.Skip)
                .Take(filterParams.Take)
                .ToListAsync();
        }
    }
}
