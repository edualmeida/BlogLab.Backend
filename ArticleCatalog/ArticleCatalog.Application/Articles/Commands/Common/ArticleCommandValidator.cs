using ArticleCatalog.Domain.Models.Articles;
using FluentValidation;

namespace ArticleCatalog.Application.Articles.Commands.Common;
public class ArticleCommandValidator : AbstractValidator<ArticleCommand>
{
    public ArticleCommandValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(ArticleModelConstants.MinTitleLength, 1)
            .WithMessage($"Title must be between {ArticleModelConstants.MinTitleLength} and {ArticleModelConstants.MaxTitleLength} characters.");

        RuleFor(b => b.Subtitle)
            .NotEmpty().WithMessage("Subtitle is required.")
            .Length(ArticleModelConstants.MinSubtitleLength, ArticleModelConstants.MaxSubtitleLength)
            .WithMessage($"Subtitle must be between {ArticleModelConstants.MinTitleLength} and {ArticleModelConstants.MaxTitleLength} characters.");

        RuleFor(b => b.Text)
            .NotEmpty().WithMessage("Text is required.")
            .Length(ArticleModelConstants.MinTextLength, ArticleModelConstants.MaxTextLength)
            .WithMessage($"Text must be between {ArticleModelConstants.MinTitleLength} and {ArticleModelConstants.MaxTitleLength} characters.");

        RuleFor(b => b.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");
    }
}