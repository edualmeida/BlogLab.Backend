using ArticleCatalog.Application.Articles.Commands.Common;
using FluentValidation;

namespace ArticleCatalog.Application.Articles.Commands.Update;
public class UpdateArticleCommandValidator : AbstractValidator<ArticleCommand>
{
    public UpdateArticleCommandValidator() 
        => Include(new ArticleCommandValidator());
}