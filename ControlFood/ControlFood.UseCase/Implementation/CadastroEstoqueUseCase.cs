using ControlFood.Domain.Entidades;
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

            CadastroEstoqueUseCaseValidation.ValidarRegrasParaInserir(produto, produtos);

            produto.Estoque.AtribuirDataDeEntrada();
            produto.Estoque.AtribuirIdentificadorUnicoProduto(produto.IdentificadorUnico);

            base.Inserir(produto.Estoque);
                         
            return produto;
        }

        public List<Estoque> BuscarDadosProdutoXEstoques(Produto produto)
        {
            var lista = _estoqueRepository.BuscarTodosPorProduto(produto);
            var listaFiltrada = lista.Where(e => e.DataValidade >= DateTime.Today).ToList();

            return listaFiltrada;
        }
    }
}
