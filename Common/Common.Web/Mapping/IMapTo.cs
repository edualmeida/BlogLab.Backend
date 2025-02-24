using AutoMapper;
using Microsoft.AspNetCore.Http;

public interface IMapTo<T>
{
    void CreateMapping(Profile mapper) => mapper.CreateMap(GetType(), typeof(T));
    T Map(IMapper mapper, HttpContext httpContext);
}