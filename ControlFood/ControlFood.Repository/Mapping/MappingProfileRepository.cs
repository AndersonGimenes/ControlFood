using AutoMapper;
using ControlFood.Repository.Entidades;
using System.Collections.Generic;
using System.Linq;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Repository.Mapping
{
    public class MappingProfileRepository : Profile
    {
        public MappingProfileRepository()
        {
            #region[ Mapper dominio para repository ]
            CreateMap<Dominio.Categoria, Entidades.Categoria>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico));

            CreateMap<Dominio.Produto, Entidades.Produto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico))
                .ForMember(dest => dest.CategoriaId, opts => opts.MapFrom(x => x.Categoria.IdentificadorUnico))
                .ForMember(dest => dest.Categoria, opts => opts.MapFrom(x => SetarNulo()))
                .ForMember(dest => dest.Adicionais, opts => opts.MapFrom(x => SetarNulo()));

            CreateMap<Dominio.Adicional, Entidades.Adicional>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico));
               
            CreateMap<Dominio.Cliente, Entidades.Cliente>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.IdentificadorUnico));

            CreateMap<Dominio.Endereco, Entidades.Endereco>();
            #endregion

            #region[ Mapper repository para dominio ]
            CreateMap<Entidades.Categoria, Dominio.Categoria>()
               .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.Produto, Dominio.Produto>()
                .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id))
                .ForMember(dest => dest.Adicionais, opts => opts.MapFrom(x => x.Adicionais.Select(x => x.Adicional).ToList()));

            CreateMap<Entidades.Adicional, Dominio.Adicional>()
                .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.Cliente, Dominio.Cliente>()
              .ForMember(dest => dest.IdentificadorUnico, opts => opts.MapFrom(x => x.Id));

            CreateMap<Entidades.Endereco, Dominio.Endereco>();
            #endregion
        }

        private object SetarNulo() => null;
    }
}
