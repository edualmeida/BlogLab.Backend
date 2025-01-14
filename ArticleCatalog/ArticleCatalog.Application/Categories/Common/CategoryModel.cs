using AutoMapper;

public class CategoryModel : IMapFrom<Category>
{
    public string Name { get; set; }

    public virtual void Mapping(Profile mapper)
    {
        mapper.CreateMap<Category, CategoryModel>();
    }
}