using FluentValidation;

public class DeleteBikeCommandValidator : AbstractValidator<BookmarkCommand>
{
    public DeleteBikeCommandValidator() 
        => Include(new BookmarkCommandValidator());
}