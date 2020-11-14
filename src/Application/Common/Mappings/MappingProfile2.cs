using System;
using AutoMapper;
using System.Linq;
using System.Reflection;
using Application.Common.Interfaces;

namespace Application.Common.Mappings
{
    public class MappingProfile2 : Profile
    {
        public IPathProvider PathProvider { get; set; }

        public void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom2<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom2`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this, PathProvider });

            }
        }
    }
}
