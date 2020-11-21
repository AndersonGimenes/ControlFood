using AutoMapper;
using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICadastroClienteUseCase _cadastroClienteUseCase;
        private readonly IClienteHelper _clienteHelper;

        public ClienteController(IMapper mapper, ICadastroClienteUseCase cadastroClienteUseCase, IClienteHelper clienteHelper)
        {
            _mapper = mapper;
            _cadastroClienteUseCase = cadastroClienteUseCase;
            _clienteHelper = clienteHelper;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var clientes = _clienteHelper.CacheClientes();
            return View(clientes);
        }

        [HttpPost]
        public IActionResult Cadastrar(Cliente cliente)
        {
            try
            {
                cliente.IsValid();
                var clienteDominio = _mapper.Map<Dominio.Cliente>(cliente);

                _cadastroClienteUseCase.Inserir(clienteDominio);

                return View(_clienteHelper.CacheClientes(renovaCache: true));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public void ConsultarEndereco()
        {

        }
    }
}
