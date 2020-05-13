using EventsHub.DAL.SQLServer;
using System;
using System.Linq;

namespace EventsHub.Cleaner.Cleaners
{
    public class ConcertCleaner : ICleaner
    {
        private readonly SqlServerDbContext context;

        public ConcertCleaner(SqlServerDbContext context)
        {
            this.context = context;
        }

        public void Clean()
        {
            var concerts = context.Concerts.Where(c => c.CreatedAt == DateTime.Now);
            context.Concerts.RemoveRange(concerts);
        }
    }
}
