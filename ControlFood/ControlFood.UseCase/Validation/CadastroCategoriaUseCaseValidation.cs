using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroCategoriaUseCaseValidation
    {
        internal static void ValidarRegrasParaInserir(Categoria categoria, List<Categoria> categorias)
        {
            VerificarDuplicidade(categoria, categorias);
        }

        internal static void ValidarRegrasParaDeletar(Categoria categoria, List<SubCategoria> subCategorias)
        {
            VerificarSubCategoriaVinculada(categoria, subCategorias);
        }

        #region [ PRIVADOS ] 
        private static void VerificarDuplicidade(Categoria categoria, List<Categoria> categorias)
        {
            if (categorias.Any(c => c.Tipo == categoria.Tipo))
                throw new CategoriaIncorretaUseCaseException(string.Format(Domain.Constantes.Mensagem.Validacao.Categoria.CategoriaDuplicada, categoria.Tipo));
        }

        private static void VerificarSubCategoriaVinculada(Categoria categoria, List<SubCategoria> subCategorias)
        {
            if (subCategorias.Any(x => x.Categoria.IdentificadorUnico == categoria.IdentificadorUnico))
                throw new CategoriaIncorretaUseCaseException(string.Format(Domain.Constantes.Mensagem.Validacao.Categoria.CategoriaVinculadaASubCategoria, categoria.Tipo));
        }
        #endregion
    }
}
