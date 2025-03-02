using Bookmarks.Application.Services.Contracts.Articles;

namespace Bookmarks.Application.Bookmarks.Queries.Common;
public class BookmarkArticleQueryResponse(
    BookmarkQueryResponse bookmark,
    ArticleQueryResponse article): IMapFrom<ArticleResponse>
{
    public BookmarkQueryResponse Bookmark => bookmark;
    public ArticleQueryResponse Article => article;
}