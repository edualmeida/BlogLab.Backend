public class LoginResponseModel
{
    public LoginResponseModel(string token) => Token = token;

    public string Token { get; }
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public bool IsAdmin { get; set; }
}