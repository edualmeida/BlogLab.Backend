internal class UserBuilder : IUserBuilder
{
    private string email = default!;
    private string firstName = default!;
    private string middleName = default!;
    private string surname = default!;
    private string password = default!;

    private bool isEmailSet = false;
    private bool isFirstNameSet = false;
    private bool isMiddleNameSet = false;
    private bool isSurnameSet = false;
    private bool isPasswordSet = false;

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

    public IUserBuilder WithPassword(string password)
    {
        this.password = password;
        isPasswordSet = true;

        return this;
    }

    public User Build()
    {
        if (!isEmailSet || !isFirstNameSet || !isMiddleNameSet || !isSurnameSet || !isPasswordSet)
            throw new InvalidOperationException("email, firstName, surName, lastName, password must have a value.");

        return new User(
            email,
            firstName,
            middleName,
            surname,
            password,
            password);
    }
}
