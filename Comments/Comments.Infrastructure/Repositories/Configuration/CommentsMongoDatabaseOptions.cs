using Common.Infrastructure.Repositories.Configuration;

namespace Comments.Infrastructure.Repositories.Configuration;
public class CommentsMongoDatabaseOptions : IMongoDatabaseOptions
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
}
