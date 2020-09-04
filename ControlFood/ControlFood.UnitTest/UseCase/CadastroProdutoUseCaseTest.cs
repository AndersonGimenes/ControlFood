using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Moq;
using System;
using Xunit;

namespace ControlFood.UnitTest.UseCase
{
    public class CadastroProdutoUseCaseTest
    {
        private readonly Mock<IGenericRepository<Produto>> _mockGeneciRepository;
        private readonly ICadastroProdutoUseCase _cadastroProduto;

        public CadastroProdutoUseCaseTest()
        {
            _mockGeneciRepository = new Mock<IGenericRepository<Produto>>();
            _cadastroProduto = new CadastroProdutoUseCase(_mockGeneciRepository.Object);
        }

        [Fact]
        public void DeveInserirUmProdutoNoSistemaComSucesso()
        {
            var produto = MockNovoProduto();

            _mockGeneciRepository
                .Setup(x => x.Inserir(It.IsAny<Produto>()))
                .Returns(AdicionarIdentificador(produto));

            _cadastroProduto.Inserir(produto);

            Assert.Equal(1, produto.IdentificadorUnico);
        }

        [Fact]
        public void DeveAtualizarOsDadosDoProdutoNoSistemaComSucesso()
        {
            var produtoRequest = MockNovoProduto();
            produtoRequest.ValorCompra = 3.50M;
            produtoRequest.IdentificadorUnico = 2;
            Produto produtoBase = default;

            _mockGeneciRepository
                .Setup(x => x.Atualizar(It.IsAny<Produto>()))
                .Callback(() => {
                    produtoBase = MockProdutoAtualizar(produtoRequest);
                });

            _cadastroProduto.Atualizar(produtoRequest);

            Assert.Equal(3.50M, produtoBase.ValorCompra);
        }

        [Fact]
        public void DeveBuscarOsDadosDoProdutoNoSistemaComSucesso()
        {
            var produtoRequest = MockProdutoPersistido();

            _mockGeneciRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns(MockProdutoBuscar(produtoRequest.IdentificadorUnico));

            var retorno = _cadastroProduto.BuscarPorIdentificacao(produtoRequest, nameof(produtoRequest.IdentificadorUnico));

            Assert.True(retorno != null);
            Assert.Equal(produtoRequest.IdentificadorUnico, retorno.IdentificadorUnico);
        }


        private Produto AdicionarIdentificador(Produto produto)
        {
            produto.IdentificadorUnico = 1;
            return produto;
        }

        private Produto MockProdutoAtualizar(Produto produtoRequest)
        {
            var produtoBase = MockProdutoPersistido();

            if (produtoRequest.IdentificadorUnico == produtoBase.IdentificadorUnico)
            {
                produtoBase.ValorCompra = produtoRequest.ValorCompra;
            }

            return produtoBase;
        }

        private Produto MockProdutoBuscar(int id)
        {
            var produto = MockProdutoPersistido();

            if (id == produto.IdentificadorUnico)
                return produto;

            return default;
        }

        private Produto MockNovoProduto()
        {
            return new Produto
            {
                Nome = "Coca-cola",
                Codigo = "0001",
                Validade = new DateTime(2021, 06, 15),
                ValorCompra = 2,
                ValorVenda = 4
            };
        }

        private Produto MockProdutoPersistido()
        {
            var produto = MockNovoProduto();
            produto.IdentificadorUnico = 2;

            return produto;
        }
    }
}
