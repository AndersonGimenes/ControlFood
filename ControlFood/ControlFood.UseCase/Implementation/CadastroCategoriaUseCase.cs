using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroCategoriaUseCase : CadastroBaseUseCase<Categoria>, ICadastroCategoriaUseCase
    {
        public CadastroCategoriaUseCase(ICategoriaRepository categoriaRepository)
           : base(categoriaRepository)
        {
        }
    }
}
