﻿using System.Reflection;
using AutoMapper;
using Common.Application.Mapping;

namespace Common.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile(Assembly assembly)
        => ApplyMappingsFromAssembly(assembly);

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly
            .GetExportedTypes()
            .Where(t => t
                .GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            const string mappingMethodName = "Mapping";

            var methodInfo = type.GetMethod(mappingMethodName)
                             ?? type.GetInterface("IMapFrom`1")?.GetMethod(mappingMethodName);

            methodInfo?.Invoke(instance, [this]);
        }
    }
}