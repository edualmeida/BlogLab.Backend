using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Services;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetById;

public class ArticleGetByIdQuery : EntityCommand, IRequest<ArticleQueryResponse>
{
    public class ArticleDetailsQueryHandler(
        IArticleQueryRepository articleRepository,
        IAuthorsHttpService authorsHttpService) : IRequestHandler<ArticleGetByIdQuery, ArticleQueryResponse>
    {
        public async Task<ArticleQueryResponse> Handle(
            ArticleGetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var article = await articleRepository.GetById(request.Id, cancellationToken);
            article.Author = (await authorsHttpService.GetById(article.AuthorId, cancellationToken)).FirstName;
            
            return article;
        }
    }
}