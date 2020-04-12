using EventsHub.DAL.Entities.Theatre;
using EventsHub.DAL.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsHub.Parser
{
    public class ParserRunner
    {
        public async Task Run()
        {
            using var dbContext = CreateDbContext();

            var theatrePlays = new List<TheatrePlay>()
            {
                new TheatrePlay()
                {
                    Name ="New play",
                    Description ="Romantic story",
                    Place ="Zankovetskia teatre",
                    PriceFrom = 100,
                    PriceTo = 190
                },
                new TheatrePlay
                {
                    Name ="new Gamlet",
                    Description ="You mast watch it twice!!!",
                    Place ="Operniy teatre",
                    PriceFrom = 110,
                    PriceTo = 210
                }
            };

            await Seed<TheatrePlay>(dbContext, theatrePlays);
        }

        private DbContext CreateDbContext()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var connectionString = configuration.GetConnectionString("SmarterAspConnectionString");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString);
            var dbContextOptions = dbContextOptionsBuilder.Options;

            return new SqlServerDbContext(dbContextOptions);
        }

        public async Task Seed<TEntity>(DbContext dbContext, IEnumerable<TEntity> entities) where TEntity : class
        {
            await dbContext.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }
    }
}
