using ControlFood.Domain.Entidades;
using ControlFood.UnitTest.Helpers;
using ControlFood.UnitTest.UseCase.Helpers;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ControlFood.UnitTest.UseCase
{
    public class CadastroProdutoUseCaseTest
    {
        private readonly Mock<IProdutoRepository> _mockProdutoRepository;
        private readonly Mock<ICategoriaRepository> _mockCategoriaRepository;
        private readonly ICadastroProdutoUseCase _cadastroProduto;
        private int listaProdutoDepois;
        private List<Produto> produtosPersistidos;

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
            var produto = HelperMock.MockProduto("gra350", "Guarana antarctica lata 350ml", idProduto: 0, idCategoria: 4);

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
        [InlineData("O produto com nome Coca-cola lata 350ml ja existe no sistema", "cc350", "Coca-cola lata 350ml")]
        public void DeveLancarUmaExceptionCasoOProdutoSejaDuplicadoOuPorNomeOuPorCodigo(string result, string codigo, string nome)
        {
            var produto = HelperMock.MockProduto(codigo, nome, idProduto: 0, idCategoria: 4);

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroProduto.Inserir(produto));
            Assert.Equal(result, ex.Message);
        }

        [Fact]
        public void CasoNaoExistaUmaCategoriaVinculadaAoProdutoDeveSerLancadaUmaException()
        {
            var produto = HelperMock.MockProduto("xpto", "Xtapa", idProduto: 0, idCategoria: 99);

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroProduto.Inserir(produto));
            Assert.Equal("Produto precisa estar vinculada a uma categoria", ex.Message);
        }

        [Fact]
        public void SeHouverEstoqueVinculadoAoProdutoNaoDeveDeletarOProdutoEDeveLancarUmaException()
        {
            //var produtoRequest = HelperMock.MockProduto("cc350", "Coca-cola lata 350ml", idProduto: 1);

            //var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroProduto.Deletar(produtoRequest));
            //Assert.Equal("Existe estoque vinculado ao Produto Coca-cola lata 350ml", ex.Message);
        }

        [Fact]
        public void AoEnviarUmaSolicitacaoDeDelecaoValidaOProdutoDeveSerDeletadoComSucesso()
        {
            //var produtoRequest = HelperMock.MockProduto("SP001", "Sorverte de palito", idProduto: 5, idSubCategoria: 6);
            //var listaProdutos = HelperMock.MockListaProdutosPersistidos();
            //var listaProdutoAntes = listaProdutos.Count;

            //_mockProdutoRepository
            //    .Setup(x => x.Deletar(It.IsAny<Produto>()))
            //    .Callback(() => listaProdutoDepois = HelperComum<Produto>.DeletarRegistro(produtoRequest, listaProdutos, nameof(produtoRequest.IdentificadorUnico)));

            //_cadastroProduto.Deletar(produtoRequest);

            //Assert.True(listaProdutoAntes > listaProdutoDepois);
        }

        [Fact]
        public void DeveAtualizarApenasOValorDeVendaEDataAlteracaoDoProdutoComSucesso()
        {
            //var produtoRequest = HelperMock.MockProduto("cc350", "Coca-cola lata 350ml", idProduto: 1);
            //produtoRequest.ValorVenda = 10.00M;

            //_mockProdutoRepository
            //    .Setup(x => x.Atualizar(It.IsAny<Produto>(), It.IsAny<List<string>>()))
            //    .Returns(() =>
            //    {
            //        produtosPersistidos = HelperMock.MockListaProdutosPersistidos();

            //        return produtosPersistidos.First(p =>
            //        {
            //            var condicao = p.IdentificadorUnico == produtoRequest.IdentificadorUnico;

            //            if (condicao)
            //                p.ValorVenda = produtoRequest.ValorVenda;

            //            return condicao;
            //        });
            //    });

            //_cadastroProduto.AtualizarProduto(produtoRequest);

            //Assert.Equal(produtoRequest.ValorVenda, produtosPersistidos.First(p => p.IdentificadorUnico == produtoRequest.IdentificadorUnico).ValorVenda);
            //Assert.True(produtoRequest.DataAlteracao > DateTime.MinValue && produtoRequest.DataAlteracao < DateTime.Now);
        }

    }
}
