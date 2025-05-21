using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Application.Articles.Queries.GetById;
using ArticleCatalog.Domain.Models.Articles;
using ArticleCatalog.Domain.Repositories;
using Common.Application;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Create;
internal sealed class CreateElasticArticle(Article article) : IRequest<Result>
{
    public Article Article => article;

    public class CreateElasticArticleHandler(
        IMediator mediator,
        IElasticArticleRepository repository) : IRequestHandler<CreateElasticArticle, Result>
    {
        public async Task<Result> Handle(CreateElasticArticle request, CancellationToken cancellationToken)
        {
            var articleQueryResponse = await mediator
                .Send(new GetArticleByIdQuery() { Id = request.Article.Id }, cancellationToken);

            var response = await repository.CreateArticleAsync(new ElasticArticle()
            {
                Id = request.Article.Id,
                Title = request.Article.Title,
                Subtitle = request.Article.Subtitle,
                Text = request.Article.Text,
                Category = articleQueryResponse.Category,
                Author = articleQueryResponse.Author,
                CreatedOnUtc = request.Article.CreatedOnUtc,
            });

            if (!response)
            {
                return Result.Failure(new ElasticArticleNotCreatedException(request.Article.Id).Message);
            }

            return Result.Success;
        }
    }
}
