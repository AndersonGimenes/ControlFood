using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.UseCase;
using System;

namespace ControlFood.UseCase.Implementation
{
    public class NotificaUseCase : INotificaUseCase
    {
        public void NotificarPedidoPreparo(Pedido pedido)
        {
            // implementar regra para emfileirar pedido
            // criar metodo notificar por monitor ou impresso
            throw new NotImplementedException();
        }

        public void NotificarPedidoPronto(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public void NotificarPedidoEmRota(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public void NotificarPedidoRealizado(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        private void AtualizarStatusPedido(Pedido pedido)
        {
            // atualizar 
        }
    }
}
