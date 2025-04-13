using ArticleCatalog.Application.Articles.Exceptions;
using ArticleCatalog.Application.Articles.Queries.Common;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetById;

public class ArticleGetByIdQuery : EntityCommand, IRequest<ArticleQueryResponse>
{
    public class ArticleDetailsQueryHandler(
        IMediator mediator) : IRequestHandler<ArticleGetByIdQuery, ArticleQueryResponse>
    {
        public async Task<ArticleQueryResponse> Handle(
            ArticleGetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var article = await mediator.Send(new GetArticleQuery { ArticleId = request.Id }, cancellationToken);

            article.Author = await mediator.Send(new GetAuthorNameQuery { AuthorId = article.AuthorId }, cancellationToken);
            article.IsBookmarked = await mediator.Send(new IsBookmarkedQuery { ArticleId = article.Id }, cancellationToken);

            return article;
        }
    }
}