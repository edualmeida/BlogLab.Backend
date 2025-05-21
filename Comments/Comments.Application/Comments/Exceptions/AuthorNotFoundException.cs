using Common.Application.Exceptions;

namespace Comments.Application.Comments.Exceptions;

public class AuthorNotFoundException(Guid authorId):
    NotFoundException("Author", authorId.ToString())
{
    
}