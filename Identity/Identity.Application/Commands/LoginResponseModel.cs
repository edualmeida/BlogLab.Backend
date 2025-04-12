
namespace Identity.Application.Commands;
public class LoginResponseModel
{
    public LoginResponseModel(string token) => Token = token;

    public string Token { get; }
    public required Guid UserId { get; set; }
    public required string Username { get; set; }
    public required bool IsAdmin { get; set; }
}