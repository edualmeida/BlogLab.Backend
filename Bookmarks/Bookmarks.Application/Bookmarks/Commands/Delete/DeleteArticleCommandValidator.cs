using FluentValidation;

public class DeleteArticleCommandValidator : AbstractValidator<BookmarkCommand>
{
    public DeleteArticleCommandValidator() 
        => Include(new BookmarkCommandValidator());
}