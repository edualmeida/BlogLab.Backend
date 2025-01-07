public class Article : Entity, IAggregateRoot
{
    internal Article(
        string title,
        string subtitle,
        string text,
        Guid categoryId,
        Guid colorId,
        Guid thumbnailId,
        Guid authorId)
    {
        Validate(title, subtitle, text);
        Title = title;
        Subtitle = subtitle;
        Text = text;
        CategoryId = categoryId;
        ColorId = colorId;
        ThumbnailId = thumbnailId;
        AuthorId = authorId;
    }

    public string Title { get; private set; }
    public string Subtitle { get; private set; }
    public string Text { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid ColorId { get; private set; }
    public Guid ThumbnailId { get; private set; }
    public Guid AuthorId { get; private set; }

    public virtual Category? Category { get; set; }
    public virtual Color? Color { get; set; }
    public virtual Thumbnail? Thumbnail { get; set; }

    public Article UpdateTitle(string title)
    {
        ValidateTitle(title);
        Title = title;
        return this;
    }

    public Article UpdateSubtitle(string subtitle)
    {
        ValidateSubtitle(subtitle);
        Subtitle = subtitle;
        return this;
    }

    public Article UpdateText(string text)
    {
        ValidateText(text);
        Text = text;
        return this;
    }

    public Article UpdateCategory(Guid categoryId)
    {
        CategoryId = categoryId;
        return this;
    }

    public Article UpdateColor(Guid colorId)
    {
        ColorId = colorId;
        return this;
    }

    public Article UpdateThumbnail(Guid thumbnailId)
    {
        ThumbnailId = thumbnailId;
        return this;
    }

    public Article DisableArticle()
    {
        Enabled = false;
        return this;
    }

    public Article UpdateAuthorId(Guid authorId)
    {
        AuthorId = authorId;
        return this;
    }

    private void Validate(string title, string subtitle, string text)
    {
        ValidateTitle(title);
        ValidateSubtitle(subtitle);
        ValidateText(text);
    }

    private void ValidateTitle(string title)
        => Guard.ForStringLength(title, ArticleModelConstants.Article.MinTitleLength, ArticleModelConstants.Article.MaxTitleLength, nameof(Title));

    private void ValidateSubtitle(string subtitle)
        => Guard.ForStringLength(subtitle, ArticleModelConstants.Article.MinTextLength, ArticleModelConstants.Article.MaxTextLength, nameof(Subtitle));

    private void ValidateText(string text)
        => Guard.ForStringLength(text, ArticleModelConstants.Article.MinTextLength, ArticleModelConstants.Article.MaxTextLength, nameof(Text));
}
