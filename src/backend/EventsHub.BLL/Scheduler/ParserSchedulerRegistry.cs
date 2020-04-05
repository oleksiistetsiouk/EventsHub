using EventsHub.BLL.Services;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EventsHub.BLL.Scheduler
{
    class ParserSchedulerRegistry : Registry
    {
        public ParserSchedulerRegistry(IServiceScope service)
        {
            Schedule(service.ServiceProvider.GetRequiredService<ParserSchedulerService>())
                .ToRunEvery(1)
                .Days()
                .At(0, 0);
        }
    }
}
