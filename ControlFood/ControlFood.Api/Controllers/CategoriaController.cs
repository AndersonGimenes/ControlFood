using AutoMapper;
using ControlFood.Api.Helpers.Interface;
using ControlFood.Api.Models.Categoria;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using ControlFood.Domain.Entidades;

namespace ControlFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICadastroCategoriaUseCase _cadastroCategoriaUseCase;
        private readonly ICategoriaHelper _categoriaHelper;

        public CategoriaController(ICadastroCategoriaUseCase cadastroCategoriaUseCase, IMapper mapper, ICategoriaHelper categoriaHelper)
        {
            _mapper = mapper;
            _cadastroCategoriaUseCase = cadastroCategoriaUseCase;
            _categoriaHelper = categoriaHelper;
        }
        
        [HttpGet]
        public IActionResult ObterTodos()
        {
            try
            {
                var categorias = _categoriaHelper.CacheCategorias(renovaCache: false);
                return Ok(categorias);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost]
        public IActionResult Cadastrar(CategoriaRequest categoria)
        {
            try
            {
                var categoriaDominio = _mapper.Map<Categoria>(categoria);

                _cadastroCategoriaUseCase.Inserir(categoriaDominio);
                var categorias = _categoriaHelper.CacheCategorias(renovaCache: true);

                return Ok(categorias);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _cadastroCategoriaUseCase.DeletarCategoria(id);
                _categoriaHelper.CacheCategorias(renovaCache: true);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
