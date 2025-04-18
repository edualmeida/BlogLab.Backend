using Common.Domain;
using Identity.Domain.Models.Users;

namespace Identity.Domain.Factories;
public interface IUserBuilder : IBuilder<User>
{
    IUserBuilder WithEmail(string email);
    IUserBuilder WithFirstName(string firstName);
    IUserBuilder WithMiddleName(string middleName);
    IUserBuilder WithSurname(string surname);
}