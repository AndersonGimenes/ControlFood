using AutoMapper;
using ControlFood.Api.Helpers.Interface;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using ControlFood.Api.Models.Produto;
using ControlFood.Domain.Entidades.Produto;

namespace ControlFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
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
        public IActionResult ObterTodos()
        {
            try
            {
                var produtos = _produtoHelper.CacheProdutos(renovaCache: false);
                return Ok(produtos);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost]
        public IActionResult Cadastrar(ProdutoRequest produto)
        {
            try
            {
                var produtoDominio = _mapper.Map<ProdutoVenda>(produto);

                _cadastroProdutoUseCase.Inserir(produtoDominio);
                var produtos = _produtoHelper.CacheProdutos(renovaCache: true);

                return Ok(produtos);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete]
        public IActionResult Deletar(ProdutoRequest produto)
        {
            try
            {
                // var produtoDominio = _mapper.Map<Dominio.Produto>(produto);

                // _cadastroProdutoUseCase.Deletar(produtoDominio);
                // _produtoHelper.CacheProdutos(renovaCache: true);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Atualizar(ProdutoRequest produto)
        {
            try
            {
                var produtoDominio = _mapper.Map<ProdutoVenda>(produto);

                _cadastroProdutoUseCase.AtualizarProduto(produtoDominio);
                _produtoHelper.CacheProdutos(renovaCache: true);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}

