using MediatR;

public class ArticleGetAllQuery : IRequest<GetAllResult>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public class ArticleAllQueryHandler(
        IArticleQueryRepository articleRepository,
        IAuthorsHttpService authorsHttpService
        ) : IRequestHandler<ArticleGetAllQuery, GetAllResult>
    {
        public async Task<GetAllResult> Handle(
            ArticleGetAllQuery request, 
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