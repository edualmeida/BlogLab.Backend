using FluentValidation;

namespace ArticleCatalog.Application.Articles.Commands.Delete;
public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
{
    public DeleteArticleCommandValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}