using ArticleCatalog.Application.Articles.Queries.Common;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetByIds;

public class ArticleGetByIdsQuery : IRequest<List<ArticleQueryResponse>>
{
    public List<Guid> Ids { get; set; } = [];
    
    public class ArticleGetByIdsQueryHandler(
        IArticleQueryRepository articleRepository,
        IAuthorsHttpService authorsHttpService) : IRequestHandler<ArticleGetByIdsQuery, List<ArticleQueryResponse>>
    {
        public async Task<List<ArticleQueryResponse>> Handle(
            ArticleGetByIdsQuery request,
            CancellationToken cancellationToken)
        {
            var articles = await articleRepository.GetByIds(request.Ids, cancellationToken);
            var authors = await authorsHttpService.GetAll(cancellationToken);
            var response = new List<ArticleQueryResponse>();

            foreach (var article in articles)
            {
                article.Author = authors.First(a => a.Id == article.AuthorId).FirstName;
                response.Add(article);
            }
            
            return response;
        }
    }
}