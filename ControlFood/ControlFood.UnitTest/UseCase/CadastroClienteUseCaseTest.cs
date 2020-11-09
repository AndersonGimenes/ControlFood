using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Moq;
using System;
using System.Collections.Generic;
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
        }

        [Fact]
        public void DeveInserirUmClienteNoSistemaComSucesso()
        {
            var cliente = MockNovoCliente();

            _mockGeneciRepository
                .Setup(x => x.Inserir(It.IsAny<Cliente>()))
                .Returns(AdicionarIdentificador(cliente));

            _cadastroCliente.Inserir(cliente);

            Assert.Equal(1, cliente.IdentificadorUnico);
        }

        [Fact(Skip = "Ajustar quando implementar")]
        public void DeveAtualizarOsDadosDoClienteNoSistemaComSucesso()
        {
            var clienteRequest = MockNovoCliente();
            clienteRequest.TelefoneCelular = "19111111111";
            clienteRequest.IdentificadorUnico = 2;
            Cliente clienteBase = default;

            _mockGeneciRepository
                .Setup(x => x.Atualizar(It.IsAny<Cliente>(), It.IsAny<List<string>>()))
                .Callback(() => {
                        clienteBase = MockClienteAtualizar(clienteRequest);
                    });

            _cadastroCliente.Atualizar(clienteRequest, null);

            Assert.Equal("19111111111", clienteBase.TelefoneCelular);
        }

        [Fact]
        public void DeveBuscarOsDadosDoClienteNoSistemaComSucesso()
        {
            Cliente clienteRequest = MockClientePersistido();

            _mockGeneciRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns(MockClienteBuscar(clienteRequest.IdentificadorUnico));

            var retorno = _cadastroCliente.BuscarPorIdentificacao(clienteRequest, nameof(clienteRequest.IdentificadorUnico));

            Assert.True(retorno != null);
            Assert.Equal(clienteRequest.IdentificadorUnico, retorno.IdentificadorUnico);
        }


        private Cliente AdicionarIdentificador(Cliente cliente)
        {
            cliente.IdentificadorUnico = 1;
            return cliente;
        }

        private Cliente MockClienteAtualizar(Cliente clienteRequest)
        {
            var clienteBase = MockClientePersistido();

            if (clienteRequest.IdentificadorUnico == clienteBase.IdentificadorUnico)
            {
                clienteBase.TelefoneCelular = clienteRequest.TelefoneCelular;
            }

            return clienteBase;
        }

        private Cliente MockClienteBuscar(int id)
        {
            var cliente = MockClientePersistido();

            if (id == cliente.IdentificadorUnico)
                return cliente;
            
            return default;
        }

        private Cliente MockNovoCliente()
        {
            var cliente = new Cliente
            {
                Nome = "Jose do teste",
                Cpf = "12345678909",
                DataNascimento = new DateTime(1983, 06, 14),
                Email = "nd@nd.com",
                TelefoneCelular = "19989898989",
                TelefoneResidencial = "1932323232"
            };

            cliente.Endereco.Logradouro = "Rua hum";
            cliente.Endereco.Numero = 2;
            cliente.Endereco.Bairro = "Maria bonita";
            cliente.Endereco.Cep = "13010020";
            cliente.Endereco.Cidade = "São José";
            cliente.Endereco.Estado = "SP";
            
            return cliente;
        }

        private Cliente MockClientePersistido()
        {
            var cliente = MockNovoCliente();
            cliente.IdentificadorUnico = 2;

            return cliente;
        }
    }
}
