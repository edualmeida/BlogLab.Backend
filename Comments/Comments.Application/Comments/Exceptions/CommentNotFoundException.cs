using Common.Application.Exceptions;

namespace Comments.Application.Comments.Exceptions;

public class CommentNotFoundException(Guid articleId): 
    NotFoundException("Comment", articleId.ToString())
{
    
}