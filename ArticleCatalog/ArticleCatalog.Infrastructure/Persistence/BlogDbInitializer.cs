using Microsoft.EntityFrameworkCore;
using System.Text.Json;

internal class BlogDbInitializer(
    BlogDbContext db,
    IInitialData initialData) : IDbInitializer
{
    internal class ArticleJson
    {
         public string title { get;  set; }
         public string subtitle { get;  set; }
         public string text { get;  set; }
         public string thumbnail { get;  set; }
    }

    public void Initialize()
    {
        if (db.Categories.Count() > 0) return;

        var articles = JsonSerializer.Deserialize<List<ArticleJson>>(InitialData.Data);

        db.Database.Migrate();

        var categoriesNames = articles.Select(x => x.text).Distinct().ToList();
        var categories = initialData.GetCategories(categoriesNames);

        foreach (var entity in categories)
        {
            db.Add(entity);
        }

        db.SaveChanges();

        foreach (var article in articles)
        {
            //db.Add(initialData.GetArticle(.Id, color.Id, category.Id, article.title, article.thumbnail));
        }

        db.SaveChanges();
    }
}

internal class InitialData
{
    public static string Data = "";
}
