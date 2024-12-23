internal class ArticleBuilder : IArticleBuilder
{
    private string articleTitle = default!;
    private string articleSubtitle = default!;
    private string articleText = default!;
    private Guid articleThumbnail = default!;
    private Guid articleCategory = default!;
    private Guid articleColor = default!;
    private bool articleEnabled = true;
    private DateTime articleCreatedOn = DateTime.UtcNow;

    private bool isTitleSet = false;
    private bool isColorSet = false;
    private bool isSubtitleSet = false;
    private bool isTextSet = false;
    private bool isCategorySet = false;
    private bool isThumbnailSet = false;

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

    public IArticleBuilder WithColorId(Guid color)
    {
        this.articleColor = color;
        isColorSet = true;

        return this;
    }

    public Article Build()
    {
        if (!isColorSet || !isCategorySet || !isTitleSet || !isThumbnailSet || !isSubtitleSet || !isTextSet)
            throw new InvalidOperationException("subtitle, text, title, thumbnail, must have a value.");

        return new Article(
            articleTitle,
            articleSubtitle,
            articleText,
            articleCategory,
            articleColor,
            articleThumbnail,
            articleEnabled,
            articleCreatedOn);
    }
}