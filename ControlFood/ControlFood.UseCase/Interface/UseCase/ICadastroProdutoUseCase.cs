using ControlFood.Domain.Entidades.Produto;
using ControlFood.UseCase.Interface.UseCase.Base;

namespace ControlFood.UseCase.Interface.UseCase
{
    public interface ICadastroProdutoUseCase : IGenericCadastroUseCase<ProdutoVenda>
    {
        void AtualizarProduto(ProdutoVenda produto);
    }
}
