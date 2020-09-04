using ControlFood.Domain.Entidades;
using ControlFood.Domain.Enuns;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ControlFood.UnitTest.UseCase
{
    public class NotificaUseCaseTest
    {
        private readonly Mock<IGenericRepository<Pedido>> _genericRepository;
        private readonly INotificaUseCase _notificaUseCase;

        public NotificaUseCaseTest()
        {
            _genericRepository = new Mock<IGenericRepository<Pedido>>();
            _notificaUseCase = new NotificaUseCase(_genericRepository.Object);
        }

        [Fact]
        public void DeveColocarOStatusDoPedidoEmPreparo()
        {
            var pedido = new Pedido
            {
                IdentificadorUnico = 10,
                Valor = 50,
                StatusPedido = StatusPedido.SemStatus,
                Desconto = 0
            };
            pedido.Items = new List<Produto>();
            pedido.Items = ItensMock();

            _notificaUseCase.NotificarPedidoPreparo(pedido);

            Assert.Equal(StatusPedido.EmPreparo, pedido.StatusPedido);
        }

        private List<Produto> ItensMock()
        {
            var itens = new List<Produto>();

            var xEgg = new Produto { Nome = "X-Egg", ValorVenda = 10 };
            xEgg.Categoria = new Categoria { ProdutoCategoria = ProdutoCategoria.Alimento, ProdutoSubCategoria = ProdutoSubCategoria.Lanche };
            itens.Add(xEgg);

            var hotdog = new Produto { Nome = "Hot Morte", ValorVenda = 12 };
            hotdog.Categoria = new Categoria { ProdutoCategoria = ProdutoCategoria.Alimento, ProdutoSubCategoria = ProdutoSubCategoria.HotDog };
            itens.Add(hotdog);

            var pastel = new Produto { Nome = "Pastel de carne", ValorVenda = 8 };
            pastel.Categoria = new Categoria { ProdutoCategoria = ProdutoCategoria.Alimento, ProdutoSubCategoria = ProdutoSubCategoria.Pastel };
            itens.Add(pastel);

            var cocaCola = new Produto { Nome = "Coca cola 600ml", ValorVenda = 8 };
            cocaCola.Categoria = new Categoria { ProdutoCategoria = ProdutoCategoria.Bebida, ProdutoSubCategoria = ProdutoSubCategoria.Refrigerante };
            itens.Add(cocaCola);

            var cerveja = new Produto { Nome = "Skol 350ml", ValorVenda = 5 };
            cerveja.Categoria = new Categoria { ProdutoCategoria = ProdutoCategoria.Bebida, ProdutoSubCategoria = ProdutoSubCategoria.Cerveja };
            itens.Add(cerveja);
            
            var suco = new Produto { Nome = "Suco de Laranja 300ml", ValorVenda = 7 };
            suco.Categoria = new Categoria { ProdutoCategoria = ProdutoCategoria.Bebida, ProdutoSubCategoria = ProdutoSubCategoria.Suco };
            itens.Add(suco);

            return itens;    
        }

    }
}
