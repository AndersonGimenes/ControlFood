using AutoMapper;
using ControlFood.UI.Models;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICadastroCategoriaUseCase _cadastroCategoriaUseCase;
        private readonly IMemoryCache _cache;

        public CategoriaController(ICadastroCategoriaUseCase cadastroCategoriaUseCase, IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cadastroCategoriaUseCase = cadastroCategoriaUseCase;
            _cache = cache;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            ViewBag.Categorias = CacheCategorias();

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

                    _cadastroCategoriaUseCase.VerificarDuplicidade(categoriaDominio, _cadastroCategoriaUseCase.BuscarTodos());

                    _cadastroCategoriaUseCase.Inserir(categoriaDominio);
                    VerificarNovaCategoria(categoria);

                    ViewBag.Categorias = CacheCategorias();
                    return View();
                }
                else
                {
                    ViewBag.Categorias = CacheCategorias();
                    return View();
                }
            }
            catch (CategoriaIncorretaUseCaseException ex)
            {
                ViewData["mensagemErro"] = ex.Message;
                ViewBag.Categorias = CacheCategorias();
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
            var categoriaDominio = _mapper.Map<Dominio.Categoria>(categoria);

            _cadastroCategoriaUseCase.Deletar(categoriaDominio);
            VerificarNovaCategoria(categoria);

            return RedirectToAction("Cadastrar");
        }

        private List<Models.Categoria> CacheCategorias()
        {
            List<Models.Categoria> categorias;

            if (!_cache.TryGetValue("ListaCategoriasCache", out categorias))
            {
                categorias = _mapper.Map<List<Models.Categoria>>(_cadastroCategoriaUseCase.BuscarTodos());

                _cache.Set("ListaCategoriasCache", categorias);

                return categorias;
            }

            return _cache.Get("ListaCategoriasCache") as List<Models.Categoria>;
        }

        private void VerificarNovaCategoria(Categoria categoria)
        {
            if (categoria != null)
                _cache.Remove("ListaCategoriasCache");
        }
    }
}
