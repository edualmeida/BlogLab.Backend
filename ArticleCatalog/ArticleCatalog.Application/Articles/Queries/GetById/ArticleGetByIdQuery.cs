using MediatR;

public class ArticleGetByIdQuery : EntityCommand, IRequest<ArticleResponse>
{
    public class ArticleDetailsQueryHandler(
        IArticleQueryRepository articleRepository,
        IAuthorsHttpService authorsHttpService) : IRequestHandler<ArticleGetByIdQuery, ArticleResponse>
    {
        public async Task<ArticleResponse> Handle(
            ArticleGetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var article = await articleRepository.GetById(request.Id, cancellationToken);
            article.Author = (await authorsHttpService.GetById(article.AuthorId, cancellationToken)).FirstName;
            
            return article;
        }
    }
}