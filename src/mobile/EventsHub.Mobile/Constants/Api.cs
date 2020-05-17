namespace EventsHub.Mobile.Constants
{
    public class Api
    {
        public const string GetFilms = "film?pageNumber={0}&pageSize={1}";
        public const string GetFilmsCount = "film/count";

        public const string GetPlays = "theatre?pageNumber={0}&pageSize={1}";
        public const string GetPlaysCount = "theatre/count";

        public const string GetConcerts = "concert?pageNumber={0}&pageSize={1}";
        public const string GetConcertsCount = "concert/count";
    }
}
