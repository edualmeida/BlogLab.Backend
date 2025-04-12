using ArticleCatalog.Domain.Models.Categories;
using ArticleCatalog.Domain.Models.Thumbnails;

namespace ArticleCatalog.Infrastructure.Persistence;
internal class ArticleCatalogDbInitializer : DbInitializer
{
    public ArticleCatalogDbInitializer(ArticleCatalogDbContext db)
        : base(db, [new CategoryData(), new ThumbnailData()]) { }

}
