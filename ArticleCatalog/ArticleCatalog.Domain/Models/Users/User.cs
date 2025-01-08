public class User : Entity, IAggregateRoot
{
    internal User(
        string email,
        string firstName,
        string middleName,
        string lastName,
        string passwordSalt,
        string passwordHash)
    {
        Validate(email, firstName, middleName, lastName);
        Email = email;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Roles = new HashSet<Role>();
    }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; private set; }
    public virtual HashSet<Role> Roles { get; set; }

    public User AddSupplier(Role role)
    {
        Roles.Add(role);
        return this;
    }

    public User RemoveSupplier(Role role)
    {
        Roles.Remove(role);
        return this;
    }

    private void Validate(
        string email,
        string firstName,
        string middleName,
        string lastName)
    {
        ValidateEmail(email);
        ValidateName(firstName, "First Name");
        ValidateName(middleName, "Middle Name");
        ValidateName(lastName, "Last Name");
    }
    private void ValidateEmail(string email)
        => Guard.ForStringLength(email, UserModelConstants.User.MinEmailLength, UserModelConstants.User.MaxEmailLength, nameof(Email));
    private void ValidateName(string name, string propertyName)
        => Guard.ForStringLength(name, UserModelConstants.User.MinNameLength, UserModelConstants.User.MaxNameLength, propertyName);
}