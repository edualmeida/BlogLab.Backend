using AutoMapper;

public class UserModel : IMapFrom<User>
{
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string Surname { get; private set; }
    public string Password { get; private set; }

    public virtual void Mapping(Profile mapper)
    {
        mapper.CreateMap<User, UserModel>();
    }
}