using HtmlAgilityPack;

namespace EventsHub.Parser.Parsers
{
    public class TheatreParser : IParser
    {
        private string link = "https://lviv-online.com/ua/events/theatre/page/1/";
        private int pageNumber;
        public TheatreParser()
        {
            var web = new HtmlWeb();
            var doc = web.Load("https://lviv-online.com/ua/events/theatre/");
            var n = doc.DocumentNode.SelectNodes("//div[contains(@class, 'page_navigation')]");
        }

        public void Parse()
        {
            throw new System.NotImplementedException();
        }
    }
}
