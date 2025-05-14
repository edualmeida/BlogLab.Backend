namespace ArticleCatalog.Application.Articles.Exceptions;

public class ElasticArticleNotCreatedException(Guid articleId): 
    Exception($"Entity Article ({articleId}) was not created in Elasticsearch.")
{
    
}