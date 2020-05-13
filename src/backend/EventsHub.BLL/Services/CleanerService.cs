using EventsHub.Cleaner;
using FluentScheduler;

namespace EventsHub.BLL.Services
{
    public class CleanerService : IJob
    {
        private readonly CleanerRunner cleanerRunner;

        public CleanerService()
        {
            cleanerRunner = new CleanerRunner();
        }

        public void Execute()
        {
            cleanerRunner.Run();
        }
    }
}
