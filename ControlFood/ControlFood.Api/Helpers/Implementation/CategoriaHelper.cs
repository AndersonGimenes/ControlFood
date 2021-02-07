using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using ControlFood.Api.Helpers.Implementation.Base;
using ControlFood.Api.Helpers.Interface;
using ControlFood.Api.Models;

namespace ControlFood.Api.Helpers.Implementation
{
    public class CategoriaHelper : CacheBaseHelper<Dominio.Categoria>, ICategoriaHelper
    {
        private const string CACHE_NAME = "ListaCategoriasCache";
        private readonly IMapper _mapper;

        public CategoriaHelper(IMemoryCache cache, IMapper mapper, ICadastroCategoriaUseCase cadastroCategoriaUseCase)
            : base(cache, cadastroCategoriaUseCase)
        {
            _mapper = mapper;
        }

        public List<Categoria> CacheCategorias(bool renovaCache = false) => _mapper.Map<List<Categoria>>(base.ListarCache(CACHE_NAME, renovaCache));
        
    }
}
