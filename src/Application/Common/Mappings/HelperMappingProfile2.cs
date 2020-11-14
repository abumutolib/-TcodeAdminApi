using System.Reflection;
using Application.Common.Interfaces;

namespace Application.Common.Mappings
{
    public class HelperMappingProfile2
    {
        public MappingProfile2 MappingProfile { get; set; }

        public HelperMappingProfile2(IPathProvider pathProvider)
        {
            MappingProfile = new MappingProfile2();
            MappingProfile.PathProvider = pathProvider;
            MappingProfile.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
