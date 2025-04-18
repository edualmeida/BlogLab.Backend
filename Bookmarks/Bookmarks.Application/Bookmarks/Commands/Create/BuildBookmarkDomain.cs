using Bookmarks.Domain.Factories;
using Bookmarks.Domain.Models.Bookmarks;
using Common.Application.Contracts;
using MediatR;

namespace Bookmarks.Application.Bookmarks.Commands.Create;
public class BuildBookmarkDomain : IRequest<Bookmark>
{
    public Guid ArticleId { get; set; }

    public class BuildBookmarkDomainHandler(
        ICurrentUserService currentUserService,
        IBookmarkFactory bookmarkFactory)
        : IRequestHandler<BuildBookmarkDomain, Bookmark>
    {
        public async Task<Bookmark> Handle(
            BuildBookmarkDomain request,
            CancellationToken cancellationToken)
        {
            var bookmark = bookmarkFactory
                .WithArticleId(request.ArticleId)
                .WithUserId(currentUserService.GetRequiredUserId())
                .Build();

            return bookmark;
        }
    }
}