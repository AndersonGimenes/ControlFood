using ControlFood.Domain.Entidades;
using ControlFood.Domain.Enuns;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Moq;
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

            Assert.Equal(1, pedido.IdentificadorUnico);
            Assert.Equal(StatusPedido.EmPreparo, pedido.StatusPedido);

        }

        [Fact]
        public void DeveAtualizarOPedidoParaPagoEPassarUmaFormaDePagamento()
        {
            var pedido = MockPedido();
            pedido.IdentificadorUnico = 10;

            _mockGenericRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns(pedido);

            _pedidoUseCase.RealizarPagamento(pedido, FormaPagamento.Debito);

            Assert.True(pedido.PedidoPago);
            Assert.Equal(FormaPagamento.Debito, pedido.FormaPagamento);
        }

        [Fact]
        public void NaoDeveRealizarPagamentoParaPedidoInexistenteEDeveLancarUmPedidoIncorretoException()
        {
            var pedido = MockPedido();
            pedido.IdentificadorUnico = 10;

            _mockGenericRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns((Pedido)null);

            var ex = Assert.Throws<PedidoIncorretoUseCaseException>(() => _pedidoUseCase.RealizarPagamento(pedido, FormaPagamento.Debito));
            Assert.Equal("O pedido numero 10 não confere no sistema, por favor verifique o numero do pedido", ex.Message);
        }


        private Pedido InserirNumeroPedido(Pedido pedido)
        {
            pedido.IdentificadorUnico = 1;
            return pedido;
        }

        private Pedido MockPedido()
        {
            var pedido = new Pedido
            {
                Valor = 15.00M,
            };

            pedido.Itens.Add(new Produto { Nome = "X-Bacon", ValorVenda = 5 });
            pedido.Itens.Add(new Produto { Nome = "X-Egg", ValorVenda = 5 });
            pedido.Itens.Add(new Produto { Nome = "Coca-cola 2L", ValorVenda = 5 });
            
            return pedido;
        }
    }
}
