using Common.Application.Exceptions;

namespace ArticleCatalog.Application.Articles.Exceptions;

public class CategoryNotFoundException(Guid categoryId): 
    NotFoundException("Category", categoryId.ToString())
{
    
}