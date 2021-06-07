using AutoMapper;
using ControlFood.Api.Helpers.Implementation.Base;
using ControlFood.Api.Helpers.Interface;
using ControlFood.Api.Models.Produto;
using ControlFood.Domain.Entidades.Produto;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace ControlFood.Api.Helpers.Implementation
{
    public class ProdutoHelper : CacheBaseHelper<ProdutoVenda>, IProdutoHelper
    {
        private const string CACHE_NAME = "ListaProdutosCache";
        private readonly IMapper _mapper;

        public ProdutoHelper(IMemoryCache cache, IMapper mapper, ICadastroProdutoUseCase cadastroProdutoUseCase)
            : base(cache, cadastroProdutoUseCase)
        {
            _mapper = mapper;
        }

        public IEnumerable<ProdutoResponse> CacheProdutos(bool renovaCache) => 
            _mapper.Map <IEnumerable<ProdutoResponse>>(base.ListarCache(CACHE_NAME, renovaCache));
    }
}
