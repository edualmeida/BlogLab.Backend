using FluentValidation;

public class UpdateBikeCommandValidator : AbstractValidator<BookmarkCommand>
{
    public UpdateBikeCommandValidator() 
        => Include(new BookmarkCommandValidator());
}