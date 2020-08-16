using ControlFood.Domain.Entidades;
using ControlFood.Domain.Enuns;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ControlFood.UnitTest.UseCase
{
    public class PedidoUseCaseTest
    {
        private readonly Mock<IGenericRepository<Pedido>> _mockGenericRepository;
        private readonly Mock<INotificaUseCase> _mockNotificaUseCase;
        private readonly IPedidoUseCase _pedidoUseCase;

        public PedidoUseCaseTest()
        {
            _mockGenericRepository = new Mock<IGenericRepository<Pedido>>();
            _mockNotificaUseCase = new Mock<INotificaUseCase>();

            _pedidoUseCase = new PedidoUseCase(_mockNotificaUseCase.Object, _mockGenericRepository.Object);
        }

        [Fact]
        public void SeValorDoPedidoNaoConferirComASomaDosItensDeveLancarUmaExeccao()
        {
            var pedido = this.MockPedido();
            pedido.Valor = 10;

            var ex = Assert.Throws<PedidoIncorretoUseCaseException>(() => _pedidoUseCase.RealizarPedido(pedido));
            Assert.Equal("Valor incorreto: valor dos itens 15, valor do pedido 10.", ex.Message);
        }

        [Fact]
        public void DeveInserirUmPedidoComSucesso()
        {
            var pedido = this.MockPedido();

            _mockGenericRepository
               .Setup(x => x.Inserir(It.IsAny<Pedido>()))
               .Returns(InserirNumeroPedido(pedido));

            _mockNotificaUseCase
                .Setup(x => x.NotificarPedidoPreparo(It.IsAny<Pedido>()))
                .Callback(() => pedido.StatusPedido = StatusPedido.EmPreparo);

            _pedidoUseCase.RealizarPedido(pedido);

            Assert.Equal(1, pedido.Numero);
            Assert.Equal(StatusPedido.EmPreparo, pedido.StatusPedido);

        }

        [Fact]
        public void DeveAtualizarOPedidoParaPagoEPassarUmaFormaDePagamento()
        {
            var pedido = MockPedido();

            _pedidoUseCase.RealizarPagamento(pedido, FormaPagamento.Debito);

            Assert.True(pedido.PedidoPago);
            Assert.Equal(FormaPagamento.Debito, pedido.FormaPagamento);
        }

        private Pedido InserirNumeroPedido(Pedido pedido)
        {
            pedido.Numero = 1;
            return pedido;
        }

        private Pedido MockPedido()
        {
            var pedido = new Pedido
            {
                Valor = 15.00M,
            };

            pedido.Items = new List<Item>
            {
                new Item { Nome = "X-Bacon", Valor = 5 },
                new Item { Nome = "X-Egg", Valor = 5 },
                new Item { Nome = "Coca-cola 2L", Valor = 5 }
            };

            return pedido;
        }
    }
}
