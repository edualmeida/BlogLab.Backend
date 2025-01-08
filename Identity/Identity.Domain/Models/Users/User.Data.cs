public class UserData
{
    public Type EntityType => typeof(User);

    public User GetData()
    {
        var adminUser = new UserBuilder()
                    .WithFirstName("Admin")
                    .WithMiddleName("admin")
                    .WithSurname("admin")
                    .WithEmail("admin@eduardolab.com")
                    .Build();

        return adminUser;
    }
}
