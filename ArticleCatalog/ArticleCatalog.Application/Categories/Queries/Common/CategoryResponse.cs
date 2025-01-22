using AutoMapper;

public class CategoryResponse : CategoryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Category, CategoryResponse>()
            .IncludeBase<Category, CategoryModel>();
}