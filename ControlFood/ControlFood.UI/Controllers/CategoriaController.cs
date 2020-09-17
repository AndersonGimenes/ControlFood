using AutoMapper;
using ControlFood.UI.Helpers;
using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Models;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICadastroCategoriaUseCase _cadastroCategoriaUseCase;
        private readonly IMemoryCache _cache;
        private readonly ICategoriaHelper _categoriaHelper;

        public CategoriaController(ICadastroCategoriaUseCase cadastroCategoriaUseCase, IMapper mapper, IMemoryCache cache, ICategoriaHelper categoriaHelper)
        {
            _mapper = mapper;
            _cadastroCategoriaUseCase = cadastroCategoriaUseCase;
            _cache = cache;
            _categoriaHelper = categoriaHelper;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            ViewBag.Categorias = _categoriaHelper.CacheCategorias();

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var categoriaDominio = _mapper.Map<Dominio.Categoria>(categoria);

                    _cadastroCategoriaUseCase.Inserir(categoriaDominio);
                    
                    ViewBag.Categorias = _categoriaHelper.CacheCategorias(renovaCache: true);
                    return View();
                }
                else
                {
                    ViewBag.Categorias = _categoriaHelper.CacheCategorias();
                    return View();
                }
            }
            catch (CategoriaIncorretaUseCaseException ex)
            {
                ViewData["mensagemErro"] = ex.Message;
                ViewBag.Categorias = _categoriaHelper.CacheCategorias();
                return View();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Deletar(Categoria categoria)
        {
            try
            {
                var categoriaDominio = _mapper.Map<Dominio.Categoria>(categoria);

                _cadastroCategoriaUseCase.Deletar(categoriaDominio);
                _categoriaHelper.CacheCategorias(renovaCache: true);
                
                return RedirectToAction("Cadastrar");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
