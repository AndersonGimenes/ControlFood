using AutoMapper;
using ControlFood.Api.Helpers.Interface;
using ControlFood.Api.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Api.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoHelper _produtoHelper;
        private readonly IMapper _mapper;
        private readonly ICadastroProdutoUseCase _cadastroProdutoUseCase;

        public ProdutoController
        (
            IProdutoHelper produtoHelper,
            IMapper mapper,
            ICadastroProdutoUseCase cadastroProdutoUseCase
        )
        {
            _produtoHelper = produtoHelper;
            _mapper = mapper;
            _cadastroProdutoUseCase = cadastroProdutoUseCase;
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
    }

}

