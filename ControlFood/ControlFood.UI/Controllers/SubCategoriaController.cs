﻿using AutoMapper;
using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Controllers
{
    public class SubCategoriaController : Controller
    {
        private readonly ICadastroSubCategoriaUseCase _cadastroSubCategoriaUseCase;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ICategoriaHelper _categoriaHelper;
        private readonly ISubcategoriaHelper _subcategoriaHelper;
        private readonly ICadastroCategoriaUseCase _cadastroCategoriaUseCase;

        public SubCategoriaController(
            ICadastroSubCategoriaUseCase cadastroSubCategoriaUseCase, 
            ICadastroCategoriaUseCase cadastroCategoriaUseCase,
            IMapper mapper, 
            IMemoryCache cache, 
            ICategoriaHelper categoriaHelper,
            ISubcategoriaHelper subcategoriaHelper)
        {
            _cadastroSubCategoriaUseCase = cadastroSubCategoriaUseCase;
            _cadastroCategoriaUseCase = cadastroCategoriaUseCase;
            _categoriaHelper = categoriaHelper;
            _subcategoriaHelper = subcategoriaHelper;
            _mapper = mapper;
            _cache = cache;           
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            ViewBag.Categorias = _categoriaHelper.CacheCategorias();
            ViewBag.SubCategorias = _subcategoriaHelper.CacheSubCategorias();

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(SubCategoria subCategoria)
        {
            var subCategoriaDominio = _mapper.Map<Dominio.SubCategoria>(subCategoria);

            _cadastroSubCategoriaUseCase.Inserir(subCategoriaDominio);

            ViewBag.Categorias = _categoriaHelper.CacheCategorias();
            ViewBag.SubCategorias = _subcategoriaHelper.CacheSubCategorias(renovaCache: true);

            return View();
        }
             
    }
}
