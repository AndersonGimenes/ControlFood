using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Api.Mapping
{
    public class MappingProfileApi : Profile
    {
        public MappingProfileApi()
        {
            #region[ Mapper modelo para dominio ]
            CreateMap<Models.Categoria, Dominio.Categoria>();

            CreateMap<Models.Produto, Dominio.Produto>();

            CreateMap<Models.Adicional, Dominio.Adicional>();

            CreateMap<Models.Cliente, Dominio.Cliente>();

            CreateMap<Models.Endereco, Dominio.Endereco>();
            #endregion

            #region[ Mapper dominio para modelo ]
            CreateMap<Dominio.Categoria, Models.Categoria>();

            CreateMap<Dominio.Produto, Models.Produto>();

            CreateMap<Dominio.Adicional, Models.Adicional>();

            CreateMap<Dominio.Cliente, Models.Cliente>();

            CreateMap<Dominio.Endereco, Models.Endereco>();
            #endregion
        }
    }
}
