using ArticleCatalog.Application.Articles.Queries.Common;
using ArticleCatalog.Application.Authors.Queries;
using ArticleCatalog.Application.Bookmarks.Queries;
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
        ILogger<ArticleAllQueryHandler> logger) : 
        IRequestHandler<GetArticlesPaginatedQuery, GetArticlesPaginatedResult>
    {
        public async Task<GetArticlesPaginatedResult> Handle(
            GetArticlesPaginatedQuery request, 
            CancellationToken cancellationToken)
        {
            // Custom metrics for the application
            var greeterMeter = new Meter("OtPrGrYa.Example", "1.0.0");
            var countGreetings = greeterMeter.CreateCounter<int>("greetings.count", description: "Counts the number of greetings");

            // Custom ActivitySource for the application
            var greeterActivitySource = new ActivitySource("OtPrGrJa.Example");

            using var activity = greeterActivitySource.StartActivity("GreeterActivity");

            //var userBookmarks = mediator.Send(new GetUserBookmarksQuery(), cancellationToken);
            //var authors = mediator.Send(new GetAuthorsQuery(), cancellationToken);
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
                article.Author = "ND";
                //authors.Result
                //    .FirstOrDefault(a => a.Id == article.AuthorId)?
                //    .FirstName ?? "ND";
            });

            // Log a message
            logger.LogInformation("Sending greeting");

            // Increment the custom counter
            countGreetings.Add(1);

            // Add a tag to the Activity
            activity?.SetTag("greeting", "Hello World!");

            return getResult.Result;
        }
    }
}