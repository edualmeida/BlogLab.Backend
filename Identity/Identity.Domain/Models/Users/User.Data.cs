using Identity.Domain.Factories;

namespace Identity.Domain.Models.Users;
public class UserData
{
    public Type EntityType => typeof(User);

    public User GetData()
    {
        var adminUser = new UserBuilder()
                    .WithFirstName("Root")
                    .WithMiddleName("root")
                    .WithSurname("root")
                    .WithEmail("root@eduardolab.com")
                    .Build();

        return adminUser;
    }
}
