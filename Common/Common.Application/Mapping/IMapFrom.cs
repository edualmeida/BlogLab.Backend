using AutoMapper;

namespace Common.Application.Mapping;
public interface IMapFrom<T>
{
    void Mapping(Profile mapper) => mapper.CreateMap(typeof(T), GetType());
}