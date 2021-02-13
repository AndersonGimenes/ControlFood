using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ControlFood.Api.Helpers.Interface;
using ControlFood.Api.Models;

namespace ControlFood.Api.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoHelper _produtoHelper;
        private readonly IMapper _mapper;
        private readonly ICadastroProdutoUseCase _cadastroProdutoUseCase;
        private readonly ICadastroEstoqueUseCase _cadastroEstoqueUseCase;

        public ProdutoController
        (
            IProdutoHelper produtoHelper,
            IMapper mapper,
            ICadastroProdutoUseCase cadastroProdutoUseCase,
            ICadastroEstoqueUseCase cadastroEstoqueUseCase
        )
        {
            _produtoHelper = produtoHelper;
            _mapper = mapper;
            _cadastroProdutoUseCase = cadastroProdutoUseCase;
            _cadastroEstoqueUseCase = cadastroEstoqueUseCase;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var produtos = _produtoHelper.CacheProdutos();

            return View(produtos);
        }

        [HttpPost]
        public IActionResult Cadastrar(Produto produto)
        {
            try
            {
                var produtoDominio = _mapper.Map<Dominio.Produto>(produto);

                _cadastroProdutoUseCase.Inserir(produtoDominio);

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
                var produtoDominio = _mapper.Map<Dominio.Produto>(produto);

                _cadastroProdutoUseCase.AtualizarProduto(produtoDominio);
                _produtoHelper.CacheProdutos(renovaCache: true);

                produto.Mensagem = Constantes.Mensagem.Comum.ItemAtualizado;
                return Json(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult BuscarEstoque(Produto produto)
        {
            try
            {
                var produtoDominio = _mapper.Map<Dominio.Produto>(produto);

                var produtos = _cadastroEstoqueUseCase.BuscarDadosProdutoXEstoques(produtoDominio);

                return Json(_mapper.Map<List<Produto>>(produtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult AtualizarEstoque(Produto produto)
        {
            try
            {
                var produtoDominio = _mapper.Map<Dominio.Produto>(produto);

                _cadastroEstoqueUseCase.AtualizarEstoque(produtoDominio);

                produto.Mensagem = Constantes.Mensagem.Comum.ItemAtualizado;
                return Json(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeletarEstoque(Produto produto)
        {
            try
            {
                var produtoDominio = _mapper.Map<Dominio.Produto>(produto);

                _cadastroEstoqueUseCase.Deletar(produtoDominio.Estoque);

                produto.Mensagem = Constantes.Mensagem.Comum.ItemDeletado;
                return Json(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}

