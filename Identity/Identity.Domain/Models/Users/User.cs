using Microsoft.AspNetCore.Identity;

public class User : IdentityUser, IEntity, IAggregateRoot
{
    internal User(
        string email,
        string firstName,
        string middleName,
        string lastName)
    {
        Validate(email, firstName, middleName, lastName);
        Email = email;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        CreatedOnUTC = DateTime.UtcNow;
        Enabled = true;
    }

    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string LastName { get; private set; }

    public DateTime CreatedOnUTC { get; set; }
    public bool Enabled { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is User user)
        {
            return Id == user.Id;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
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
        => Guard.ForStringLength(email, UserModelConstants.Identity.MinEmailLength, UserModelConstants.Identity.MaxEmailLength, nameof(Email));
    private void ValidateName(string name, string propertyName)
        => Guard.ForStringLength(name, UserModelConstants.Identity.MinNameLength, UserModelConstants.Identity.MaxNameLength, propertyName);
}
