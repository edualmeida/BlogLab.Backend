namespace Common.Application.Contracts;
public interface ICurrentUserService
{
    Guid? GetUserId();
    Guid GetRequiredUserId();
}