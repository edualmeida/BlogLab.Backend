namespace ArticleCatalog.Application.Articles.Exceptions;

public class ArticleNotFoundException(Guid articleId): 
    Exception($"Article not found: {articleId}")
{
    
}