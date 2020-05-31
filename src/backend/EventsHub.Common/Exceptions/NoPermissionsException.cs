namespace Common.Exceptions
{
    public class NoPermissionsException : ApiException
    {
        public NoPermissionsException(string message, params object[] args) : base(message, args)
        {
        }
    }
}
