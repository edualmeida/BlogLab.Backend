using FluentValidation;

namespace Bookmarks.Application.Bookmarks.Commands.Common;
public class BookmarkCommandValidator : AbstractValidator<BookmarkCommand>
{
    public BookmarkCommandValidator()
    {
    }
}
