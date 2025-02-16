public class CreateArticleRequest : ArticleCommand
{
    public CreateArticleCommand ToCommand(Guid authorId)
        => new CreateArticleCommand()
        {
            Title = Title,
            Subtitle = Subtitle,
            Text = Text,
            CategoryId = CategoryId,
            AuthorId = authorId
        };
}