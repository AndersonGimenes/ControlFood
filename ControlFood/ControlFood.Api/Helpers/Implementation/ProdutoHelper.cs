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
    public class ProdutoHelper : CacheBaseHelper<Dominio.Produto>, IProdutoHelper
    {
        private const string CACHE_NAME = "ListaProdutosCache";
        private readonly IMapper _mapper;

        public ProdutoHelper(IMemoryCache cache, IMapper mapper, ICadastroProdutoUseCase cadastroProdutoUseCase)
            : base(cache, cadastroProdutoUseCase)
        {
            _mapper = mapper;
        }

        public List<Produto> CacheProdutos(bool renovaCache = false) => _mapper.Map <List<Produto>>(base.ListarCache(CACHE_NAME, renovaCache));
    }
}
