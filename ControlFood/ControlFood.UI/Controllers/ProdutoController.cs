using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;
using ControlFood.UI.Helpers.Interface;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControlFood.UI.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ISubCategoriaHelper _subcategoriaHelper;
        private readonly IProdutoHelper _produtoHelper;
        private readonly IMapper _mapper;
        private readonly ICadastroProdutoUseCase _cadastroProdutoUseCase;

        public ProdutoController(ISubCategoriaHelper subcategoriaHelper, IProdutoHelper produtoHelper, IMapper mapper, ICadastroProdutoUseCase cadastroProdutoUseCase)
        {
            _subcategoriaHelper = subcategoriaHelper;
            _produtoHelper = produtoHelper;
            _mapper = mapper;
            _cadastroProdutoUseCase = cadastroProdutoUseCase;
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

                var produtoDominio = _mapper.Map<Dominio.Produto>(produto);
                
                _cadastroProdutoUseCase.Inserir(produtoDominio);

                ViewBag.SubCategorias = _subcategoriaHelper.CacheSubCategorias();
                var produtosPersistidos = _produtoHelper.CacheProdutos(renovaCache: true);
                
                return View(produtosPersistidos);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }

}

