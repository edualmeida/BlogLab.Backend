using FluentValidation;

public class CreateBikeCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateBikeCommandValidator()
        => Include(new ArticleCommandValidator());
}