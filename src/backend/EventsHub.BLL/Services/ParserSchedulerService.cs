using EventsHub.Parser;
using FluentScheduler;

namespace EventsHub.BLL.Services
{
    public class ParserSchedulerService : IJob
    {
        private ParserRunner parserRunner;

        public ParserSchedulerService()
        {
            parserRunner = new ParserRunner();
        }

        public async void Execute()
        {
            await parserRunner.Run();
        }
    }
}
