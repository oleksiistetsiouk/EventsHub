using EventsHub.BLL.Services;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;

namespace EventsHub.BLL.Scheduler
{
    class ParserSchedulerRegistry : Registry
    {
        public ParserSchedulerRegistry(IServiceScope service)
        {
            Schedule(service.ServiceProvider.GetRequiredService<ParserService>())
                .ToRunEvery(1)
                .Days()
                .At(1, 0);
        }
    }
}
