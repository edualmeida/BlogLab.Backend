using FluentValidation;

namespace Comments.Application.Comments.Commands.Delete;
public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}