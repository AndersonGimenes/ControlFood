using ControlFood.Domain.Entidades;
using ControlFood.Domain.Enuns;

namespace ControlFood.UseCase.Interface.UseCase
{
    public interface IPedidoUseCase
    {
        Pedido RealizarPedido(Pedido pedido);
        Pedido RealizarPagamento(Pedido pedido, FormaPagamento formaPagamento);
    }
}
