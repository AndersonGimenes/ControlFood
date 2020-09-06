﻿using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Moq;
using System;
using Xunit;

namespace ControlFood.UnitTest.UseCase
{
    public class CadastroFuncionarioUseCaseTest
    {
        private readonly Mock<IGenericRepository<Funcionario>> _mockGeneciRepository;
        private readonly ICadastroFuncionarioUseCase _cadastroFuncionario;

        public CadastroFuncionarioUseCaseTest()
        {
            _mockGeneciRepository = new Mock<IGenericRepository<Funcionario>>();
            _cadastroFuncionario = new CadastroFuncionarioUseCase(_mockGeneciRepository.Object);
        }

        [Fact]
        public void DeveInserirUmClienteNoSistemaComSucesso()
        {
            var funcionario = MockNovoFuncionario();

            _mockGeneciRepository
                .Setup(x => x.Inserir(It.IsAny<Funcionario>()))
                .Returns(AdicionarIdentificador(funcionario));

            _cadastroFuncionario.Inserir(funcionario);

            Assert.Equal(1, funcionario.IdentificadorUnico);
        }

        [Fact]
        public void DeveAtualizarOsDadosDoClienteNoSistemaComSucesso()
        {
            var funcionarioRequest = MockNovoFuncionario();
            funcionarioRequest.TelefoneCelular = "19111111111";
            funcionarioRequest.IdentificadorUnico = 2;
            Funcionario funcionarioBase = default;

            _mockGeneciRepository
                .Setup(x => x.Atualizar(It.IsAny<Funcionario>()))
                .Callback(() =>
                {
                    funcionarioBase = MockClienteAtualizar(funcionarioRequest);
                });

            _cadastroFuncionario.Atualizar(funcionarioRequest);

            Assert.Equal("19111111111", funcionarioBase.TelefoneCelular);
        }

        [Fact]
        public void DeveBuscarOsDadosDoClienteNoSistemaComSucesso()
        {
            Funcionario funcionarioRequest = MockFuncionarioPersistido();

            _mockGeneciRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns(MockFuncionarioBuscar(funcionarioRequest.IdentificadorUnico));

            var retorno = _cadastroFuncionario.BuscarPorIdentificacao(funcionarioRequest, nameof(funcionarioRequest.IdentificadorUnico));

            Assert.True(retorno != null);
            Assert.Equal(funcionarioRequest.IdentificadorUnico, retorno.IdentificadorUnico);
        }


        private Funcionario AdicionarIdentificador(Funcionario funcionario)
        {
            funcionario.IdentificadorUnico = 1;
            return funcionario;
        }

        private Funcionario MockClienteAtualizar(Funcionario funcionarioRequest)
        {
            var funcionarioBase = MockFuncionarioPersistido();

            if (funcionarioRequest.IdentificadorUnico == funcionarioBase.IdentificadorUnico)
            {
                funcionarioBase.TelefoneCelular = funcionarioRequest.TelefoneCelular;
            }

            return funcionarioBase;
        }

        private Funcionario MockFuncionarioBuscar(int id)
        {
            var funcionario = MockFuncionarioPersistido();

            if (id == funcionario.IdentificadorUnico)
                return funcionario;

            return default;
        }

        private Funcionario MockNovoFuncionario()
        {
            var funcionario = new Funcionario
            {
                Nome = "Jose do teste",
                Cpf = "12345678909",
                DataNascimento = new DateTime(1983, 06, 14),
                Email = "nd@nd.com",
                TelefoneCelular = "19989898989",
                TelefoneResidencial = "1932323232",
                Cargo = "Garçon"
            };

            funcionario.Endereco.Logradouro = "Rua hum";
            funcionario.Endereco.Numero = 2;
            funcionario.Endereco.Bairro = "Maria bonita";
            funcionario.Endereco.Cep = "13010020";
            funcionario.Endereco.Cidade = "São José";
            funcionario.Endereco.Estado = "SP";

            return funcionario;
        }

        private Funcionario MockFuncionarioPersistido()
        {
            var funcionario = MockNovoFuncionario();
            funcionario.IdentificadorUnico = 2;

            return funcionario;
        }
    }
}
