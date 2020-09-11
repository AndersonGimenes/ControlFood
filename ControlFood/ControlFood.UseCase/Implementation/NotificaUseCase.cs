using ControlFood.Domain.Entidades;
using ControlFood.Domain.Enuns;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using System;
using System.Collections.Generic;

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

            this.SepararPedido(pedido);
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

        #region [ METODOS PRIVADOS ]

        private void SepararPedido(Pedido pedido)
        {
            var pedidoCozinha = new List<Produto>();
            var pedidoBar = new List<Produto>();
            
            pedido.Itens.ForEach(item =>
            {
            if (item.SubCategoria.IndicadorItemCozinha)
            {
                pedidoCozinha.Add(item);
            }
            else if (item.SubCategoria.IndicadorItemBar)
                {
                    pedidoBar.Add(item);
                }
            });

            this.NotificarCozinha(pedido.IdentificadorUnico, pedidoCozinha);
            this.NotificarBar(pedido.IdentificadorUnico, pedidoBar);
        }

        private void NotificarCozinha(int numero, List<Produto> items)
        {
            // imprime ou mostra em tela 
            
        }

        private void NotificarBar(int numero, List<Produto> items)
        {
            // imprime ou mostra em tela 
        }

        #endregion
    }
}
