using EventsHub.DAL.Entities.Concert;
using EventsHub.DAL.Entities.Film;
using EventsHub.DAL.Entities.Theatre;
using EventsHub.DAL.SQLServer;
using EventsHub.Parser.Parsers;
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
            var parsers = new List<IParser>()
            {
                new FilmParser(),
                new ConcertParser(),
                new TheatreParser()
            };

            foreach (var parser in parsers)
            {
                await parser.Parse();
                using (var db = CreateDbContext())
                {
                    if (parser is FilmParser)
                    {
                        await db.Set<Film>().AddRangeAsync((parser as FilmParser).Films);
                    }
                    if (parser is ConcertParser)
                    {
                        await db.Set<Concert>().AddRangeAsync((parser as ConcertParser).Concerts);
                    }    
                    if (parser is TheatreParser)
                    {
                        await db.Set<TheatrePlay>().AddRangeAsync((parser as TheatreParser).Plays);
                    }
                    await db.SaveChangesAsync();
                }
            }
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
    }
}
