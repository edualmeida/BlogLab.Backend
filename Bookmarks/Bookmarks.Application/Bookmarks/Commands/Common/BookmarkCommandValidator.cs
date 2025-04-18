using FluentValidation;

namespace Bookmarks.Application.Bookmarks.Commands.Common;
public class BookmarkCommandValidator : AbstractValidator<BookmarkCommand>
{
    public BookmarkCommandValidator()
    {
        RuleFor(b => b.UserId)
            .NotEmpty().WithMessage("{UserId is required.")
            .WithMessage($"UserId must not be empty.");

        RuleFor(b => b.ArticleId)
            .NotEmpty().WithMessage("ArticleId is required.")
            .WithMessage($"ArticleId must not be empty.");
    }
}
