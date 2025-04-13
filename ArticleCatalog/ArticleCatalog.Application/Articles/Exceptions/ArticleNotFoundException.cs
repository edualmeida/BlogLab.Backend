using Common.Application.Exceptions;

namespace ArticleCatalog.Application.Articles.Exceptions;

public class ArticleNotFoundException(Guid articleId): 
    NotFoundException("Article", articleId.ToString())
{
    
}