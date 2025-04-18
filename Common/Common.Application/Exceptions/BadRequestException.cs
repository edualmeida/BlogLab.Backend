namespace Common.Application.Exceptions;
public class BadRequestException : Exception
{
    public BadRequestException(string name, object key)
        : base($"Entity '{name}' ({key}) already exist.")
    {
    }

    public BadRequestException(string message) :
        base(message)
    {
    }
}