using AutoMapper;
using ControlFood.Api.Models.Categoria;
using ControlFood.Api.Models.Produto;
using ControlFood.Domain.Entidades;
using ControlFood.Domain.Entidades.Produto;

namespace ControlFood.Api.Mapping
{
    public class MappingProfileApi : Profile
    {
        public MappingProfileApi()
        {
            #region[ Mapper modelo para dominio ]
            CreateMap<CategoriaRequest, Categoria>();
            CreateMap<ProdutoRequest, ProdutoVenda>();
            CreateMap<Models.Adicional, Adicional>();
            CreateMap<Models.Cliente, Cliente>();
            CreateMap<Models.Endereco, Endereco>();
            #endregion

            #region[ Mapper dominio para modelo ]
            CreateMap<Categoria, CategoriaResponse>();
            CreateMap<ProdutoVenda, ProdutoResponse>();
            CreateMap<Adicional, Models.Adicional>();
            CreateMap<Cliente, Models.Cliente>();
            CreateMap<Endereco, Models.Endereco>();
            #endregion
        }
    }
}
