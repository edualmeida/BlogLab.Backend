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

    public class BuildArticleDomainHandler(IArticlesDomainRepository articleRepository) 
        : IRequestHandler<BuildArticleDomain, Result<Article>>
    {
        public async Task<Result<Article>> Handle(
            BuildArticleDomain request, 
            CancellationToken cancellationToken)
        {
            var domainArticle = await articleRepository.Find(request.Id, cancellationToken);

            if(domainArticle is null)
            {
                return Result<Article>.Failure(new ArticleNotFoundException(request.Id).Message);
            }

            domainArticle.UpdateTitle(request.Article.Title);
            domainArticle.UpdateSubtitle(request.Article.Subtitle);
            domainArticle.UpdateText(request.Article.Text);
            domainArticle.UpdateCategory(request.Article.CategoryId);
            domainArticle.UpdateThumbnail(new Guid("01965f47-83db-7f38-bce5-8c1b8e44ce4a"));

            return domainArticle;
        }
    }
}