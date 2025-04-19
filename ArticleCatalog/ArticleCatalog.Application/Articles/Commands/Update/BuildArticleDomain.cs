using ArticleCatalog.Application.Articles.Commands.Common;
using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Domain.Models.Articles;
using ArticleCatalog.Domain.Repositories;
using Common.Application;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Update;
public class BuildArticleDomain(Guid id, ArticleCommand articleCommand) 
    : IRequest<Result<Article>>
{
    public Guid Id => id;
    public ArticleCommand Article => articleCommand;

    public class BuildArticleDomainHandler(IArticleDomainRepository articleRepository) 
        : IRequestHandler<BuildArticleDomain, Result<Article>>
    {
        public async Task<Result<Article>> Handle(
            BuildArticleDomain updateArticleCommand, 
            CancellationToken cancellationToken)
        {
            var domainArticle = await articleRepository.Find(updateArticleCommand.Id, cancellationToken);

            if(domainArticle is null)
            {
                return Result<Article>.Failure(new ArticleNotFoundException(updateArticleCommand.Id).Message);
            }

            domainArticle.UpdateTitle(updateArticleCommand.Article.Title);
            domainArticle.UpdateSubtitle(updateArticleCommand.Article.Subtitle);
            domainArticle.UpdateText(updateArticleCommand.Article.Text);
            domainArticle.UpdateCategory(updateArticleCommand.Article.CategoryId);
            domainArticle.UpdateThumbnail(updateArticleCommand.Article.ThumbnailId);

            return domainArticle;
        }
    }
}