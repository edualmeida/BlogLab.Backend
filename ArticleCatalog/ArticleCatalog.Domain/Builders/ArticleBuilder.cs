using ArticleCatalog.Domain.Models.Articles;

namespace ArticleCatalog.Domain.Builders;
internal class ArticleBuilder : IArticleBuilder
{
    private string articleTitle = default!;
    private string articleSubtitle = default!;
    private string articleText = default!;
    private Guid articleThumbnail = default!;
    private Guid articleCategory = default!;
    private Guid articleAuthorId = default!;

    private bool isTitleSet = false;
    private bool isSubtitleSet = false;
    private bool isTextSet = false;
    private bool isCategorySet = false;
    private bool isThumbnailSet = false;
    private bool isAuthorSet = false;

    public IArticleBuilder WithTitle(string title)
    {
        articleTitle = title;
        isTitleSet = true;

        return this;
    }

    public IArticleBuilder WithSubtitle(string subtitle)
    {
        articleSubtitle = subtitle;
        isSubtitleSet = true;

        return this;
    }

    public IArticleBuilder WithText(string text)
    {
        articleText = text;
        isTextSet = true;

        return this;
    }

    public IArticleBuilder WithCategoryId(Guid category)
    {
        articleCategory = category;
        isCategorySet = true;

        return this;
    }

    public IArticleBuilder WithThumbnailId(Guid thumbnail)
    {
        this.articleThumbnail = thumbnail;
        isThumbnailSet = true;

        return this;
    }

    public IArticleBuilder WithAuthorId(Guid authorId)
    {
        this.articleAuthorId = authorId;
        isThumbnailSet = true;

        return this;
    }

    public Article Build()
    {
        if (!isCategorySet || !isTitleSet || !isThumbnailSet || !isSubtitleSet || !isTextSet || isAuthorSet)
            throw new InvalidOperationException("subtitle, text, title, thumbnail, author must have a value.");

        return new Article(
            articleTitle,
            articleSubtitle,
            articleText,
            articleCategory,
            articleThumbnail,
            articleAuthorId);
    }
}