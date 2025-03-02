using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetAllPaginated;
public class ArticleGetAllPaginatedQuery : IRequest<ArticleGetAllPaginatedResult>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public class ArticleAllQueryHandler(
        IArticleQueryRepository articleRepository,
        IAuthorsHttpService authorsHttpService
        ) : IRequestHandler<ArticleGetAllPaginatedQuery, ArticleGetAllPaginatedResult>
    {
        public async Task<ArticleGetAllPaginatedResult> Handle(
            ArticleGetAllPaginatedQuery request, 
            CancellationToken cancellationToken)
        {
            var getResult = await articleRepository.GetAll(
                request.PageNumber, 
                request.PageSize, 
                cancellationToken);

            var authors = await authorsHttpService.GetAll(
                cancellationToken);

            getResult.Articles.ForEach(article =>
            {
                article.Author = authors
                    .FirstOrDefault(a => a.Id == article.AuthorId)?.FirstName ?? "ND";
            });

            return getResult;
        }
    }
}