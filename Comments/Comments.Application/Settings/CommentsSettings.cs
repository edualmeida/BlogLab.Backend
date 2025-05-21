namespace Comments.Application.Settings;

public class CommentsSettings(
    AuthorsApiClientSettings authorsApiClientSettings)
{
    public AuthorsApiClientSettings AuthorsApiClientSettings { get; }
        = authorsApiClientSettings;
}