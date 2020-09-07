using Common.Exceptions;

namespace EventsHub.Common.Exceptions
{
    public class CleanerException : ApiException
    {
        public CleanerException(string parserName, string message) : base("Cleaner: {0}: {1}",
            parserName, message)
        {

        }
    }
}