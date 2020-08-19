using ControlFood.Domain.Entidades;
using ControlFood.Domain.Enuns;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using System;

namespace ControlFood.UseCase.Implementation
{
    public class NotificaUseCase : INotificaUseCase
    {
        private readonly IGenericRepository<Pedido> _genericRepository;

        public NotificaUseCase(IGenericRepository<Pedido> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public void NotificarPedidoPreparo(Pedido pedido)
        {
            pedido.StatusPedido = StatusPedido.EmPreparo;
            _genericRepository.Atualizar(pedido);

            // delegar uma formar de imprimir ou enfileirar pedido em tela
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
