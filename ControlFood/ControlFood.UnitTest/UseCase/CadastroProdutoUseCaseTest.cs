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
        private readonly Mock<ISubCategoriaRepository> _mockSubCategoriaRepository;
        private readonly Mock<IProdutoRepository> _mockProdutoRepository;
        private readonly ICadastroProdutoUseCase _cadastroProduto;

        public CadastroProdutoUseCaseTest()
        {
            _mockSubCategoriaRepository = new Mock<ISubCategoriaRepository>();
            _mockProdutoRepository = new Mock<IProdutoRepository>();

            _cadastroProduto = new CadastroProdutoUseCase(_mockProdutoRepository.Object, _mockSubCategoriaRepository.Object);

            _mockSubCategoriaRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaSubCategoriasPersistidas());

            _mockProdutoRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaProdutosPersistidos());
        }

        [Fact]
        public void DeveInserirUmProdutoNoSistemaComSucesso()
        {
            var produto = HelperMock.MockProduto("gra350", "Guarana antarica lata 350ml");

            _mockProdutoRepository
                .Setup(x => x.Inserir(It.IsAny<Produto>()))
                .Returns(() => {
                    produto.IdentificadorUnico = 4;
                    return produto;
                });

            _cadastroProduto.Inserir(produto);

            Assert.Equal(4, produto.IdentificadorUnico);
        }

        [Theory]
        [InlineData("O produto com codigo cc350 ja existe no sistema", "cc350", "Coca-cola")]
        [InlineData("O produto com nome Coca-cola lata 350ml ja existe no sistema", "cc001", "Coca-cola lata 350ml")]
        [InlineData("O produto com nome Coca-cola lata 350ml ja existe no sistema", "cc350", "Coca-cola lata 350ml")]
        public void DeveLancarUmaExceptionCasoOProdutoSejaDuplicadoOuPorNomeOuPorCodigo(string result, string codigo, string nome)
        {
            var produto = HelperMock.MockProduto(codigo, nome);

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroProduto.Inserir(produto));
            Assert.Equal(result, ex.Message);
        }

        [Fact]
        public void CasoNaoExistaUmaSubCategoriaVinculadaAoProdutoDeveSerLancadaUmaException()
        {
            var produto = HelperMock.MockProduto("xpto", "Xtapa", idSubCategoria: 5);

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroProduto.Inserir(produto));
            Assert.Equal("Produto precisa estar vinculada a uma sub-categoria", ex.Message);
        }

        [Fact]
        public void DeveBuscaTodososProdutosPersistidosNoBanco()
        {
            var produtos = _cadastroProduto.BuscarTodos();

            Assert.NotNull(produtos);
            Assert.True(produtos.Count > 0);
        }


        private Produto AdicionarIdentificador(Produto produto)
        {
            produto.IdentificadorUnico = 1;
            return produto;
        }

    }
}
