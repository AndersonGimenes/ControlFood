using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ControlFood.UI.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICadastroClienteUseCase _cadastroClienteUseCase;

        public ClienteController(IMapper mapper, ICadastroClienteUseCase cadastroClienteUseCase)
        {
            _mapper = mapper;
            _cadastroClienteUseCase = cadastroClienteUseCase;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var clientes = new List<Cliente>();
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

                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
