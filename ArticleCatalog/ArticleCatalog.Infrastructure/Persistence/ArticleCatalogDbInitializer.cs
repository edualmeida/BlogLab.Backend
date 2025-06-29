using ArticleCatalog.Domain.Models.Categories;
using ArticleCatalog.Domain.Models.Thumbnails;
using Common.Infrastructure.Persistence;

namespace ArticleCatalog.Infrastructure.Persistence;
public class ArticleCatalogDbInitializer : DbInitializer
{
    public ArticleCatalogDbInitializer(ArticleCatalogDbContext db)
        : base(db, [new CategoryData(), new ThumbnailData()], nameof(ArticleCatalogDbContext)) { }

}
