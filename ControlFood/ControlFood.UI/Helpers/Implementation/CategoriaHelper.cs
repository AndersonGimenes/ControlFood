using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;
using ControlFood.UI.Helpers.Implementation.Base;
using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace ControlFood.UI.Helpers.Implementation

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

        public List<Categoria> CacheCategorias() => _mapper.Map<List<Categoria>>(base.ListaGenericaCache(CACHE_NAME));
        
    }
}
