using Bookmarks.Application.Bookmarks.Commands.Common;
using FluentValidation;

namespace Bookmarks.Application.Bookmarks.Commands.Delete;
public class DeleteArticleCommandValidator : AbstractValidator<BookmarkCommand>
{
    public DeleteArticleCommandValidator() 
        => Include(new BookmarkCommandValidator());
}