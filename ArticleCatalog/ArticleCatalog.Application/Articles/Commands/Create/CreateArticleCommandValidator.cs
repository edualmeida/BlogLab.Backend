using FluentValidation;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
        => Include(new ArticleCommandValidator());
}