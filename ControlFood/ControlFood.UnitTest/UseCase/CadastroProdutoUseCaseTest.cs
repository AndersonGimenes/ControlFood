using ControlFood.Domain.Entidades;
using ControlFood.UnitTest.UseCase.Helpers;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ControlFood.UnitTest.UseCase
{
    public class CadastroProdutoUseCaseTest
    {
        private readonly Mock<IProdutoRepository> _mockProdutoRepository;
        private readonly Mock<ICategoriaRepository> _mockCategoriaRepository;
        private readonly Mock<IAdicionalRepository> _mockAdicionalRepository;
        private readonly ICadastroProdutoUseCase _cadastroProduto;
       
        public CadastroProdutoUseCaseTest()
        {
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            _mockCategoriaRepository = new Mock<ICategoriaRepository>();
            _mockAdicionalRepository = new Mock<IAdicionalRepository>();

            _cadastroProduto = new CadastroProdutoUseCase(_mockProdutoRepository.Object, _mockCategoriaRepository.Object, _mockAdicionalRepository.Object);

            _mockProdutoRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaProdutosPersistidos());

            _mockCategoriaRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaCategoriasPersistidas());

            _mockAdicionalRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.ListaMockAdicionaisPersistidos());
        }

        [Fact]
        public void DeveInserirUmProdutoNoSistemaComSucesso()
        {
            var produto = HelperMock.MockProduto("gra350", "Guarana antarctica lata 350ml", idProduto: 0, idCategoria: 4, new List<Adicional>());

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


        [Fact]
        public void DeveInserirUmProdutoNoSistemaComSucessoOndeExistaUmAdcinalCadastrado()
        {
            var produto = HelperMock.MockProduto("XB-001", "X-Bacon", idProduto: 0, idCategoria: 1, new List<Adicional> { new Adicional { IdentificadorUnico = 1, Tipo = "Bacon", Valor = 2.00m } });

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
            var produto = HelperMock.MockProduto(codigo, nome, idProduto: 0, idCategoria: 4, adicionais: new List<Adicional>());

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroProduto.Inserir(produto));
            Assert.Equal(result, ex.Message);
        }

        [Fact]
        public void CasoNaoExistaUmaCategoriaVinculadaAoProdutoDeveSerLancadaUmaException()
        {
            var produto = HelperMock.MockProduto("xpto", "Xtapa", idProduto: 0, idCategoria: 99, adicionais: new List<Adicional>());

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroProduto.Inserir(produto));
            Assert.Equal("Produto precisa estar vinculada a uma categoria", ex.Message);
        }

        [Fact]
        public void CasoNaoExistaUmAdicionalCadastradoNoSistemaDeveSerLancadaUmaException()
        {
            var produto = HelperMock.MockProduto("XS-001", "X-Salada", idProduto: 0, idCategoria: 1, new List<Adicional> { new Adicional { IdentificadorUnico = 99, Tipo = "Queijo", Valor = 2.00m } });

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroProduto.Inserir(produto));
            Assert.Equal("Um dos adicionais especificados não esta cadastrado no sistema", ex.Message);
        }
    }
}
