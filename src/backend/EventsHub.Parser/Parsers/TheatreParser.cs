using EventsHub.Common.Exceptions;
using HtmlAgilityPack;
using System;
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
            try
            {
                var web = new HtmlWeb();
                var doc = await web.LoadFromWebAsync("https://lviv-online.com/ua/events/theatre/");
                var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'page_navigation')]");
            }
            catch (ParserException ex)
            {
                throw new ParserException(nameof(TheatreParser), ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(TheatreParser)}: {ex.Message}");
            }
        }
    }
}
