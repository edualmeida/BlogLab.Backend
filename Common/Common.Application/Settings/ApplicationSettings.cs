public class ApplicationSettings
{
    public ApplicationSettings() => JwtPrivateKey = default!;

    public string JwtPrivateKey { get; private set; }
}