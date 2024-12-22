public interface IArticleFactory : IFactory<Article>
{
    IArticleFactory WithTitle(string title);
    IArticleFactory WithSubtitle(string subtitle);
    IArticleFactory WithText(string text);
    IArticleFactory WithThumbnailId(Guid thumbnailId);
    IArticleFactory WithCategoryId(Guid categoryId);
    IArticleFactory WithColorId(Guid colorId);
}