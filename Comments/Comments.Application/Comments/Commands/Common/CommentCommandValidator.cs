using Comments.Domain.Models.Comments;
using FluentValidation;

namespace Comments.Application.Comments.Commands.Common;
public class CommentCommandValidator : AbstractValidator<CommentCommand>
{
    public CommentCommandValidator()
    {
        RuleFor(b => b.Text)
            .NotEmpty().WithMessage("Text is required.")
            .Length(CommentModelConstants.MinTextLength, CommentModelConstants.MaxTextLength)
            .WithMessage($"Text must be between {CommentModelConstants.MinTextLength} and {CommentModelConstants.MaxTextLength} characters.");

        RuleFor(b => b.ArticleId)
            .NotEmpty().WithMessage("ArticleId is required.");

        RuleFor(b => b.AuthorId)
            .NotEmpty().WithMessage("AuthorId is required.");
    }
}