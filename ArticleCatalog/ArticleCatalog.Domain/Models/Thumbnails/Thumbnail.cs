public class Thumbnail : Entity, IAggregateRoot
{
    internal Thumbnail(
        string name)
    {
        Validate(name);

        Name = name;
        Articles = new HashSet<Article>();
    }

    public string Name { get; private set; }
    public HashSet<Article> Articles { get; private set; }

    private void Validate(string name)
    {
        ValidateName(name);
    }

    private void ValidateName(string name)
        => Guard.ForStringLength(name, ThumbnailsModelConstants.Thumbnail.MinNameLength, ThumbnailsModelConstants.Thumbnail.MaxNameLength, nameof(Name));
}