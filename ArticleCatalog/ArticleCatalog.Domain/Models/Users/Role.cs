public class Role : Entity
{
    public Role(string name)
    {
        Validate(name);
        Name = name;
        Users = new HashSet<User>();

    }

    public string Name { get; private set; }
    public HashSet<User> Users { get; private set; }

    private void Validate(string name)
        => Guard.ForStringLength(name, UserModelConstants.Role.MinNameLength, UserModelConstants.Role.MaxNameLength, nameof(Name));
}