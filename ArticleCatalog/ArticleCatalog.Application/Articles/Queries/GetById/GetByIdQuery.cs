using ArticleCatalog.Application.Articles.Queries.Common;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetById;

public class GetByIdQuery : EntityCommand, IRequest<ArticleQueryResponse>
{
    public class ArticleDetailsQueryHandler(
        IMediator mediator) : IRequestHandler<GetByIdQuery, ArticleQueryResponse>
    {
        public async Task<ArticleQueryResponse> Handle(
            GetByIdQuery request,
            CancellationToken cancellationToken)
        {
            var article = await mediator.Send(new GetRepositoryArticle { ArticleId = request.Id }, cancellationToken);

            article.Author = await mediator.Send(new GetAuthorName { AuthorId = article.AuthorId }, cancellationToken);
            article.IsBookmarked = await mediator.Send(new GetIsBookmarked { ArticleId = article.Id }, cancellationToken);

            return article;
        }
    }
}