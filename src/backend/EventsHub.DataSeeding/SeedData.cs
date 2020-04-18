using EventsHub.DAL.SQLServer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsHub.DataSeeding
{
    internal static class SeedData
    {
        public static async Task Seed<TEntity>(this SqlServerDbContext dbContext, IEnumerable<TEntity> entities) where TEntity : class
        {
            await dbContext.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }

        public static async Task Clear<TEntity>(this SqlServerDbContext dbContext) where TEntity : class
        {
            var entities = await dbContext.Set<TEntity>().ToListAsync();
            dbContext.Set<TEntity>().RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }

        public static async Task ClearAndSeed<TEntity>(this SqlServerDbContext dbContext, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            await dbContext.Clear<TEntity>();
            await dbContext.Seed(entities);
        }
    }
}
