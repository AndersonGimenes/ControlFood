using AutoMapper;
using ControlFood.UI.Helpers.Implementation.Base;
using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Helpers.Implementation
{
    public class ClienteHelper : CacheBaseHelper<Dominio.Cliente>, IClienteHelper
    {
        private const string CACHE_NAME = "ListaClientesCache";
        private readonly IMapper _mapper;
        public ClienteHelper(IMemoryCache cache, IMapper mapper, ICadastroClienteUseCase cadastroClienteUseCase)
            : base(cache, cadastroClienteUseCase)
        {
            _mapper = mapper;
        }
        public List<Cliente> CacheClientes(bool renovaCache = false) => _mapper.Map<List<Cliente>>(base.ListarCache(CACHE_NAME, renovaCache));
    }
}
