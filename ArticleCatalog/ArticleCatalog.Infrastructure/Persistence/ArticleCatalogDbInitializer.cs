internal class ArticleCatalogDbInitializer : DbInitializer
{
    public ArticleCatalogDbInitializer(ArticleCatalogDbContext db)
        : base(db, [new CategoryData(), new ColorData(), new ThumbnailData()]) { }

}
