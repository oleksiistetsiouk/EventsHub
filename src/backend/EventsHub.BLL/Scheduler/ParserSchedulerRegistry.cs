using EventsHub.BLL.Services;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;

namespace EventsHub.BLL.Scheduler
{
    class ParserSchedulerRegistry : Registry
    {
        public ParserSchedulerRegistry(IServiceScope service)
        {
            Schedule(service.ServiceProvider.GetRequiredService<ParserSchedulerService>())
                .ToRunEvery(20)
                .Seconds();
        }
    }
}
