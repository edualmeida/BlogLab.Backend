namespace ArticleCatalog.Application.Articles.Exceptions;

public class AuthorNotFoundException(Guid authorId): 
    Exception($"Author {authorId} not found.")
{
    
}