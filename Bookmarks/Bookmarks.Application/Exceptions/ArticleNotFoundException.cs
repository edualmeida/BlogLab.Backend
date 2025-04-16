using Common.Application.Exceptions;

namespace Bookmarks.Application.Exceptions;
public class ArticleNotFoundException(Guid articleId) :
    NotFoundException("Article", articleId.ToString())
{

}
