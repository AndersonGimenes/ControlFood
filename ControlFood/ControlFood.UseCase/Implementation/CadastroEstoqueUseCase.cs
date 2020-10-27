using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroEstoqueUseCase : CadastroBaseUseCase<Estoque>, ICadastroEstoqueUseCase
    {
        private readonly IProdutoRepository _produtoRepository;

        public CadastroEstoqueUseCase(IEstoqueRepository estoqueRepository, IProdutoRepository produtoRepository) 
            : base(estoqueRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public Produto InserirEstoque(Produto produto)
        {
            VerificarProdutoVinculado(_produtoRepository.BuscarTodos(), produto);

            produto.Estoque.AtribuirDataDeEntrada();

            base.Inserir(produto.Estoque);

            // ajustar 
            return default;
        }

        private void VerificarProdutoVinculado(List<Produto> produtos, Produto produto)
        {
            if(!produtos.Any(x => x.IdentificadorUnico == produto.Estoque.IdentificadorUnicoProduto))
                throw new ProdutoIncorretoUseCaseException(Mensagem.Validacao.Produto.ProdutoInexistente);
        }
    }
}
