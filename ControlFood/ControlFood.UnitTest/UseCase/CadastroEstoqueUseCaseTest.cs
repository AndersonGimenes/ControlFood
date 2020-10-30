﻿using ControlFood.Domain.Entidades;
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
    public class CadastroEstoqueUseCaseTest
    {
        private readonly Mock<IEstoqueRepository> _mockEstoqueRepository;
        private readonly Mock<IProdutoRepository> _mockProdutoRepository;
        private readonly ICadastroEstoqueUseCase _cadastroEstoqueUseCase;

        public CadastroEstoqueUseCaseTest()
        {
            _mockEstoqueRepository = new Mock<IEstoqueRepository>();
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            
            _cadastroEstoqueUseCase = new CadastroEstoqueUseCase(_mockEstoqueRepository.Object, _mockProdutoRepository.Object);

            _mockProdutoRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaProdutosPersistidos());
        }

        [Fact]
        public void DeveInserirOEstoqueDadoProdutoExistente()
        {
            var dataInicio = DateTime.Now;

            var estoqueCocaCola = new Produto();
            estoqueCocaCola.Estoque = new Estoque { Quantidade = 10, DataValidade = new DateTime(2021, 06, 10), ValorCompraTotal = 50.90M, ValorCompraUnidade = 5.09M };
            estoqueCocaCola.Estoque.AtribuirIdentificadorUnicoProduto(1);

            _cadastroEstoqueUseCase.InserirEstoque(estoqueCocaCola);

            var dataFim = DateTime.Now;

            Assert.True(estoqueCocaCola.Estoque.DataEntrada > dataInicio && estoqueCocaCola.Estoque.DataEntrada < dataFim);
        }

        [Fact]
        public void SeNaoHouverProdutoCadastradoDeveLancarUmaException()
        {
            var estoqueCocaCola = new Produto();
            estoqueCocaCola.Estoque = new Estoque { Quantidade = 10, DataValidade = new DateTime(2021, 06, 10), ValorCompraTotal = 50.90M, ValorCompraUnidade = 5.09M };
            estoqueCocaCola.Estoque.AtribuirIdentificadorUnicoProduto(99);

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroEstoqueUseCase.InserirEstoque(estoqueCocaCola));
            
            Assert.Equal("Não é possivel cadastrar o estoque: Produto inexistente", ex.Message);
        }

        [Fact]
        public void AoInserirUmaDataDeValidadeMenorQueODiaAtualDeveSerLancadaUmaException()
        {
            var mensagemErro = string.Format("A data de validade do produto deve ser maior que {0}", DateTime.Today.ToString("dd/MM/yyyy"));

            var sorvete = new Produto();
            sorvete.Estoque = new Estoque { Quantidade = 10, DataValidade = new DateTime(2020, 05, 15), ValorCompraTotal = 30.00M, ValorCompraUnidade = 3.00M };
            sorvete.Estoque.AtribuirIdentificadorUnicoProduto(5);

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroEstoqueUseCase.InserirEstoque(sorvete));

            Assert.Equal(mensagemErro, ex.Message);
        }

        [Fact]
        public void SeAQauntidadeVezesValorUnitarioForDiferenteDoValorTotalDeveSerLancadaUmaException()
        {
            var sorvete = new Produto();
            sorvete.Estoque = new Estoque { Quantidade = 10, DataValidade = new DateTime(2021, 05, 15), ValorCompraTotal = 35.00M, ValorCompraUnidade = 3.00M };
            sorvete.Estoque.AtribuirIdentificadorUnicoProduto(5);

            var ex = Assert.Throws<ProdutoIncorretoUseCaseException>(() => _cadastroEstoqueUseCase.InserirEstoque(sorvete));

            Assert.Equal("A quantidade X valor unitario é diferente do valor total do lote.", ex.Message);
        }
    }
}
