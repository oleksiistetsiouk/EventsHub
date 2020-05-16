using EventsHub.DAL.SQLServer;
using EventsHub.Parser.Parsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace EventsHub.Parser
{
    public class ParserRunner
    {
        public async Task Run()
        {
            var fparser = new FilmParser();
            await fparser.Parse();

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
    }
}
