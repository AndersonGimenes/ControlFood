using AutoMapper;
using System;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Mapping
{
    public class MappingProfileUI : Profile
    {
        public MappingProfileUI()
        {
            // mapper modelo para dominio
            CreateMap<Models.Categoria, Dominio.Categoria>();

            CreateMap<Models.SubCategoria, Dominio.SubCategoria>()
                .ForMember(dest => dest.IndicadorItemCozinha, opts => opts.MapFrom(x => x.Indicador == 0))
                .ForMember(dest => dest.IndicadorItemBar, opts => opts.MapFrom(x => x.Indicador == 1));

            CreateMap<Models.Produto, Dominio.Produto>();

            CreateMap<Models.Estoque, Dominio.Estoque>();

            // mapper dominio para modelo
            CreateMap<Dominio.Categoria, Models.Categoria>();            

            CreateMap<Dominio.SubCategoria, Models.SubCategoria>()
                .ForMember(dest => dest.Indicador, opts => opts.MapFrom(x => x.IndicadorItemCozinha ? 0 : 1));

            CreateMap<Dominio.Estoque, Models.Estoque>()
                .ForMember(dest => dest.DataValidade, opts => opts.MapFrom(x => x.DataValidade.ToString("dd/MM/yyyy")));

            CreateMap<Dominio.Produto, Models.Produto>();
        }
    }
}
