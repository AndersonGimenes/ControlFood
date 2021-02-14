using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Validation.Comum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroProdutoUseCaseValidation
    {
        internal static void ValidarRegrasParaInserir(Produto produto, IEnumerable<Produto> produtos, IEnumerable<Categoria> categorias)
        {
            // Verifica se existe outro produto cadastrado com mesmo nome
            ComumValidation<Produto>
                .VerificarDuplicidade(produto, produtos, nameof(produto.Nome), () => throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorNome, produto.Nome)));

            // Verifica se existe outro produto cadastrado com mesmo codigo interno
            ComumValidation<Produto>
                .VerificarDuplicidade(produto, produtos, nameof(produto.CodigoInterno), () => throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorCodigo, produto.CodigoInterno)));

            // Verifica se existe sub-categoria vinculada para inserir o produto
            // Refatorar = verificar se existe alguma categoria vinculada para inserir o produto no banco
            ComumValidation<Categoria>
                .VerificarVinculoInserir(produto.Categoria, categorias, nameof(produto.Categoria.IdentificadorUnico), () => throw new ProdutoIncorretoUseCaseException(Mensagem.Validacao.Produto.CategoriaNaoVinculadaAoProduto));
        }

        //internal static void ValidarRegrasParaDeletar(Produto produto, List<Estoque> estoques)
        //{
        //    // Verfica se existe algum estoque vinculadoao produto a ser deletado
        //    VerificarEstoqueVinculado(produto, estoques);
        //}

        #region[ PRIVADOS ]
        //private static void VerificarEstoqueVinculado(Produto produto, List<Estoque> estoques)
        //{
        //    if (estoques.Any(e => e.IdentificadorUnicoProduto == produto.IdentificadorUnico))
        //        throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.EstoqueVinculado, produto.Nome));
        //}        
        #endregion
    }
}
