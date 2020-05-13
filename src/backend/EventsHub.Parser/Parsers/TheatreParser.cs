using HtmlAgilityPack;
using System.Threading.Tasks;

namespace EventsHub.Parser.Parsers
{
    public class TheatreParser : IParser
    {
        private string link = "https://lviv-online.com/ua/events/theatre/page/1/";
        private int pageNumber;

        public TheatreParser()
        {
        }

        public async Task Parse()
        {
            var web = new HtmlWeb();
            var doc = web.Load("https://lviv-online.com/ua/events/theatre/");
            var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'page_navigation')]");
        }
    }
}
