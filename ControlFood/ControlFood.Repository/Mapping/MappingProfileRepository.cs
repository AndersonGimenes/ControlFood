using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Repository.Mapping
{
    public class MappingProfileRepository : Profile
    {
        public MappingProfileRepository()
        {
            #region[ DOMINIO PARA REPOSITORY ]
            CreateMap<Dominio.Categoria, Entidades.Categoria>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico));

            CreateMap<Dominio.SubCategoria, Entidades.SubCategoria>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico))
                .ForMember(dest => dest.CategoriaId, opts => opts.MapFrom(x => x.Categoria.IdentificadorUnico))
                .ForMember(dest => dest.Categoria, opts => opts.MapFrom(x => SetarNulo()));
            #endregion

            #region [ REPOSITORY PARA DOMINIO ]
            CreateMap<Entidades.Categoria, Dominio.Categoria>()
               .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.SubCategoria, Dominio.SubCategoria>()
               .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.Produto, Dominio.Produto>()
                .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            #endregion
        }

        // Seta nulo para não gravar Categoria por triger quando inserir uma subcategoria
        private Entidades.Categoria SetarNulo() => null;
    }
}
