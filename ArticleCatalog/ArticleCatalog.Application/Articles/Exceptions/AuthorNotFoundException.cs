using Common.Application.Exceptions;

namespace ArticleCatalog.Application.Articles.Exceptions;

public class AuthorNotFoundException(Guid authorId):
    NotFoundException("Author", authorId.ToString())
{
    
}