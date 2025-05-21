using Comments.Application.Comments.Commands.Common;
using FluentValidation;

namespace Comments.Application.Comments.Commands.Create.Validators;
public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
        => Include(new CommentCommandValidator());
}