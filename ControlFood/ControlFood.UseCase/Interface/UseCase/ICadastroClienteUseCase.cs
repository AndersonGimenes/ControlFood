using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.UseCase.Base;

namespace ControlFood.UseCase.Interface.UseCase
{
    public interface ICadastroClienteUseCase : IGenericCadastroUseCase<Cliente>
    {
        Cliente BuscarPorIdentificacao(Cliente cliente);
        void AtualizarCliente(Cliente cliente);
    }
}
