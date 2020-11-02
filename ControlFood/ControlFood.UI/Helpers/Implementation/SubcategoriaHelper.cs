using AutoMapper;
using ControlFood.UI.Helpers.Implementation.Base;
using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Helpers.Implementation
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
