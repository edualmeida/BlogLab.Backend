using FluentValidation;

public class UpdateArticleCommandValidator : AbstractValidator<ArticleCommand>
{
    public UpdateArticleCommandValidator() 
        => Include(new ArticleCommandValidator());
}