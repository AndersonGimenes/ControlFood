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
                .ForMember(dest => dest.IndicadorItemCozinha, opts => opts.MapFrom(x => IndicadorCozinha(x.Indicador)))
                .ForMember(dest => dest.IndicadorItemBar, opts => opts.MapFrom(x => IndicadorBar(x.Indicador)));

            CreateMap<Dominio.SubCategoria, Models.SubCategoria>();
        }

        private bool IndicadorBar(int indicador) => indicador == 1;
        private bool IndicadorCozinha(int indicador) => indicador == 0;
    }
}
