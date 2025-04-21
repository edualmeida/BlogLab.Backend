using ArticleCatalog.Application.Articles.Queries.GetPaginated;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.Common;
internal sealed class GetRepositoryArticlesQuery : IRequest<GetArticlesPaginatedResult>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public class GetRepositoryArticlesQueryHandler(
        IArticleQueryRepository articleRepository) : IRequestHandler<GetRepositoryArticlesQuery, GetArticlesPaginatedResult>
    {
        public async Task<GetArticlesPaginatedResult> Handle(
            GetRepositoryArticlesQuery request,
            CancellationToken cancellationToken)
        {
            var getResult = await articleRepository.GetAll(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            if(getResult == null)
            {
                return new GetArticlesPaginatedResult
                {
                    Articles = [],
                    TotalCount = 0,
                    TotalPages = 0
                };
            }

            return getResult;
        }
    }

}
