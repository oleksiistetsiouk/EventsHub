using EventsHub.Common.Helpers;
using EventsHub.DAL.Entities.Theatre;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventsHub.DAL.Repositories
{
    public class TheatreRepository : Repository<TheatrePlay>
    {
        private readonly DbSet<TheatrePlay> dbSet;

        public TheatreRepository(DbSet<TheatrePlay> dbSet) : base(dbSet)
        {
            this.dbSet = dbSet;
        }

        public override async Task<IEnumerable<TheatrePlay>> GetAll(Expression<Func<TheatrePlay, bool>> predicate = null, FilterParams filterParams = null)
        {
            return await dbSet
                .Where(t => t.PriceFrom >= filterParams.FromPrice)
                .Where(t => t.PriceTo <= filterParams.ToPrice)
                .Skip(filterParams.Skip)
                .Take(filterParams.Take)
                .ToListAsync();
        }
    }
}
