using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Domain.Models.Articles;
using ArticleCatalog.Domain.Repositories;
using Common.Application;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Create.Validators;
internal sealed class PutArticleIndex(Article article) : IRequest<Result>
{
    public Article Article => article;

    public class PutArticleIndexHandler(
        IElasticArticleRepository repository) : IRequestHandler<PutArticleIndex, Result>
    {
        public async Task<Result> Handle(PutArticleIndex request, CancellationToken cancellationToken)
        {
            var response = await repository.CreateArticleAsync(request.Article);

            if (!response)
            {
                return Result.Failure(new ElasticArticleNotCreatedException(request.Article.Id).Message);
            }

            return Result.Success;
        }
    }
}
