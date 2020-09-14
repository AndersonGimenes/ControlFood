using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.UseCase.Base;
using System.Collections.Generic;

namespace ControlFood.UseCase.Interface.UseCase
{
    public interface ICadastroSubCategoriaUseCase : IGenericCadastroUseCase<SubCategoria>
    {
        void VerificarDuplicidade(SubCategoria subCategoria, List<SubCategoria> subCategorias);
        void VerificarCategoriaVinculada(SubCategoria subCategoria, List<Categoria> categorias);
    }
}
