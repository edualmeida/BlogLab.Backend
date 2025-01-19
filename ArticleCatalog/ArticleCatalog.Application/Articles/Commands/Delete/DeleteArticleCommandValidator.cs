using FluentValidation;

public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
{
    public DeleteArticleCommandValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}