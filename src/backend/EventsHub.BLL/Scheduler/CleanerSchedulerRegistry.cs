﻿using EventsHub.BLL.Services;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;

namespace EventsHub.BLL.Scheduler
{
    class CleanerSchedulerRegistry : Registry
    {
        public CleanerSchedulerRegistry(IServiceScope service)
        {
            Schedule(service.ServiceProvider.GetRequiredService<CleanerService>())
                .ToRunEvery(1)
                .Days()
                .At(1, 0);
        }
    }
}
