using ArticleCatalog.Application.Categories.Common;
using ArticleCatalog.Domain.Models.Categories;
using AutoMapper;

namespace ArticleCatalog.Application.Categories.Queries.Common;
public class CategoryResponse : CategoryModel
{
    public Guid Id { get; set; }

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Category, CategoryResponse>()
            .IncludeBase<Category, CategoryModel>();
}