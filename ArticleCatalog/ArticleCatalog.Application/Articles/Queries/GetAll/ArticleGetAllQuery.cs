using MediatR;

public class ArticleGetAllQuery : IRequest<List<ArticleResponse>>
{
    public class ArticleAllQueryHandler(
        IArticleQueryRepository articleRepository,
        IAuthorsHttpService authorsHttpService
        ) : IRequestHandler<ArticleGetAllQuery, List<ArticleResponse>>
    {
        public async Task<List<ArticleResponse>> Handle(
            ArticleGetAllQuery request, 
            CancellationToken cancellationToken)
        {
            var articles = await articleRepository.GetAll(cancellationToken);
            var authors = await authorsHttpService.GetAll(cancellationToken);

            articles.ForEach(article =>
            {
                article.Author = authors.FirstOrDefault(a => a.Id == article.AuthorId)?.FirstName ?? "ND";
            });

            return articles;
        }
    }
}