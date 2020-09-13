using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Repository.Mapping
{
    public class MappingProfileRepository : Profile
    {
        public MappingProfileRepository()
        {
            CreateMap<Dominio.Categoria, Entidades.Categoria>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico));

            CreateMap<Entidades.Categoria, Dominio.Categoria>()
                .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));
        }
    }
}
