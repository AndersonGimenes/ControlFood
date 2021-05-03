using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroEnderecoUseCase : CadastroBaseUseCase<Endereco>, ICadastroEnderecoUseCase
    {
        public CadastroEnderecoUseCase(IEnderecoRepository enderecoRepository)
           : base(enderecoRepository)
        {

        }
    }
}
