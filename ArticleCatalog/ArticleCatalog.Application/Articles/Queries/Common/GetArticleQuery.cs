using ArticleCatalog.Application.Articles.Queries;
using ArticleCatalog.Application.Articles.Queries.Common;
using MediatR;

namespace ArticleCatalog.Application.Articles.Exceptions;
public class GetArticleQuery : IRequest<ArticleQueryResponse>
{
    public Guid ArticleId { get; set; }
    public class GetArticleQueryHandler(
        IArticleQueryRepository articleRepository) : IRequestHandler<GetArticleQuery, ArticleQueryResponse>
    {
        public async Task<ArticleQueryResponse> Handle(
            GetArticleQuery request,
            CancellationToken cancellationToken)
        {
            return await articleRepository.GetById(request.ArticleId, cancellationToken) ??
                throw new ArticleNotFoundException(request.ArticleId);
        }
    }

}
