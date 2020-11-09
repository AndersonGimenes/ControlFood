﻿using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroEstoqueUseCase : CadastroBaseUseCase<Estoque>, ICadastroEstoqueUseCase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueRepository _estoqueRepository;

        public CadastroEstoqueUseCase(IEstoqueRepository estoqueRepository, IProdutoRepository produtoRepository)
            : base(estoqueRepository)
        {
            _produtoRepository = produtoRepository;
            _estoqueRepository = estoqueRepository;
        }

        public Produto InserirEstoque(Produto produto)
        {
            var produtos = _produtoRepository.BuscarTodos();

            produto.Estoque.AtribuirDataDeEntrada();
            produto.Estoque.AtribuirIdentificadorUnicoProduto(produto.IdentificadorUnico);

            CadastroEstoqueUseCaseValidation.ValidarRegrasParaInserir(produto, produtos);

            base.Inserir(produto.Estoque);

            return produto;
        }

        public List<Produto> BuscarDadosProdutoXEstoques(Produto produto)
        {
            var lista = _estoqueRepository.BuscarTodosPorProduto(produto);
            var listaFiltrada = lista.Where(e => e.DataValidade >= DateTime.Today).ToList();

            return RetonarListaProdutoXEstoque(listaFiltrada);
        }

        public Produto AtualizarEstoque(Produto produto)
        {
            produto.Estoque.AtribuirDataDeAlteracao();
            produto.Estoque.AtribuirIdentificadorUnicoProduto(produto.IdentificadorUnico);

            base.Atualizar(
                produto.Estoque, 
                new List<string> 
                {
                    nameof(produto.Estoque.DataAlteracao), nameof(produto.Estoque.DataValidade), nameof(produto.Estoque.Quantidade), nameof(produto.Estoque.ValorCompraTotal), nameof(produto.Estoque.ValorCompraUnidade) 
                });

            return produto;
        }

        private List<Produto> RetonarListaProdutoXEstoque(List<Estoque> estoques)
        {
            var produtos = new List<Produto>();
            estoques.ForEach(estoque =>
            {
                produtos.Add(new Produto { IdentificadorUnico = estoque.IdentificadorUnicoProduto, Estoque = estoque });
            });

            return produtos;
        }
    }
}
