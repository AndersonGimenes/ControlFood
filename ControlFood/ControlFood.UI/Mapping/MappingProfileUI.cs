using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Mapping
{
    public class MappingProfileUI : Profile
    {
        public MappingProfileUI()
        {
            CreateMap<Models.Categoria, Dominio.Categoria>();
        }
    }
}
