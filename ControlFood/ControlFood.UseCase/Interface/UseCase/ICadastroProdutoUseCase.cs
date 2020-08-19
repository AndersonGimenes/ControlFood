using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.UseCase.Base;

namespace ControlFood.UseCase.Interface.UseCase
{
    public interface ICadastroProdutoUseCase : ICadastroBaseUseCase<Produto>
    {
        Produto BuscarPorIdentificacao(Produto produto);
    }
}
