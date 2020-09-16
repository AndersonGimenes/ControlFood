using AutoMapper;
using ControlFood.UseCase.Interface.UseCase.Base;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace ControlFood.UI.Helpers.Implementation
{
    public abstract class BaseHelper<T> where T : class
    {
        private readonly IMemoryCache _cache;
        private readonly IGenericCadastroUseCase<T> _genericCadastroUseCase;

        public BaseHelper(IMemoryCache cache, IGenericCadastroUseCase<T> genericCadastroUseCase)
        {
            _cache = cache;
            _genericCadastroUseCase = genericCadastroUseCase;
        }

        protected List<T> CacheCategorias(string cacheName)
        {
            List<T> listaRetorno;

            if (!_cache.TryGetValue(cacheName, out listaRetorno))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3600));

                listaRetorno = _genericCadastroUseCase.BuscarTodos();

                _cache.Set(cacheName, listaRetorno, cacheEntryOptions);

                return listaRetorno;
            }

            return _cache.Get(cacheName) as List<T>;
        }
    }
}
