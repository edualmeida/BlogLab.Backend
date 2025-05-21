using Comments.Application.Comments.Commands.Common;
using FluentValidation;

namespace Comments.Application.Comments.Commands.Update;
public class UpdateCommentCommandValidator : AbstractValidator<CommentCommand>
{
    public UpdateCommentCommandValidator() 
        => Include(new CommentCommandValidator());
}