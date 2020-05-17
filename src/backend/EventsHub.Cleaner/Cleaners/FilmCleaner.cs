using EventsHub.DAL.SQLServer;
using System;
using System.Linq;

namespace EventsHub.Cleaner.Cleaners
{
    public class FilmCleaner : ICleaner
    {
        private readonly SqlServerDbContext context;

        public FilmCleaner(SqlServerDbContext context)
        {
            this.context = context;
        }

        public void Clean()
        {
            var sessions = context.Sessions.Where(c => c.CreatedAt == DateTime.Now);
            context.Sessions.RemoveRange(sessions);

            var cinemas = context.Cinemas.Where(c => c.CreatedAt == DateTime.Now);
            context.Cinemas.RemoveRange(cinemas);

            var films = context.Concerts.Where(c => c.CreatedAt == DateTime.Now);
            context.Concerts.RemoveRange(films);
        }
    }
}
