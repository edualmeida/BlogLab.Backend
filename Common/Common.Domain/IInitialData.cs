public interface IInitialData
{
    List<object> GetThumbnails(IEnumerable<string> names);
    List<object> GetColors(IEnumerable<string> names);
    List<object> GetCategories(IEnumerable<string> names);
    object GetArticle(
        Guid thumbnailId,
        Guid colorId,
        Guid categoryId,
        string title,
        string subtitle,
        string text);
}