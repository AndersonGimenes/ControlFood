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
            ItensMock(pedido);

            _notificaUseCase.NotificarPedidoPreparo(pedido);

            Assert.Equal(StatusPedido.EmPreparo, pedido.StatusPedido);
        }

        private void ItensMock(Pedido pedido)
        {
            var xEgg = new Produto { Nome = "X-Egg", ValorVenda = 10 };
            xEgg.Categoria.Tipo = "Alimento";
            xEgg.Categoria.SubCategoria.Tipo = "Lanche";
            xEgg.Categoria.SubCategoria.IndicadorItemCozinha = true;
            pedido.Itens.Add(xEgg);

            var hotdog = new Produto { Nome = "Hot Morte", ValorVenda = 12 };
            hotdog.Categoria.Tipo = "Alimento";
            hotdog.Categoria.SubCategoria.Tipo = "Cachorro quente";
            hotdog.Categoria.SubCategoria.IndicadorItemCozinha = true;
            pedido.Itens.Add(hotdog);

            var pastel = new Produto { Nome = "Pastel de carne", ValorVenda = 8 };
            pastel.Categoria.Tipo = "Alimento";
            pastel.Categoria.SubCategoria.Tipo = "Pastel";
            pastel.Categoria.SubCategoria.IndicadorItemCozinha = true;
            pedido.Itens.Add(pastel);

            var cocaCola = new Produto { Nome = "Coca cola 600ml", ValorVenda = 8 };
            cocaCola.Categoria.Tipo = "Bebida";
            cocaCola.Categoria.SubCategoria.Tipo = "Coca cola 350ml";
            cocaCola.Categoria.SubCategoria.IndicadorItemBar = true;
            pedido.Itens.Add(cocaCola);

            var cerveja = new Produto { Nome = "Skol 350ml", ValorVenda = 5 };
            cerveja.Categoria.Tipo = "Bebida";
            cerveja.Categoria.SubCategoria.Tipo = "Cerveja";
            cerveja.Categoria.SubCategoria.IndicadorItemBar = true;
            pedido.Itens.Add(cerveja);
            
            var suco = new Produto { Nome = "Suco de Laranja 300ml", ValorVenda = 7 };
            suco.Categoria.Tipo = "Bebida";
            suco.Categoria.SubCategoria.Tipo = "Suco";
            suco.Categoria.SubCategoria.IndicadorItemCozinha = true;
            pedido.Itens.Add(suco);

        }

    }
}
