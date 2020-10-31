using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Validation.Comum;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroSubCategoriaUseCaseValidation
    {
        internal static void ValidarRegrasParaInserir(SubCategoria subCategoria, List<Categoria> categorias, List<SubCategoria> subCategorias)
        {            
            ComumValidation<Categoria>
                .VerificarVinculoInserir(subCategoria.Categoria, categorias, nameof(subCategoria.Categoria.IdentificadorUnico), () => throw new SubCategoriaIncorretaUseCaseException(Mensagem.Validacao.SubCategoria.CategoriaNaoVinculadaASubCategoria));

            ComumValidation<SubCategoria>
                .VerificarDuplicidade(subCategoria, subCategorias, nameof(subCategoria.Tipo), () => throw new SubCategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.SubCategoria.SubCategoriaDuplicada, subCategoria.Tipo)));
        }

        internal static void ValidarRegrasParaAtualizar(SubCategoria subCategoria, SubCategoria subCategoriaPersistida)
        {
            VerificarTiposAtaulizacao(subCategoriaPersistida, subCategoria);
        }

        internal static void ValidarRegrasParaDeletar(SubCategoria subCategoria, List<Produto> produtos)
        {
            var produtosCast = produtos
                                    .Cast<object>()
                                    .ToList();
            
            ComumValidation<SubCategoria>
                .VerificarVinculoDeletar(subCategoria, produtosCast, nameof(Produto.SubCategoria), nameof(subCategoria.IdentificadorUnico), () => throw new SubCategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.SubCategoria.ProdutoVinculadoASubCategoria, subCategoria.Tipo)));
        }

        #region [ PRIVADOS ]

        private static void VerificarTiposAtaulizacao(SubCategoria subCategoriaPersistida, SubCategoria subCategoria)
        {
            if (subCategoria.Tipo != subCategoriaPersistida.Tipo)
                throw new SubCategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.Comum.EdicaoInvalida, nameof(subCategoria.Tipo)));

            if (subCategoria.Categoria.IdentificadorUnico != subCategoriaPersistida.Categoria.IdentificadorUnico)
                throw new SubCategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.Comum.EdicaoInvalida, nameof(subCategoria.Categoria.IdentificadorUnico)));
        }
        #endregion
    }
}
