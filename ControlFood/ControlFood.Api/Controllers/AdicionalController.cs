using AutoMapper;
using ControlFood.Api.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdicionalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICadastroAdicionalUseCase _cadastroAdicionalUseCase;

        public AdicionalController(ICadastroAdicionalUseCase cadastroAdicionalUseCase, IMapper mapper)
        {
            _mapper = mapper;
            _cadastroAdicionalUseCase = cadastroAdicionalUseCase;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            try
            {
                var adicionais = _mapper.Map<List<Adicional>>(_cadastroAdicionalUseCase.BuscarTodos());
                return Ok(adicionais);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(Adicional adiconal)
        {
            try
            {
                var adicionalDominio = _mapper.Map<Dominio.Adicional>(adiconal);
                _cadastroAdicionalUseCase.Inserir(adicionalDominio);

                var adicionais = _mapper.Map<List<Adicional>>(_cadastroAdicionalUseCase.BuscarTodos());

                return Ok(adicionais);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Deletar(Adicional adiconal)
        {
            try
            {
                var adicionalDominio = _mapper.Map<Dominio.Adicional>(adiconal);

                // cadastrar adicional

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
