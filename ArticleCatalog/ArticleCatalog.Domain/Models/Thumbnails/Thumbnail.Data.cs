public class ThumbnailData : IInitialData
{
    public Type EntityType => typeof(Thumbnail);

    public IEnumerable<object> GetData()
    {
        var t1 = new Thumbnail("Default");

        return [t1];
    }
}
