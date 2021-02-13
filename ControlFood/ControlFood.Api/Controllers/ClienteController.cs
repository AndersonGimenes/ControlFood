﻿using AutoMapper;
using ControlFood.Api.Helpers.Interface;
using ControlFood.Api.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Api.Controllers
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
        public IActionResult BuscarEndereco(Cliente cliente)
        {
            var clienteDominio = _mapper.Map<Dominio.Cliente>(cliente);

            var clienteResponse = _cadastroClienteUseCase.BuscarPorIdentificacao(clienteDominio);

            return Json(_mapper.Map<Cliente>(clienteResponse));
        }
    }
}