using AutoMapper;
using ControlFood.Api.Helpers.Interface;
using ControlFood.Api.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
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
        public IActionResult ObterTodos()
        {
            try
            {
                var clientes = _clienteHelper.CacheClientes();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(Cliente cliente)
        {
            try
            {
                var clienteDominio = _mapper.Map<Dominio.Cliente>(cliente);
                _cadastroClienteUseCase.Inserir(clienteDominio);

                return Ok(_clienteHelper.CacheClientes(renovaCache: true));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Atualizar(Cliente cliente)
        {
            try
            {
                var clienteDominio = _mapper.Map<Dominio.Cliente>(cliente);

                _cadastroClienteUseCase.AtualizarCliente(clienteDominio);
                _clienteHelper.CacheClientes(renovaCache: true);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
