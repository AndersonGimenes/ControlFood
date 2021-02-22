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
        internal static void ValidarRegrasParaInserir(Produto produto, IEnumerable<Produto> produtos, IEnumerable<Categoria> categorias, IEnumerable<Adicional> adcionais)
        {
            // Verifica se existe outro produto cadastrado com mesmo nome
            ComumValidation<Produto>
                .VerificarDuplicidade(produto, produtos, nameof(produto.Nome), () => throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorNome, produto.Nome)));

            // Verifica se existe outro produto cadastrado com mesmo codigo interno
            ComumValidation<Produto>
                .VerificarDuplicidade(produto, produtos, nameof(produto.CodigoInterno), () => throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorCodigo, produto.CodigoInterno)));

            // Verifica se existe categoria vinculada para inserir o produto
            ComumValidation<Categoria>
                .VerificarVinculoInserir(produto.Categoria, categorias, nameof(produto.Categoria.IdentificadorUnico), () => throw new ProdutoIncorretoUseCaseException(Mensagem.Validacao.Produto.CategoriaNaoVinculadaAoProduto));

            if(produto.Adicionais.Count != 0 && !produto.Adicionais.TrueForAll(z => adcionais.Any(x => x.IdentificadorUnico == z.IdentificadorUnico)))
                throw new ProdutoIncorretoUseCaseException(Mensagem.Validacao.Produto.ProdutoSemAdicional);
        }
    }
}
