using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ControlFood.UI.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ISubCategoriaHelper _subcategoriaHelper;
        private readonly IProdutoHelper _produtoHelper;

        public ProdutoController(ISubCategoriaHelper subcategoriaHelper, IProdutoHelper produtoHelper)
        {
            _subcategoriaHelper = subcategoriaHelper;
            _produtoHelper = produtoHelper;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.SubCategorias = _subcategoriaHelper.CacheSubCategorias();
            var produtos = _produtoHelper.CacheProdutos();

            return View(produtos);
        }

        [HttpPost]
        public IActionResult Cadastrar(Produto produto)
        {
            try
            {
                // validar produto is valid

                // criar mapper para dominio 

                // chamar metodo para inserir

                // mapear retorno 

                // retornar lista de produtos
                return View();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }

}

