using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.UseCase.Base;

namespace ControlFood.UseCase.Interface.UseCase
{
    public interface ICadastroCategoriaUseCase : IGenericCadastroUseCase<Categoria>
    {
        void DeletarCategoria(int idCategoria);
    }
}
