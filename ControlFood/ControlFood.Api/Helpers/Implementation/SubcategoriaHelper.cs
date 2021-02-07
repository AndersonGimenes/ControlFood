using AutoMapper;
using ControlFood.Api.Helpers.Implementation.Base;
using ControlFood.Api.Helpers.Interface;
using ControlFood.Api.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Api.Helpers.Implementation
{
    public class SubcategoriaHelper : CacheBaseHelper<Dominio.SubCategoria>, ISubCategoriaHelper
    {
        private readonly IMapper _mapper;
        private const string CACHE_NAME = "ListaSubCategoriasCache";

        public SubcategoriaHelper(IMapper mapper, IMemoryCache cache, ICadastroSubCategoriaUseCase cadastroSubCategoriaUseCase)
            : base(cache, cadastroSubCategoriaUseCase)
        {
            _mapper = mapper;
        }
        public List<SubCategoria> CacheSubCategorias(bool renovaCache = false) => _mapper.Map<List<SubCategoria>>(base.ListarCache(CACHE_NAME, renovaCache));

    }
}
