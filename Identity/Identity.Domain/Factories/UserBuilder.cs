using Identity.Domain.Models.Users;

namespace Identity.Domain.Factories;
internal class UserBuilder : IUserBuilder
{
    private string email = default!;
    private string firstName = default!;
    private string middleName = default!;
    private string surname = default!;

    private bool isEmailSet = false;
    private bool isFirstNameSet = false;
    private bool isMiddleNameSet = false;
    private bool isSurnameSet = false;

    public IUserBuilder WithEmail(string email)
    {
        this.email = email;
        isEmailSet = true;

        return this;
    }

    public IUserBuilder WithFirstName(string firstName)
    {
        this.firstName = firstName;
        isFirstNameSet = true;

        return this;
    }

    public IUserBuilder WithMiddleName(string middleName)
    {
        this.middleName = middleName;
        isMiddleNameSet = true;

        return this;
    }

    public IUserBuilder WithSurname(string surname)
    {
        this.surname = surname;
        isSurnameSet = true;

        return this;
    }

    public User Build()
    {
        if (!isEmailSet || !isFirstNameSet || !isMiddleNameSet || !isSurnameSet)
            throw new InvalidOperationException("email, firstName, surName, lastName must have a value.");

        return new User(
            email,
            firstName,
            middleName,
            surname);
    }
}