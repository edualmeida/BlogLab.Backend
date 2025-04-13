using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Application.Articles.Queries.Common;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetById;
internal sealed class GetRepositoryArticle : IRequest<ArticleQueryResponse>
{
    public Guid ArticleId { get; set; }
    public class GetArticleQueryHandler(
        IArticleQueryRepository articleRepository) : IRequestHandler<GetRepositoryArticle, ArticleQueryResponse>
    {
        public async Task<ArticleQueryResponse> Handle(
            GetRepositoryArticle request,
            CancellationToken cancellationToken)
        {
            return await articleRepository.GetById(request.ArticleId, cancellationToken) ??
                throw new ArticleNotFoundException(request.ArticleId);
        }
    }

}
