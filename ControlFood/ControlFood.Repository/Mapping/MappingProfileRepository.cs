using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Repository.Mapping
{
    public class MappingProfileRepository : Profile
    {
        public MappingProfileRepository()
        {
            CreateMap<Dominio.Categoria, Entidades.Categoria>();
            CreateMap<Entidades.Categoria, Dominio.Categoria>();
        }
    }
}
