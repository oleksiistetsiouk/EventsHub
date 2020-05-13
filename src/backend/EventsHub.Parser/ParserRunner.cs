using EventsHub.DAL.SQLServer;
using EventsHub.Parser.Parsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace EventsHub.Parser
{
    public class ParserRunner
    {
        public async Task Run()
        {
            var now = DateTime.Now;
            var linkTempate = $"https://www.032.ua/afisha/cat/1/kino?date={now.Year}-{now.Month}-{now.Day}";

            var fparser = new FilmParser(linkTempate);
            fparser.Parse();

            using (var db = CreateDbContext())
            {
                db.Films.AddRange(fparser.Films);
                db.SaveChanges();
            }
        }

        private SqlServerDbContext CreateDbContext()
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

        private void Test()
        {
            //formatted output
            //using (var context = CreateDbContext())
            //{
            //    foreach (var film in context.Films.Take(5).ToList())
            //    {
            //        Console.WriteLine(film.Title);
            //        foreach (var cinema in film.Cinemas)
            //        {
            //            Console.WriteLine("\t" + cinema.CinemaName);
            //            foreach (var session in cinema.Sessions)
            //            {
            //                Console.WriteLine("\t\t" + session.Time + "\n");
            //            }
            //        }
            //    }
            //}
        }
    }
}
