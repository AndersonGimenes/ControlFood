using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Repository.Mapping
{
    public class MappingProfileRepository : Profile
    {
        public MappingProfileRepository()
        {
            // mapear dominio para repository
            CreateMap<Dominio.Categoria, Entidades.Categoria>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico));

            CreateMap<Dominio.SubCategoria, Entidades.SubCategoria>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico))
                .ForMember(dest => dest.CategoriaId, opts => opts.MapFrom(x => x.Categoria.IdentificadorUnico))
                .ForMember(dest => dest.Categoria, opts => opts.MapFrom(x => SetarNulo()));

            CreateMap<Dominio.Estoque, Entidades.Estoque>()
                .ForMember(dest => dest.IdProduto, opts => opts.MapFrom(x => x.IdentificadorUnicoProduto));

            CreateMap<Dominio.Produto, Entidades.Produto>()
                .ForMember(dest => dest.SubCategoriaId, opts => opts.MapFrom(x => x.SubCategoria.IdentificadorUnico))
                .ForMember(dest => dest.SubCategoria, opts => opts.MapFrom(x => SetarNulo()))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico));

            // mapear repository para dominio
            CreateMap<Entidades.Categoria, Dominio.Categoria>()
               .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.SubCategoria, Dominio.SubCategoria>()
               .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.Estoque, Dominio.Estoque>()
                .ForMember(dest => dest.IdentificadorUnicoProduto, opts => opts.MapFrom(x => x.IdProduto));

            CreateMap<Entidades.Produto, Dominio.Produto>()
                .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

        }

        // Seta nulo para não gravar Categoria por triger quando inserir uma subcategoria
        private object SetarNulo() => null;
    }
}
