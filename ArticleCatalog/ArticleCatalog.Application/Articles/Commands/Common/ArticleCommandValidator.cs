using FluentValidation;

public class ArticleCommandValidator : AbstractValidator<ArticleCommand>
{
    public ArticleCommandValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(ArticleModelConstants.Article.MinTitleLength, ArticleModelConstants.Article.MaxTitleLength)
            .WithMessage($"Name must be between {ArticleModelConstants.Article.MinTitleLength} and {ArticleModelConstants.Article.MaxTitleLength} characters.");

        RuleFor(b => b.Subtitle)
            .NotEmpty().WithMessage("Subtitle is required.")
            .Length(ArticleModelConstants.Article.MinTitleLength, ArticleModelConstants.Article.MaxTitleLength)
            .WithMessage($"Name must be between {ArticleModelConstants.Article.MinTitleLength} and {ArticleModelConstants.Article.MaxTitleLength} characters.");

        RuleFor(b => b.Text)
            .NotEmpty().WithMessage("Text is required.")
            .Length(ArticleModelConstants.Article.MinTitleLength, ArticleModelConstants.Article.MaxTitleLength)
            .WithMessage($"Name must be between {ArticleModelConstants.Article.MinTitleLength} and {ArticleModelConstants.Article.MaxTitleLength} characters.");

        RuleFor(b => b.Category)
            .NotEmpty().WithMessage("CategoryId is required.");

        RuleFor(b => b.Color)
            .NotEmpty().WithMessage("ColorId is required.");

        RuleFor(b => b.Thumbnail)
            .NotEmpty().WithMessage("Thumbnail is required.");
    }
}