using Bookmarks.Application.Services.Contracts.Articles;

namespace Bookmarks.Application.Bookmarks.Queries.Common;
public class BookmarkArticleQueryResponse: IMapFrom<HttpArticleResponse>
{
    public BookmarkQueryResponse Bookmark { get; set; }
    public ArticleResponse Article { get; set; }
}