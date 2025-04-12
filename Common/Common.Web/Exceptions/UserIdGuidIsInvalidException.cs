namespace Common.Web.Exceptions;

public class UserIdGuidIsInvalidException(string? userIdGuid) : 
    Exception($"The GUID {userIdGuid} is not a valid user id")
{
}