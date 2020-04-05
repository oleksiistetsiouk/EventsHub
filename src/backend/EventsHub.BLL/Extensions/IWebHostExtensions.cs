using EventsHub.BLL.Scheduler;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventsHub.BLL.Extensions
{
    public static class IWebHostExtensions
    {
        public static IHost StartScheduler(this IHost host)
        {
            var scope = host.Services.CreateScope();
            JobManager.Initialize(new ParserSchedulerRegistry(scope));

            return host;
        }
    }
}
