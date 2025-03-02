using Bookmarks.Application.Bookmarks.Commands.Common;
using FluentValidation;

namespace Bookmarks.Application.Bookmarks.Commands.Update;
public class UpdateArticleCommandValidator : AbstractValidator<BookmarkCommand>
{
    public UpdateArticleCommandValidator() 
        => Include(new BookmarkCommandValidator());
}