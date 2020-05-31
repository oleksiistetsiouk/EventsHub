using Common.Exceptions;

namespace EventsHub.Common.Exceptions
{
    public class ParserException : ApiException
    {
        public ParserException(string parserName, string message) : base("Parser: {0}: {1}",
            parserName, message)
        {

        }
    }
}
