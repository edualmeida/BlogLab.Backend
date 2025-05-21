namespace Comments.Application.Comments.Exceptions;

public class ElasticArticleNotCreatedException(Guid articleId): 
    Exception($"Entity Comment ({articleId}) was not created in Elasticsearch.")
{
    
}