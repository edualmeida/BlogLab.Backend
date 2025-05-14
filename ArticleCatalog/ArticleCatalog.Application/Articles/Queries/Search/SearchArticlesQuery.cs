using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Articles.Queries.GetByIds;
using ArticleCatalog.Domain.Repositories;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetById;

public class SearchArticlesQuery : IRequest<List<ArticleQueryResponse>>
{
    public string Query { get; set; } = "";
    public class SearchArticlesQueryHandler(
        IMediator mediator,
        IElasticArticleRepository repository) : 
        IRequestHandler<SearchArticlesQuery, List<ArticleQueryResponse>>
    {
        public async Task<List<ArticleQueryResponse>> Handle(
            SearchArticlesQuery request,
            CancellationToken cancellationToken)
        {
            var response = await repository.SearchArticlesAsync(request.Query);

            if(!response.Any())
            {
                return new List<ArticleQueryResponse>();
            }

            var articles = await mediator.Send(new ArticleGetByIdsQuery { 
                ArticleIds = response.ToList(),
            }, cancellationToken);

            return articles;
        }
    }
}