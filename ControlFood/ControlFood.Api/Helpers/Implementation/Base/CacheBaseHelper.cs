﻿using ControlFood.UseCase.Interface.UseCase.Base;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace ControlFood.Api.Helpers.Implementation.Base
{
    public abstract class CacheBaseHelper<T> where T : class
    {
        private readonly IMemoryCache _cache;
        private readonly IGenericCadastroUseCase<T> _genericCadastroUseCase;

        public CacheBaseHelper(IMemoryCache cache, IGenericCadastroUseCase<T> genericCadastroUseCase)
        {
            _cache = cache;
            _genericCadastroUseCase = genericCadastroUseCase;
        }

        protected IEnumerable<T> ListarCache(string cacheName, bool renovaCache)
        {
            if (renovaCache || !_cache.TryGetValue(cacheName, out _))
            {
                var listaRetorno = _genericCadastroUseCase.BuscarTodos();

                this.SetarListaCache(cacheName, listaRetorno);
            }

            return _cache.Get(cacheName) as IEnumerable<T>;
        }

        private void SetarListaCache(string cacheName, IEnumerable<T> listaGenerica)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3600));

            _cache.Set(cacheName, listaGenerica, cacheEntryOptions);
        }
    }
}
