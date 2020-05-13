using EventsHub.Cleaner.Cleaners;
using EventsHub.DAL.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EventsHub.Cleaner
{
    public class CleanerRunner
    {
        private readonly List<ICleaner> cleaners;
        private readonly SqlServerDbContext context;

        public CleanerRunner()
        {
            context = (SqlServerDbContext)CreateDbContext();
            cleaners.AddRange(new List<ICleaner>() 
            {
                new ConcertCleaner(context), 
                new TheatreCleaner(context), 
                new FilmCleaner(context) 
            });
        }

        public void Run()
        {
            foreach (var cleaner in cleaners)
            {
                cleaner.Clean();
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
