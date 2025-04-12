using AutoMapper;
using Bookmarks.Application.Services.Contracts.Articles;

namespace Bookmarks.Application.Bookmarks.Queries.Common;

public class ArticleResponse: IMapFrom<HttpArticleResponse>
{
    public Guid Id { get; set; }
    public string Category { get; set; } = "";
    public string Thumbnail { get; set; } = "";
    public DateTime CreatedOn { get; set; }
    public string Author { get; set; } = "";
    public Guid AuthorId { get; set; }
    
    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<HttpArticleResponse, ArticleResponse>();
}