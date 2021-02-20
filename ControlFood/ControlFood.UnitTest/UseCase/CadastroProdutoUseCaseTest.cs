using ControlFood.Domain.Entidades;
using ControlFood.UnitTest.UseCase.Helpers;
using ControlFood.UseCase.Exceptions;
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
        private readonly Mock<IProdutoRepository> _mockProdutoRepository;
        private readonly Mock<ICategoriaRepository> _mockCategoriaRepository;
        private readonly ICadastroProdutoUseCase _cadastroProduto;
       
        public CadastroProdutoUseCaseTest()
        {
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            _mockCategoriaRepository = new Mock<ICategoriaRepository>();

            _cadastroProduto = new CadastroProdutoUseCase(_mockProdutoRepository.Object, _mockCategoriaRepository.Object);

            _mockProdutoRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaProdutosPersistidos());

            _mockCategoriaRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaCategoriasPersistidas());
        }

        [Fact]
        public void DeveInserirUmProdutoNoSistemaComSucesso()
        {
            var produto = HelperMock.MockProduto("gra350", "Guarana antarctica lata 350ml", idProduto: 0, idCategoria: 4, adicionais: null);

            _mockProdutoRepository
                .Setup(x => x.Inserir(It.IsAny<Produto>()))
                .Returns(() =>
                {
                    produto.IdentificadorUnico = 5;
                    return produto;
                });

            _cadastroProduto.Inserir(produto);

            Assert.Equal(5, produto.IdentificadorUnico);
            Assert.True(produto.DataCadastro > DateTime.MinValue && produto.DataCadastro < DateTime.Now);
        }

        [Theory]
        [InlineData("O produto com codigo cc350 ja existe no sistema", "cc350", "Coca-cola")]
        [InlineData("O produto com nome Coca-cola lata 350ml ja existe no sistema", "cc001", "Coca-cola lata 350ml")]
        public void DeveLancarUmaExceptionCasoOProdutoSejaDuplicadoOuPorNomeOuPorCodigo(string result, string codigo, string nome)
        {
            var produto = HelperMock.MockProduto(codigo, nome, idProduto: 0, idCategoria: 4, adicionais: null);

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroProduto.Inserir(produto));
            Assert.Equal(result, ex.Message);
        }

        [Fact]
        public void CasoNaoExistaUmaCategoriaVinculadaAoProdutoDeveSerLancadaUmaException()
        {
            var produto = HelperMock.MockProduto("xpto", "Xtapa", idProduto: 0, idCategoria: 99, adicionais:null);

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroProduto.Inserir(produto));
            Assert.Equal("Produto precisa estar vinculada a uma categoria", ex.Message);
        }
    }
}
