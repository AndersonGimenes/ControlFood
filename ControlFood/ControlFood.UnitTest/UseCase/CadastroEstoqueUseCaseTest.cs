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
    public class CadastroEstoqueUseCaseTest
    {
        private readonly Mock<IEstoqueRepository> _mockEstoqueRepository;
        private readonly Mock<IProdutoRepository> _mockProdutoRepository;
        private readonly ICadastroEstoqueUseCase _cadastroEstoqueUseCase;
        private int listaEstoqueDepois;

        public CadastroEstoqueUseCaseTest()
        {
            _mockEstoqueRepository = new Mock<IEstoqueRepository>();
            _mockProdutoRepository = new Mock<IProdutoRepository>();

            _cadastroEstoqueUseCase = new CadastroEstoqueUseCase(_mockEstoqueRepository.Object, _mockProdutoRepository.Object);

            _mockProdutoRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaProdutosPersistidos());

            _mockEstoqueRepository
                .Setup(x => x.BuscarTodosPorProduto(It.IsAny<Produto>()))
                .Returns(HelperMock.MockListaEstoque());
        }

        [Fact]
        public void DeveInserirOEstoqueDadoProdutoExistente()
        {
            var estoqueCocaCola = new Produto { IdentificadorUnico = 1 };
            estoqueCocaCola.Estoque = new Estoque { Quantidade = 10, DataValidade = new DateTime(2021, 06, 10), ValorCompraTotal = 50.90M, ValorCompraUnidade = 5.09M };

            _mockEstoqueRepository
                .Setup(x => x.Inserir(It.IsAny<Estoque>()))
                .Returns(() =>
                {
                    estoqueCocaCola.Estoque.IdentificadorUnico = 1;
                    return estoqueCocaCola.Estoque;
                });

            _cadastroEstoqueUseCase.InserirEstoque(estoqueCocaCola);

            Assert.Equal(1, estoqueCocaCola.Estoque.IdentificadorUnico);
            Assert.True(estoqueCocaCola.Estoque.DataCadastro > DateTime.MinValue && estoqueCocaCola.Estoque.DataCadastro < DateTime.Now);
        }

        [Fact]
        public void SeNaoHouverProdutoCadastradoDeveLancarUmaException()
        {
            var estoqueCocaCola = new Produto { IdentificadorUnico = 99 };
            estoqueCocaCola.Estoque = new Estoque { Quantidade = 10, DataValidade = new DateTime(2021, 06, 10), ValorCompraTotal = 50.90M, ValorCompraUnidade = 5.09M };

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroEstoqueUseCase.InserirEstoque(estoqueCocaCola));

            Assert.Equal("Não é possivel cadastrar o estoque: Produto inexistente", ex.Message);
        }

        [Fact]
        public void AoInserirUmaDataDeValidadeMenorQueODiaAtualDeveSerLancadaUmaException()
        {
            var mensagemErro = string.Format("A data de validade do produto deve ser maior que {0}", DateTime.Today.ToString("dd/MM/yyyy"));

            var sorvete = new Produto { IdentificadorUnico = 5 };
            sorvete.Estoque = new Estoque { Quantidade = 10, DataValidade = new DateTime(2020, 05, 15), ValorCompraTotal = 30.00M, ValorCompraUnidade = 3.00M };

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroEstoqueUseCase.InserirEstoque(sorvete));

            Assert.Equal(mensagemErro, ex.Message);
        }

        [Fact]
        public void SeAQauntidadeVezesValorUnitarioForDiferenteDoValorTotalDeveSerLancadaUmaException()
        {
            var sorvete = new Produto { IdentificadorUnico = 5 };
            sorvete.Estoque = new Estoque { Quantidade = 10, DataValidade = new DateTime(2021, 05, 15), ValorCompraTotal = 35.00M, ValorCompraUnidade = 3.00M };

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroEstoqueUseCase.InserirEstoque(sorvete));

            Assert.Equal("A quantidade X valor unitario é diferente do valor total do lote.", ex.Message);
        }

        [Fact]
        public void DeveDevolverListaDeEstoquePorProdutoComDataValidadeOk()
        {
            var produto = HelperMock.MockProduto("cc350", "Coca-cola lata 350ml", idProduto: 1);

            var estoquesXProduto = _cadastroEstoqueUseCase.BuscarDadosProdutoXEstoques(produto);

            Assert.DoesNotContain(estoquesXProduto, p => p.Estoque.DataValidade < DateTime.Today);
        }

        [Fact]
        public void DeveAtualizarOItemDoEstoqueComSucesso()
        {
            var itemAntesAtualizacao = HelperMock.MockListaEstoque().First(p => p.IdentificadorUnico == 5);
            var itemAtualizacao = new Estoque { IdentificadorUnico = 5, Quantidade = 20, DataValidade = DateTime.Now.AddDays(200), ValorCompraTotal = 60.00M, ValorCompraUnidade = 6.00M };
            var produto = new Produto { Estoque = itemAtualizacao };

            _mockEstoqueRepository
                .Setup(x => x.Atualizar(It.IsAny<Estoque>(), It.IsAny<List<string>>()))
                .Callback(() =>
                {
                    itemAntesAtualizacao = itemAtualizacao;
                });

            _cadastroEstoqueUseCase.AtualizarEstoque(produto);

            Assert.Equal(produto.Estoque, itemAntesAtualizacao);
            Assert.True(itemAtualizacao.DataAlteracao > DateTime.MinValue && itemAtualizacao.DataAlteracao < DateTime.Now);

        }

        [Fact]
        public void AoEnviarUmaSolicitacaoDeDelecaoOEstoqueDoProdutoDeveSerDeletadoComSucesso()
        {
            var produtoRequest = new Produto { Estoque = new Estoque { IdentificadorUnico = 5 } };
            var listaEstoque = HelperMock.MockListaEstoque();
            var listaEstoqueAntes = listaEstoque.Count;

            _mockEstoqueRepository
                .Setup(x => x.Deletar(It.IsAny<Estoque>()))
                .Callback(() => listaEstoqueDepois = HelperComum<Estoque>.DeletarRegistro(produtoRequest.Estoque, listaEstoque, nameof(produtoRequest.Estoque.IdentificadorUnico)));

            _cadastroEstoqueUseCase.Deletar(produtoRequest.Estoque);

            Assert.True(listaEstoqueAntes > listaEstoqueDepois);
        }
    }
}
