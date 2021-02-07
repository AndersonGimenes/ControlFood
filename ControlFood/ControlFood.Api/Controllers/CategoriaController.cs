using AutoMapper;
using ControlFood.Api.Helpers.Interface;
using ControlFood.Api.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Api.Controllers
{
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
        public IActionResult Cadastrar()
        {
            var categorias = _categoriaHelper.CacheCategorias();
            return Ok(categorias);
        }

        [HttpPost]
        public IActionResult Cadastrar(Categoria categoria)
        {
            try
            {
                var categoriaDominio = _mapper.Map<Dominio.Categoria>(categoria);

                _cadastroCategoriaUseCase.Inserir(categoriaDominio);

                var categorias = _categoriaHelper.CacheCategorias(renovaCache: true);

                return Ok(categorias);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete]
        public IActionResult Deletar(Categoria categoria)
        {
            try
            {
                var categoriaDominio = _mapper.Map<Dominio.Categoria>(categoria);

                _cadastroCategoriaUseCase.Deletar(categoriaDominio);
                _categoriaHelper.CacheCategorias(renovaCache: true);

                categoria.Mensagem = Constantes.Mensagem.Comum.ItemDeletado;

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
