using AutoMapper;
using Application.Common.Interfaces;

namespace Application.Common.Mappings
{
    public interface IMapFrom2<T>
    {
        void Mapping(Profile profile, IPathProvider pathProvider) => profile.CreateMap(typeof(T), GetType());
    }
}
