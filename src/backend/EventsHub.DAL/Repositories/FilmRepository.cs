using EventsHub.Common.Helpers;
using EventsHub.DAL.Entities.Film;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventsHub.DAL.Repositories
{
    public class FilmRepository : Repository<Film>
    {
        private readonly DbSet<Film> dbSet;

        public FilmRepository(DbSet<Film> dbSet) : base(dbSet)
        {
            this.dbSet = dbSet;
        }

        public override async Task<Film> Get(Expression<Func<Film, bool>> predicate = null)
        {
            return await dbSet
                .Include(f => f.Cinemas)
                .ThenInclude(c => c.Sessions)
                .SingleOrDefaultAsync(predicate);
        }

        public override async Task<IEnumerable<Film>> GetAll(Expression<Func<Film, bool>> predicate = null, FilterParams filterParams = null)
        {
            return await dbSet
                .Include(f => f.Cinemas)
                .ThenInclude(c => c.Sessions)
                .Skip(filterParams.Skip)
                .Take(filterParams.Take)
                .ToListAsync();
        }
    }
}
