using ArticleCatalog.Application.Articles.Commands.Common;
using FluentValidation;

namespace ArticleCatalog.Application.Articles.Commands.Create.Validators;
public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
        => Include(new ArticleCommandValidator());
}