using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Authors.Queries;
using ArticleCatalog.Application.Bookmarks.Queries;
using Common.Domain.Telemetry;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace ArticleCatalog.Application.Articles.Queries.GetPaginated;
public class GetArticlesPaginatedQuery : IRequest<GetArticlesPaginatedResult>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public class ArticleAllQueryHandler(
        IMediator mediator,
        ILogger<ArticleAllQueryHandler> logger,
        IActivityScopeFactory activityScopeFactory) : 
        IRequestHandler<GetArticlesPaginatedQuery, GetArticlesPaginatedResult>
    {
        public async Task<GetArticlesPaginatedResult> Handle(
            GetArticlesPaginatedQuery request, 
            CancellationToken cancellationToken)
        {
            using var activity = activityScopeFactory.Start("GetArticlesPaginatedQuery.Handle");

            activity.AddTag("PageNumber", request.PageNumber.ToString());
            activity.AddTag("PageSize", request.PageSize.ToString());

            //var userBookmarks = mediator.Send(new GetUserBookmarksQuery(), cancellationToken);
            var authors = mediator.Send(new GetAuthorsQuery(), cancellationToken);
            var getResult = mediator.Send(new GetRepositoryArticlesQuery
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            }, cancellationToken);

            //await Task.WhenAll(getResult, userBookmarks, authors);
            await Task.WhenAll(getResult);
            getResult.Result.Articles.ForEach(article =>
            {
                article.IsBookmarked = false;//userBookmarks.Result.Any(x => x.Bookmark.ArticleId == article.Id);
                article.Author = authors.Result
                    .FirstOrDefault(a => a.Id == article.AuthorId)?
                    .FirstName ?? "ND";
            });

            // Log a message
            logger.LogInformation("GetArticlesPaginatedQuery finished");

            activity?.AddTag("articles.count", getResult.Result.Articles.Count.ToString());
            activity?.AddEvent("ArticlesPaginatedQueryCompleted");

            return getResult.Result;
        }
    }
}