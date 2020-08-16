using ControlFood.Domain.Entidades;

namespace ControlFood.UseCase.Interface.UseCase
{
    public interface INotificaUseCase
    {
        void NotificarPedidoPreparo(Pedido pedido);
        void NotificarPedidoPronto(Pedido pedido);
        void NotificarPedidoEmRota(Pedido pedido);
        void NotificarPedidoRealizado(Pedido pedido);
    }
}
