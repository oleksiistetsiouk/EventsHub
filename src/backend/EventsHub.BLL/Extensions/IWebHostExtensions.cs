using EventsHub.BLL.Scheduler;
using FluentScheduler;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EventsHub.BLL.Extensions
{
    public static class IWebHostExtensions
    {
        public static IWebHost StartScheduler(this IWebHost host)
        {
            var scope = host.Services.CreateScope();
            JobManager.Initialize(new ParserSchedulerRegistry(scope));

            return host;
        }
    }
}
