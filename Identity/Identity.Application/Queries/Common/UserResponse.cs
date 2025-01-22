using AutoMapper;

public class UserResponse : IMapFrom<User>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<User, UserResponse>()
            .ForMember(p => p.Id, opt => opt.MapFrom(src => new Guid(src.Id)));
            //.IncludeBase<User, UserModel>();
}
