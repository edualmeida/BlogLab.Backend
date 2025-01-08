public interface IUserBuilder : IBuilder<User>
{
    IUserBuilder WithEmail(string email);
    IUserBuilder WithFirstName(string name);
    IUserBuilder WithMiddleName(string middleName);
    IUserBuilder WithSurname(string surname);
}