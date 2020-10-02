using AutoMapper;
using System;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Mapping
{
    public class MappingProfileUI : Profile
    {
        public MappingProfileUI()
        {
            CreateMap<Models.Categoria, Dominio.Categoria>();
            CreateMap<Dominio.Categoria, Models.Categoria>();

            CreateMap<Models.SubCategoria, Dominio.SubCategoria>()
                .ForMember(dest => dest.IndicadorItemCozinha, opts => opts.MapFrom(x => x.Indicador == 0))
                .ForMember(dest => dest.IndicadorItemBar, opts => opts.MapFrom(x => x.Indicador == 1));

            CreateMap<Dominio.SubCategoria, Models.SubCategoria>()
                .ForMember(dest => dest.Indicador, opts => opts.MapFrom(x => x.IndicadorItemCozinha ? 0 : 1));
        }
    }
}
