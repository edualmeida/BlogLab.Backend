using Bookmarks.Application.Services.Contracts.Articles;
using Common.Application.Mapping;

namespace Bookmarks.Application.Bookmarks.Queries.Common;
public class BookmarkArticleQueryResponse: IMapFrom<HttpArticleResponse>
{
    public required BookmarkQueryResponse Bookmark { get; set; }
    public required ArticleResponse Article { get; set; }
}