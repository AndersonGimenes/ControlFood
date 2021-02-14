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

            CreateMap<Dominio.Produto, Entidades.Produto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico))
                .ForMember(dest => dest.CategoriaId, opts => opts.MapFrom(x => x.Categoria.IdentificadorUnico))
                .ForMember(dest => dest.Categoria, opts => opts.MapFrom(x => SetarNulo()));

            CreateMap<Dominio.Cliente, Entidades.Cliente>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico));

            CreateMap<Dominio.Endereco, Entidades.Endereco>();

            CreateMap<Entidades.Categoria, Dominio.Categoria>()
               .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.Produto, Dominio.Produto>()
                .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.Cliente, Dominio.Cliente>()
              .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.Endereco, Dominio.Endereco>();
        }

        private object SetarNulo() => null;
    }
}
