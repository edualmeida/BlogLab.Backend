public class Color : Entity, IAggregateRoot
{
    internal Color(
        string name)
    {
        Validate(name);

        Name = name;
    }

    public string Name { get; private set; }

    private void Validate(string name)
    {
        ValidateName(name);
    }

    private void ValidateName(string name)
        => Guard.ForStringLength(name, 2, 50, nameof(Name));
}