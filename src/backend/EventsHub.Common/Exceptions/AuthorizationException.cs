using Common.Exceptions;

namespace EventsHub.Common.Exceptions
{
    public class AuthorizationException : ApiException
    {
        public AuthorizationException() : base()
        {

        }

        public AuthorizationException(string message, params object[] args) : base(message, args)
        {

        }
    }
}
