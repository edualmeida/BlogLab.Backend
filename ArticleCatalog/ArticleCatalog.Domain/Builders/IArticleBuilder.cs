public interface IArticleBuilder : IBuilder<Article>
{
    IArticleBuilder WithTitle(string title);
    IArticleBuilder WithSubtitle(string subtitle);
    IArticleBuilder WithText(string text);
    IArticleBuilder WithThumbnailId(Guid thumbnailId);
    IArticleBuilder WithCategoryId(Guid categoryId);
    IArticleBuilder WithColorId(Guid colorId);
}