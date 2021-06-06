using AutoMapper;
using ControlFood.Api.Models.Categoria;
using ControlFood.Domain.Entidades;

namespace ControlFood.Api.Mapping
{
    public class MappingProfileApi : Profile
    {
        public MappingProfileApi()
        {
            #region[ Mapper modelo para dominio ]
            CreateMap<CategoriaRequest, Categoria>();
            CreateMap<Models.Produto, Produto>();
            CreateMap<Models.Adicional, Adicional>();
            CreateMap<Models.Cliente, Cliente>();
            CreateMap<Models.Endereco, Endereco>();
            #endregion

            #region[ Mapper dominio para modelo ]
            CreateMap<Categoria, CategoriaResponse>();
            CreateMap<Produto, Models.Produto>();
            CreateMap<Adicional, Models.Adicional>();
            CreateMap<Cliente, Models.Cliente>();
            CreateMap<Endereco, Models.Endereco>();
            #endregion
        }
    }
}
