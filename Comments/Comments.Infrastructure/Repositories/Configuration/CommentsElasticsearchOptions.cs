using Common.Infrastructure.Repositories.Configuration;

namespace Comments.Infrastructure.Repositories.Configuration;
public sealed class CommentsElasticsearchOptions: IElasticsearchOptions
{
    public string NodeUri { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string IndexName { get; set; } = string.Empty;
}
