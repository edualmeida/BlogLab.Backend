using Common.Application.Exceptions;

namespace Comments.Application.Comments.Exceptions;

public class CategoryNotFoundException(Guid categoryId): 
    NotFoundException("Category", categoryId.ToString())
{
    
}