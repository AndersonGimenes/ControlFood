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
        private readonly ICadastroEstoqueUseCase _cadastroEstoqueUseCase;

        public ProdutoController
        (
            ISubCategoriaHelper subcategoriaHelper, 
            IProdutoHelper produtoHelper, 
            IMapper mapper, 
            ICadastroProdutoUseCase cadastroProdutoUseCase,
            ICadastroEstoqueUseCase cadastroEstoqueUseCase
        )
        {
            _subcategoriaHelper = subcategoriaHelper;
            _produtoHelper = produtoHelper;
            _mapper = mapper;
            _cadastroProdutoUseCase = cadastroProdutoUseCase;
            _cadastroEstoqueUseCase = cadastroEstoqueUseCase;
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
                produto.IsValid();

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

        public IActionResult CadastrarEstoque(Produto produto)
        {
            try
            {
                produto.Estoque.IsValid();

                var produtoDominio = _mapper.Map<Dominio.Produto>(produto);
                
                _cadastroEstoqueUseCase.InserirEstoque(produtoDominio);

                produto.Mensagem = Constantes.Mensagem.EStoque.EStoqueCadastrado;
                return Json(produto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete]
        public IActionResult Deletar(Produto produto)
        {
            try
            {
                var produtoDominio = _mapper.Map<Dominio.Produto>(produto);

                _cadastroProdutoUseCase.Deletar(produtoDominio);
                _produtoHelper.CacheProdutos(renovaCache: true);

                produto.Mensagem = Constantes.Mensagem.Comum.ItemDeletado;
                return Json(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Atualizar(Produto produto)
        {
            try
            {
                produto.IsValid();

                var produtoDominio = _mapper.Map<Dominio.Produto>(produto);

                _cadastroProdutoUseCase.Atualizar(produtoDominio);
                _produtoHelper.CacheProdutos(renovaCache: true);

                produto.Mensagem = Constantes.Mensagem.Comum.ItemAtualizado;
                return Json(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}

