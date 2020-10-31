using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Validation.Comum;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroCategoriaUseCaseValidation
    {
       
        internal static void ValidarRegrasParaInserir(Categoria categoria, List<Categoria> categorias)
        {
            // Verifica se existe outra categoria cadastrada com mesmo nome
            ComumValidation<Categoria>
                .VerificarDuplicidade(categoria, categorias, nameof(categoria.Tipo), () => throw new CategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.Categoria.CategoriaDuplicada, categoria.Tipo)));
        }

        internal static void ValidarRegrasParaDeletar(Categoria categoria, List<SubCategoria> subCategorias)
        {
            var subCategoriasCast = subCategorias
                                        .Cast<object>()
                                        .ToList();

            // Verfica se existe alguma sub-categoria vinculada a categoria a ser deletada
            ComumValidation<Categoria>
                .VerificarVinculoDeletar(categoria, subCategoriasCast, nameof(SubCategoria.Categoria), nameof(categoria.IdentificadorUnico), () => throw new CategoriaIncorretaUseCaseException(string.Format(Domain.Constantes.Mensagem.Validacao.Categoria.CategoriaVinculadaASubCategoria, categoria.Tipo)));
        }
    }
}
