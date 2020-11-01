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
        internal static void ValidarRegrasParaInserir(Produto produto, List<Produto> produtos, List<SubCategoria> subCategorias)
        {
            // Verifica se existe outro produto cadastrado com mesmo nome
            ComumValidation<Produto>
                .VerificarDuplicidade(produto, produtos, nameof(produto.Nome), () => throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorNome, produto.Nome)));

            // Verifica se existe outro produto cadastrado com mesmo codigo interno
            ComumValidation<Produto>
                .VerificarDuplicidade(produto, produtos, nameof(produto.CodigoInterno), () => throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorCodigo, produto.CodigoInterno)));

            // Verifica se existe sub-categoria vinculada para inserir o produto
            ComumValidation<SubCategoria>
                .VerificarVinculoInserir(produto.SubCategoria, subCategorias, nameof(produto.SubCategoria.IdentificadorUnico), () => throw new ProdutoIncorretoUseCaseException(Mensagem.Validacao.Produto.SubCategoriaNaoVinculadaAoProduto));
        }

        internal static void ValidarRegrasParaDeletar(Produto produto, List<Estoque> estoques)
        {
            // Verfica se existe algum estoque vinculadoao produto a ser deletado
            VerificarEstoqueVinculado(produto, estoques);
        }

        internal static void ValidarRegrasParaAtualizar(Produto produto, Produto produtoPersistido)
        {
            // validar campos que não podem ser atualizados
            ComumValidation<Produto>
                .VerificarTiposAtaulizacao(produtoPersistido, produto, nameof(produto.Nome), () => throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Comum.EdicaoInvalida, nameof(produto.Nome))));

            ComumValidation<Produto>
                .VerificarTiposAtaulizacao(produtoPersistido, produto, nameof(produto.CodigoInterno), () => throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Comum.EdicaoInvalida, nameof(produto.CodigoInterno))));

            ComumValidation<SubCategoria>
                .VerificarTiposAtaulizacao(produtoPersistido.SubCategoria, produto.SubCategoria, nameof(produto.SubCategoria.IdentificadorUnico), () => throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Comum.EdicaoInvalida, nameof(produto.SubCategoria.IdentificadorUnico))));
        }

        #region[ PRIVADOS ]
        private static void VerificarEstoqueVinculado(Produto produto, List<Estoque> estoques)
        {
            if (estoques.Any(e => e.IdentificadorUnicoProduto == produto.IdentificadorUnico))
                throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.EstoqueVinculado, produto.Nome));
        }        
        #endregion
    }
}
