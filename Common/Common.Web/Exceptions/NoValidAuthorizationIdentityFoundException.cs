
namespace Common.Web.Exceptions;
public class NoValidAuthorizationIdentityFoundException : Exception
{
    public NoValidAuthorizationIdentityFoundException(string message) : base(message)
    {
    }
}