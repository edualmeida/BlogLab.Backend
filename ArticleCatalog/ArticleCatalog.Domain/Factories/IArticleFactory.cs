public interface IArticleFactory : IFactory<Article>
{
    IArticleFactory WithTitle(string title);
    IArticleFactory WithSubtitle(string subtitle);
    IArticleFactory WithText(string text);
    IArticleFactory WithThumbnail(Guid thumbnailId);
    IArticleFactory WithCategory(Guid categoryId);
    IArticleFactory WithColor(Guid colorId);
}