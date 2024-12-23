using FluentValidation;

public class UpdateArticleCommandValidator : AbstractValidator<BookmarkCommand>
{
    public UpdateArticleCommandValidator() 
        => Include(new BookmarkCommandValidator());
}