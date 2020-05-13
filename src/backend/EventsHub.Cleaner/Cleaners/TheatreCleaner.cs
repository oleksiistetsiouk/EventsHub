using EventsHub.DAL.SQLServer;
using System;
using System.Linq;

namespace EventsHub.Cleaner.Cleaners
{
    public class TheatreCleaner : ICleaner
    {
        private readonly SqlServerDbContext context;

        public TheatreCleaner(SqlServerDbContext context)
        {
            this.context = context;
        }

        public void Clean()
        {
            var theatrePlays = context.TheatrePlays.Where(c => c.CreatedAt == DateTime.Now);
            context.TheatrePlays.RemoveRange(theatrePlays);
        }
    }
}
