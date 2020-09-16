using AutoMapper;
using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace ControlFood.UI.Helpers.Implementation

{
    public class CategoriaHelper : BaseHelper<Domain.Entidades.Categoria>, ICategoriaHelper
    {
        private const string CACHE_NAME = "ListaCategoriasCache";
        private readonly IMapper _mapper;

        public CategoriaHelper(IMemoryCache cache, IMapper mapper, ICadastroCategoriaUseCase cadastroCategoriaUseCase)
            : base(cache, cadastroCategoriaUseCase)
        {
            _mapper = mapper;
        }

        public List<Categoria> CacheCategorias() => _mapper.Map<List<Categoria>>(base.CacheCategorias(CACHE_NAME));
        
    }
}
