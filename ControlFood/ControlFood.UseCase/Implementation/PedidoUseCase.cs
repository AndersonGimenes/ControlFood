using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.Domain.Enuns;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using System.Linq;

namespace ControlFood.UseCase.Implementation
{
    public class PedidoUseCase : IPedidoUseCase
    {
        private readonly INotificaUseCase _notificaUseCase;
        private readonly IGenericRepository<Pedido> _genericRepository;

        public PedidoUseCase(INotificaUseCase notificaUseCase, IGenericRepository<Pedido> genericRepository)
        {
            _notificaUseCase = notificaUseCase;
            _genericRepository = genericRepository;
        }

        public Pedido RealizarPedido(Pedido pedido)
        {
            // validar pedido (validação sera na classe Pedido)
            _genericRepository.Inserir(pedido);

            _notificaUseCase.NotificarPedidoPreparo(pedido);

            return pedido;
        }

        public Pedido RealizarPagamento(Pedido pedido, FormaPagamento formaPagamento)
        {
            var pedidoDto = _genericRepository.BuscarPorId(pedido.IdentificadorUnico);
            if (pedidoDto is null)
                throw new PedidoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Comum.PedidoInexistente, pedido.IdentificadorUnico));
            
            pedido.PedidoPago = true;
            pedido.FormaPagamento = formaPagamento;
            
            _genericRepository.Atualizar(pedido);

            return pedido;
        }
    }
}
