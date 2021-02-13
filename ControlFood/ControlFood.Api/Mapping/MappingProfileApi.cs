using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Api.Mapping
{
    public class MappingProfileApi : Profile
    {
        public MappingProfileApi()
        {
            // mapper modelo para dominio
            CreateMap<Models.Categoria, Dominio.Categoria>();

            CreateMap<Models.Produto, Dominio.Produto>();

            CreateMap<Models.Estoque, Dominio.Estoque>();

            CreateMap<Models.Cliente, Dominio.Cliente>();
            CreateMap<Models.Endereco, Dominio.Endereco>();

            // mapper dominio para modelo
            CreateMap<Dominio.Categoria, Models.Categoria>();            

            CreateMap<Dominio.Estoque, Models.Estoque>();

            CreateMap<Dominio.Produto, Models.Produto>();

            CreateMap<Dominio.Cliente, Models.Cliente>();
            CreateMap<Dominio.Endereco, Models.Endereco>();
        }
    }
}
