using EventsHub.DAL.Entities.Film;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventsHub.Parser.Parsers
{
    public class FilmParser : IParser
    {
        private readonly string link;
        public List<Film> Films { get; set; }

        public FilmParser(string link)
        {
            this.link = link;
            Films = new List<Film>();
        }

        public void Parse()
        {
            var filmLinks = GetFilmLinks(link);
            ParseFilms(filmLinks);
        }

        private IEnumerable<string> GetFilmLinks(string link)
        {
            var web = new HtmlWeb();
            var doc = web.Load(link);
            var filmLinks = doc.DocumentNode.SelectNodes("//div[contains(@class, 'card__title')]")
                .Select(n => n.ChildNodes.ElementAt(1).Attributes.First().Value);

            return filmLinks;
        }

        private void ParseFilms(IEnumerable<string> filmLinks)
        {
            HtmlWeb web = new HtmlWeb();

            foreach (var filmLink in filmLinks)
            {
                var doc = web.Load(filmLink);

                var title = doc.DocumentNode.SelectNodes("//div[contains(@class, 'inner-title')]").ElementAt(0).InnerText.Trim();
                var info = doc.DocumentNode.SelectNodes("//div[contains(@class, 'info__text')]");

                if (info == null)
                {
                    //Debug.Write(title + "\n");
                    continue;
                }

                var genre = info.ElementAt(0).InnerText.Trim();
                var country = info.ElementAt(1).InnerText.Trim();
                var cast = info.ElementAt(2).InnerText.Trim();
                var year = info.ElementAt(3).InnerText.Trim();

                if (cast.All(char.IsDigit))
                {
                    var temp = cast;
                    cast = year;
                    year = temp;
                }

                if (cast.Length < 3)
                {
                    cast = null;
                }

                var director = info.ElementAt(4).InnerText.Trim();
                var description = doc.DocumentNode.SelectNodes("//div[contains(@class, 'film-paragraph')]").ElementAt(0).InnerText.Trim();
                var posterUrl = doc.DocumentNode.SelectNodes("//img[contains(@class, 'afisha-poster')]").First().Attributes.Last().Value;
                var trailerUrl = CreateYoutubeUrl(doc);

                var film = new Film
                {
                    Title = title,
                    Genre = genre,
                    Country = country,
                    Director = director,
                    Cast = cast,
                    Description = description,
                    PosterUrl = posterUrl,
                    TrailerUrl = trailerUrl,
                    Year = year
                };

                var cinemaNodes = doc.GetElementbyId("movie-schedule").ChildNodes.Where(n => isValid(n.Name));
                List<Cinema> cinemas = ParseCinemas(cinemaNodes, film);
            }
        }

        private List<Cinema> ParseCinemas(IEnumerable<HtmlNode> cinemaNodes, Film film)
        {
            List<Cinema> cinemas = new List<Cinema>();

            foreach (var cinemaNode in cinemaNodes)
            {
                var cinemaInfo = cinemaNode.ChildNodes.ElementAt(1).ChildNodes.Where(n => isValid(n.Name));
                var cinemaName = cinemaInfo.ElementAt(0).ChildNodes.ElementAt(1).ChildNodes.Where(n => isValid(n.Name)).First().InnerText.Trim();
                var cinemaAddress = cinemaInfo.ElementAt(0).ChildNodes.ElementAt(1).ChildNodes.Where(n => isValid(n.Name)).Last().InnerText.Trim();

                var cinema = new Cinema()
                {
                    CinemaName = cinemaName,
                    Address = cinemaAddress
                };

                var sessionsInfo = cinemaInfo.ElementAt(1).ChildNodes.ElementAt(1).ChildNodes.Where(n => isValid(n.Name));

                List<Session> sessions = ParseSessions(sessionsInfo, cinema, film);

                cinema.Sessions = sessions;
                cinemas.Add(cinema);
            }

            film.Cinemas = cinemas;
            Films.Add(film);

            return cinemas;
        }

        private List<Session> ParseSessions(IEnumerable<HtmlNode> sessionsInfo, Cinema cinema, Film film)
        {
            List<Session> sessions = new List<Session>();

            foreach (var sessionInfo in sessionsInfo)
            {
                string directLink = null;
                bool isShown = false;
                if (sessionInfo.Attributes.Count() == 3)
                {
                    if (cinema.CinemaName.Contains("Multiplex"))
                        directLink = CreateSessionLink(sessionInfo.Attributes.Last().Value, "Multiplex");
                    else if (cinema.CinemaName.Contains("Планета"))
                        directLink = CreateSessionLink(sessionInfo.Attributes.Last().Value, "Планета");
                    else
                        directLink = null;
                }
                else
                {
                    isShown = true;
                    directLink = null;
                }

                var priceInfo = ParseSessionPrice(sessionInfo.Attributes.ElementAt(1).Value);
                int priceFrom = priceInfo.Item1;
                int priceTo = priceInfo.Item2;

                var time = DateTime.Parse(sessionInfo.InnerText.Split('\n').ElementAt(0));

                string sessionType = null;
                if (sessionInfo.ChildNodes.Count() != 1)
                    sessionType = sessionInfo.InnerText.Split('\n').ElementAt(1);

                var session = new Session()
                {
                    Time = time,
                    DirectLink = directLink,
                    IsShown = isShown,
                    PriceFrom = priceFrom,
                    PriceTo = priceTo,
                    SessionType = sessionType,
                    Film = film,
                    Cinema = cinema
                };
                sessions.Add(session);
            }

            return sessions;
        }

        private string CreateSessionLink(string atrDate, string cinemaName)
        {
            var linkData = atrDate.Split(',', '.', '\\', '[', ']', '{', '}', '"', ':').Where(s => !string.IsNullOrEmpty(s));
            string ssid = linkData.ElementAt(3);
            string placeId = linkData.ElementAt(5); ;
            string eventId = linkData.ElementAt(7);

            if (cinemaName == "Multiplex")
            {
                return string.Format($"https://multiplex.ua/ua/movie/{eventId}?wcid={placeId}&ssid={ssid}");
            }
            else if (cinemaName == "Планета")
            {
                return string.Format($"https://pay.planetakino.ua/hall/{placeId}/{ssid}");
            }

            return null;
        }

        private (int, int) ParseSessionPrice(string str)
        {
            var res = str.Split(' ', '\n', '№', ',').Where(r => r.All(char.IsDigit) && !string.IsNullOrEmpty(r));
            int priceFrom = 0, priceTo = 0;
            if (res != null && res.Count() != 0)
            {
                if (res.Count() == 1)
                {
                    priceFrom = priceTo = Convert.ToInt32(res.ElementAt(0));

                    if (priceFrom < 40)
                        priceFrom = priceTo = 0;
                }
                else
                {
                    priceFrom = Convert.ToInt32(res.ElementAt(0));
                    priceTo = Convert.ToInt32(res.ElementAt(1));

                    if (priceFrom < 40)
                        priceFrom = 0;

                    if (priceTo < 40)
                        priceTo = 0;
                }
            }

            return (priceFrom, priceTo);
        }

        public static bool isValid(string text)
        {
            return !text.StartsWith("#");
        }

        private string CreateYoutubeUrl(HtmlDocument doc)
        {
            return "https://www.youtube.com/watch?v=" + doc.DocumentNode
                       .SelectNodes("//a[contains(@class, 'progressive-video__link')]").First().Attributes
                       .Last().Value.Split('/').Last();
        }
    }
}
