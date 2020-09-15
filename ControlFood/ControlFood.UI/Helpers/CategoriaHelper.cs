using AutoMapper;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace ControlFood.UI.Helpers
{
    public class CategoriaHelper : ICategoriaHelper
    {
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly ICadastroCategoriaUseCase _cadastroCategoriaUseCase;

        public CategoriaHelper(IMemoryCache cache, IMapper mapper, ICadastroCategoriaUseCase cadastroCategoriaUseCase)
        {
            _cache = cache;
            _mapper = mapper;
            _cadastroCategoriaUseCase = cadastroCategoriaUseCase;
        }
        public List<Models.Categoria> CacheCategorias()
        {
            List<Models.Categoria> categorias;

            if (!_cache.TryGetValue("ListaCategoriasCache", out categorias))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3600));

                categorias = _mapper.Map<List<Models.Categoria>>(_cadastroCategoriaUseCase.BuscarTodos());

                _cache.Set("ListaCategoriasCache", categorias, cacheEntryOptions);

                return categorias;
            }

            return _cache.Get("ListaCategoriasCache") as List<Models.Categoria>;
        }
    }
}
