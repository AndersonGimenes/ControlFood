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
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private readonly ICadastroClienteUseCase _cadastroCliente;

        public CadastroClienteUseCaseTest()
        {
            _mockClienteRepository = new Mock<IClienteRepository>();
            _cadastroCliente = new CadastroClienteUseCase(_mockClienteRepository.Object);

            _mockClienteRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaCliente());
        }

        [Fact]
        public void DeveInserirUmClienteNoSistemaComSucesso()
        {
            var cliente = HelperMock.MockCliente("12345678910");
            cliente.Nome = "Jose Aldo";            
            cliente.Enderecos = new List<Endereco>();

            //Substituir por ClienteRepository
            _mockClienteRepository
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

        [Theory]
        [InlineData("12345678909", "O CPF 12345678909 ja existe no sistema")]
        [InlineData("", "O Nome Jose do teste ja existe no sistema")]
        public void CasoAlgumCampoNaoPassePelaValidacaoDeveLancarUmaException(string cpf, string resultado)
        {
            var cliente = HelperMock.MockCliente(cpf);
            
            var ex = Assert.Throws<PessoaIncorretaUseCaseException>(() => _cadastroCliente.Inserir(cliente));
            Assert.Equal(resultado, ex.Message);
        }

        [Fact]
        public void CasoDataDeNascimentoForMenorQueDezAnosDeveLancarUmaException()
        {
            var cliente = HelperMock.MockCliente("12345678910");
            cliente.Nome = "Roberto Carlos";
            cliente.DataNascimento = DateTime.Today.AddYears(-9);

            var ex = Assert.Throws<PessoaIncorretaUseCaseException>(() => _cadastroCliente.Inserir(cliente));
            Assert.Equal("O Data de nascimento esta invalida. Cliente deve ter ao menos 10 anos", ex.Message);
        }

        [Fact(Skip = "Ajustar quando implementar")]
        public void DeveAtualizarOsDadosDoClienteNoSistemaComSucesso()
        {
            var clienteRequest = HelperMock.MockCliente("12345678909", 2);
            clienteRequest.TelefoneCelular = "19111111111";
            Cliente clienteBase = default;

            _mockClienteRepository
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
            var clienteRequest = HelperMock.MockCliente(identificadorUnico: 1);

            _mockClienteRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns(HelperMock.MockListaCliente().First(c => c.IdentificadorUnico == clienteRequest.IdentificadorUnico));

            var retorno = _cadastroCliente.BuscarPorIdentificacao(clienteRequest, nameof(clienteRequest.IdentificadorUnico));

            Assert.True(retorno != null);
            Assert.Equal(clienteRequest.IdentificadorUnico, retorno.IdentificadorUnico);
        }

        [Fact]
        public void DeveBuscarTodosOsEnderecosCadastradosParaOCliente()
        {
            var clienteRequest = HelperMock.MockCliente(identificadorUnico: 1);

            _mockClienteRepository
               .Setup(x => x.BuscarPorId(It.IsAny<int>()))
               .Returns(HelperMock.MockListaCliente().First(c => c.IdentificadorUnico == clienteRequest.IdentificadorUnico));

            var retorno = _cadastroCliente.BuscarPorIdentificacao(clienteRequest, nameof(clienteRequest.IdentificadorUnico));

            Assert.NotNull(retorno);
            Assert.Equal(clienteRequest.IdentificadorUnico, retorno.IdentificadorUnico);
            Assert.True(retorno.Enderecos.Count > 0);
        }
    }
}
