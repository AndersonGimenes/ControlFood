using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroEstoqueUseCaseValidation
    {
        internal static void ValidarRegrasParaInserir(Produto produto, List<Produto> produtos)
        {
            VerificarProdutoVinculado(produtos, produto);
            VerificarValidade(produto);
            VerificarValoresParaPersistencia(produto);
        }

        #region [ PRIVADOS ]
        private static void VerificarValoresParaPersistencia(Produto produto)
        {
            if ((produto.Estoque.Quantidade * produto.Estoque.ValorCompraUnidade) != produto.Estoque.ValorCompraTotal)
                throw new ProdutoIncorretoUseCaseException(Mensagem.Validacao.Produto.ValoresDivergentes);
        }

        private static void VerificarValidade(Produto produto)
        {
            if (produto.Estoque.DataValidade <= DateTime.Today)
                throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ValidadeIncorreta, DateTime.Today.ToString("dd/MM/yyyy")));
        }

        private static void VerificarProdutoVinculado(List<Produto> produtos, Produto produto)
        {
            if (!produtos.Any(x => x.IdentificadorUnico == produto.Estoque.IdentificadorUnicoProduto))
                throw new ProdutoIncorretoUseCaseException(Mensagem.Validacao.Produto.ProdutoInexistente);
        }
        #endregion

    }
}
