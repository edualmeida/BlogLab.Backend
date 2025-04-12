using System.Reflection;
using AutoMapper;

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
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            const string mappingMethodName = "CreateMapping";

            var methodInfo = type.GetMethod(mappingMethodName)
                             ?? type.GetInterface("IMapTo`1")?.GetMethod(mappingMethodName);

            methodInfo?.Invoke(instance, [this]);
        }
    }
}