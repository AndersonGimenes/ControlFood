using ControlFood.Domain.Entidades;
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
    public class CadastroClienteUseCaseTest
    {
        private readonly Mock<IGenericRepository<Cliente>> _mockGeneciRepository;
        private readonly ICadastroClienteUseCase _cadastroCliente;

        public CadastroClienteUseCaseTest()
        {
            _mockGeneciRepository = new Mock<IGenericRepository<Cliente>>();
            _cadastroCliente = new CadastroClienteUseCase(_mockGeneciRepository.Object);

            _mockGeneciRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaCliente());
        }

        [Fact]
        public void DeveInserirUmClienteNoSistemaComSucesso()
        {
            var cliente = HelperMock.MockCliente("12345678910");

            //Substituir por ClienteRepository
            _mockGeneciRepository
                .Setup(x => x.Inserir(It.IsAny<Cliente>()))
                .Returns(() =>
                {
                    cliente.IdentificadorUnico = 1;
                    return cliente;
                });

            _cadastroCliente.Inserir(cliente);

            Assert.Equal(1, cliente.IdentificadorUnico);
            Assert.True(cliente.DataCadastro > DateTime.MinValue && cliente.DataCadastro < DateTime.Now);
        }

        [Fact]
        public void CasoCpfJaTenhaSidoCadastradoDeveSerLancadaExcepetion()
        {
            var cliente = HelperMock.MockCliente("12345678909");

            var ex = Assert.Throws<PessoaIncorretaUseCaseException>(() => _cadastroCliente.Inserir(cliente));
            Assert.Equal("O CPF 12345678909 ja existe no sistema", ex.Message);
        }

        [Fact(Skip = "Ajustar quando implementar")]
        public void DeveAtualizarOsDadosDoClienteNoSistemaComSucesso()
        {
            var clienteRequest = HelperMock.MockCliente("12345678909", 2);
            clienteRequest.TelefoneCelular = "19111111111";
            Cliente clienteBase = default;

            _mockGeneciRepository
                .Setup(x => x.Atualizar(It.IsAny<Cliente>(), It.IsAny<List<string>>()))
                .Callback(() =>
                {
                    // Ajustar quando implementar o fluxo de atualizar
                    clienteBase = new Cliente { TelefoneCelular = clienteRequest.TelefoneCelular };
                });

            _cadastroCliente.Atualizar(clienteRequest, null);

            Assert.Equal("19111111111", clienteBase.TelefoneCelular);
        }

        [Fact]
        public void DeveBuscarOsDadosDoClienteNoSistemaComSucesso()
        {
            Cliente clienteRequest = HelperMock.MockCliente("12345678909", 1);

            _mockGeneciRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns(HelperMock.MockListaCliente().First(c => c.IdentificadorUnico == clienteRequest.IdentificadorUnico));

            var retorno = _cadastroCliente.BuscarPorIdentificacao(clienteRequest, nameof(clienteRequest.IdentificadorUnico));

            Assert.True(retorno != null);
            Assert.Equal(clienteRequest.IdentificadorUnico, retorno.IdentificadorUnico);
        }
    }
}
