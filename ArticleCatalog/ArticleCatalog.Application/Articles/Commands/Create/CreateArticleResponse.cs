namespace ArticleCatalog.Application.Articles.Commands.Create;
public class CreateArticleResponse
{
    internal CreateArticleResponse(Guid id) => Id = id;

    public Guid Id { get; }
}