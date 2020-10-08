﻿using AutoMapper;
using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Controllers
{
    public class SubCategoriaController : Controller
    {
        private readonly ICadastroSubCategoriaUseCase _cadastroSubCategoriaUseCase;
        private readonly IMapper _mapper;
        private readonly ICategoriaHelper _categoriaHelper;
        private readonly ISubcategoriaHelper _subcategoriaHelper;

        public SubCategoriaController(ICadastroSubCategoriaUseCase cadastroSubCategoriaUseCase, IMapper mapper, ICategoriaHelper categoriaHelper, ISubcategoriaHelper subcategoriaHelper)
        {
            _cadastroSubCategoriaUseCase = cadastroSubCategoriaUseCase;
            _categoriaHelper = categoriaHelper;
            _subcategoriaHelper = subcategoriaHelper;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            ViewBag.Categorias = _categoriaHelper.CacheCategorias();
            var subCategorias = _subcategoriaHelper.CacheSubCategorias();

            return View(subCategorias);
        }

        [HttpPost]
        public IActionResult Cadastrar(SubCategoria subCategoria)
        {
            try
            {
                subCategoria.IsValid();

                var subCategoriaDominio = _mapper.Map<Dominio.SubCategoria>(subCategoria);

                _cadastroSubCategoriaUseCase.Inserir(subCategoriaDominio);

                ViewBag.Categorias = _categoriaHelper.CacheCategorias();
                var subCategorias = _subcategoriaHelper.CacheSubCategorias(renovaCache: true);

                return View(subCategorias);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Deletar(SubCategoria subCategoria)
        {
            try
            {
                var subCategoriaDominio = _mapper.Map<Dominio.SubCategoria>(subCategoria);

                _cadastroSubCategoriaUseCase.Deletar(subCategoriaDominio);
                _subcategoriaHelper.CacheSubCategorias(renovaCache: true);

                subCategoria.Mensagem = Constantes.Mensagem.Comum.ItemDeletado;
                return Json(subCategoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Atualizar(SubCategoria subCategoria)
        {
            try
            {
                var subCategoriaDominio = _mapper.Map<Dominio.SubCategoria>(subCategoria);

                _cadastroSubCategoriaUseCase.Atualizar(subCategoriaDominio);
                _subcategoriaHelper.CacheSubCategorias(renovaCache: true);

                subCategoria.Mensagem = Constantes.Mensagem.Comum.ItemAtualizado;
                return Json(subCategoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
