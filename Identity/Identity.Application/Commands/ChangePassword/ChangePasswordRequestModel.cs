namespace Identity.Application.Commands.ChangePassword;
public class ChangePasswordRequestModel(
        Guid userId,
        string currentPassword,
        string newPassword)
{
    public Guid UserId { get; } = userId;

    public string CurrentPassword { get; } = currentPassword;

    public string NewPassword { get; } = newPassword;
}