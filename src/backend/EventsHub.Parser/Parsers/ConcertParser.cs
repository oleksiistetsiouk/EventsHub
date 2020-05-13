using EventsHub.DAL.Entities.Concert;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsHub.Parser.Parsers
{
    public class ConcertParser : IParser
    {
        public List<Concert> Concerts { get; set; }
        private readonly string link;
        private Dictionary<string, int> months;

        public ConcertParser()
        {
            link = "https://concert.ua/uk/catalog/lviv/concerts";
            Concerts = new List<Concert>();
            months = new Dictionary<string, int>()
            {
                { "січня", 1 },
                { "лютого", 2 },
                { "березня", 3 },
                { "квітня", 4 },
                { "травня", 5 },
                { "червня", 6 },
                { "липня", 7 },
                { "серпня", 8 },
                { "вересня", 9 },
                { "жовтня", 10 },
                { "листопада", 11 },
                { "грудня", 12 },
            };
        }

        public async Task Parse()
        {
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(link);

            var events = doc.DocumentNode.SelectNodes("//a[contains(@class, 'event')]");
            foreach (var @event in events)
            {
                var eventNodes = @event.ChildNodes.Where(n => n.Name != "#text");

                var title = eventNodes.ElementAt(1).ChildNodes.Where(n => n.Name != "#text").ElementAt(1).InnerText;
                var date = ConvertDate(eventNodes.ElementAt(1).ChildNodes.Where(n => n.Name != "#text").ElementAt(0).InnerText);
                var place = eventNodes.ElementAt(1).ChildNodes.Where(n => n.Name != "#text").ElementAt(2).InnerText.Trim();
                var price = ConvertPrice(eventNodes.ElementAt(1).ChildNodes.Where(n => n.Name != "#text").ElementAt(3).InnerText);
                var posterUrl = eventNodes.Where(n => n.Name == "img").First().Attributes.ElementAt(1).Value.Split(':').ElementAt(1).Substring(2);
                var directLink = "https://concert.ua" + @event.Attributes.ElementAt(1).Value;

                var concert = new Concert()
                {
                    Title = title,
                    Date = date,
                    Place = place,
                    Price = price,
                    PosterUrl = posterUrl,
                    DirectLink = directLink
                };

                Concerts.Add(concert);
            }
        }

        private int ConvertPrice(string priceString)
        {
            var priceInfo = priceString.Split(' ');
            int price = Convert.ToInt32(priceInfo.ElementAt(1));

            return price;
        }

        private DateTime ConvertDate(string dateString)
        {
            var dateInfo = dateString.Split('\n', ',').ElementAt(1).Split(' ');
            var date = DateTime.Parse($"{dateInfo[0]}/{months[dateInfo[1]]}/{DateTime.Now.Year} {dateInfo[2]}:00");

            return date;
        }
    }
}
