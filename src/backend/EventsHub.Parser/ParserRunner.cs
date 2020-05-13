using EventsHub.DAL.Entities.Theatre;
using EventsHub.DAL.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void Test()
        {
            DateTime now = DateTime.Now;
            string linkTempate = $"https://www.032.ua/afisha/cat/1/kino?date={now.Year}-{now.Month}-{now.Day}";

            //FilmParser fparser = new FilmParser(linkTempate);
            //fparser.Parse();
            //
            //ConcertParser cparser = new ConcertParser();
            //cparser.ParseConcerts();

            //TheatreParser parser = new TheatreParser();

            //using (var db = new EventsHubContext())
            //{
            //    db.Database.EnsureDeleted();
            //    db.Database.EnsureCreated();

            //    db.Films.Add(new Film() { Title = "Fight Club" });
            //    db.Films.AddRange(fparser.Films);
            //    db.Concerts.AddRange(cparser.Concerts);

            //    db.SaveChanges();
            //}
            var context = (SqlServerDbContext)CreateDbContext();
            using (context)
            {
                foreach (var film in context.Films.Take(5).ToList())
                {
                    Console.WriteLine(film.Title);
                    foreach (var cinema in film.Cinemas)
                    {
                        Console.WriteLine("\t" + cinema.CinemaName);
                        foreach (var session in cinema.Sessions)
                        {
                            Console.WriteLine("\t\t" + session.Time + "\n");
                        }
                    }
                }
            }
        }
    }
}
