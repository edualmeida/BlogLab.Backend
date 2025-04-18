using Bookmarks.Application.Bookmarks.Commands.Common;
using FluentValidation;

namespace Bookmarks.Application.Bookmarks.Commands.Create;
public class CreateBookmarkCommandValidator : AbstractValidator<BookmarkCommand>
{
    public CreateBookmarkCommandValidator()
        => Include(new BookmarkCommandValidator());
}
