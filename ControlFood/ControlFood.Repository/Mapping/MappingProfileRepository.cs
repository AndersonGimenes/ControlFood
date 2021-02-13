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

            CreateMap<Dominio.Estoque, Entidades.Estoque>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico))
                .ForMember(dest => dest.IdProduto, opts => opts.MapFrom(x => x.IdentificadorUnicoProduto));

            CreateMap<Dominio.Cliente, Entidades.Cliente>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico));

            CreateMap<Dominio.Endereco, Entidades.Endereco>();

            // mapear repository para dominio
            CreateMap<Entidades.Categoria, Dominio.Categoria>()
               .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.Estoque, Dominio.Estoque>()
                .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id))
                .ForMember(dest => dest.IdentificadorUnicoProduto, opts => opts.MapFrom(x => x.IdProduto));

            CreateMap<Entidades.Produto, Dominio.Produto>()
                .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.Cliente, Dominio.Cliente>()
              .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.Endereco, Dominio.Endereco>();
        }
    }
}
