public class ApplicationSettings
{
    public ApplicationSettings() => PrivateKey = default!;

    public string PrivateKey { get; private set; }
}