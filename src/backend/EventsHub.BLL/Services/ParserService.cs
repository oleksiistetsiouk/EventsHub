using EventsHub.Parser;
using FluentScheduler;

namespace EventsHub.BLL.Services
{
    public class ParserService : IJob
    {
        private ParserRunner parserRunner;

        public ParserService()
        {
            parserRunner = new ParserRunner();
        }

        public async void Execute()
        {
            await parserRunner.Run();
        }
    }
}
