using AutoMapper;

public class UserResponse : IMapFrom<User>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<User, UserResponse>();
            //.IncludeBase<User, UserModel>();
}
