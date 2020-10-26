using AutoMapper;
using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Controllers
{
    public class CategoriaController : Controller
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
            return View(categorias);
        }

        [HttpPost]
        public IActionResult Cadastrar(Categoria categoria)
        {
            try
            {
                categoria.IsValid();

                var categoriaDominio = _mapper.Map<Dominio.Categoria>(categoria);

                _cadastroCategoriaUseCase.Inserir(categoriaDominio);

                var categorias = _categoriaHelper.CacheCategorias(renovaCache: true);

                return View(categorias);

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

                return Json(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
