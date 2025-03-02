namespace ArticleCatalog.Domain.Models.Categories;
public class CategoryData : IInitialData
{
    public Type EntityType => typeof(Category);

    public IEnumerable<object> GetData()
    {
        var news = new Category("News");
        var designPatterns = new Category("Design Patterns");

        return [news, designPatterns];
    }
}
