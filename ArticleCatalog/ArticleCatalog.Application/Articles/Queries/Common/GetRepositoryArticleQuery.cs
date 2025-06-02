using ArticleCatalog.Application.Articles.Exceptions;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.Common;
internal sealed class GetRepositoryArticleQuery : IRequest<ArticleQueryResponse>
{
    public Guid ArticleId { get; set; }

    public class GetArticleQueryHandler(
        IArticlesQueryRepository articleRepository) : 
        IRequestHandler<GetRepositoryArticleQuery, ArticleQueryResponse>
    {
        public async Task<ArticleQueryResponse> Handle(
            GetRepositoryArticleQuery request,
            CancellationToken cancellationToken)
        {
            return await articleRepository.GetById(request.ArticleId, cancellationToken) ??
                throw new ArticleNotFoundException(request.ArticleId);
        }
    }

}
