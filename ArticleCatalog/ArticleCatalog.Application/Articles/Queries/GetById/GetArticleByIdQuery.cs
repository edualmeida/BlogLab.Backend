using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Authors.Queries;
using ArticleCatalog.Application.Bookmarks.Queries;
using MediatR;

namespace ArticleCatalog.Application.Articles.Queries.GetById;

public class GetArticleByIdQuery : EntityCommand, IRequest<ArticleQueryResponse>
{
    public class ArticleDetailsQueryHandler(IMediator mediator) : 
        IRequestHandler<GetArticleByIdQuery, ArticleQueryResponse>
    {
        public async Task<ArticleQueryResponse> Handle(
            GetArticleByIdQuery request,
            CancellationToken cancellationToken)
        {
            var article = await mediator.Send(new GetRepositoryArticleQuery { ArticleId = request.Id }, cancellationToken);

            article.Author = await mediator.Send(new GetAuthorNameQuery { AuthorId = article.AuthorId }, cancellationToken);
            article.IsBookmarked = await mediator.Send(new GetIsBookmarkedQuery { ArticleId = article.Id }, cancellationToken);

            return article;
        }
    }
}