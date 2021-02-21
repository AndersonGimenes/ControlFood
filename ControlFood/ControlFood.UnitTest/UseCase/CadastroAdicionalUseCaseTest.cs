using ControlFood.Domain.Entidades;
using ControlFood.UnitTest.UseCase.Helpers;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using Moq;
using System;
using Xunit;

namespace ControlFood.UnitTest.UseCase
{
    public class CadastroAdicionalUseCaseTest
    {
        private readonly CadastroAdicionalUseCase _cadastroAdicionalUseCase;
        private readonly Mock<IAdicionalRepository> _mockAdicionalRepository;

        public CadastroAdicionalUseCaseTest()
        {
            _mockAdicionalRepository = new Mock<IAdicionalRepository>();
            _cadastroAdicionalUseCase = new CadastroAdicionalUseCase(_mockAdicionalRepository.Object);

            _mockAdicionalRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.ListaMockAdicionaisPersistidos());
        }

        [Fact]
        public void DeveInserirUmNovoAdicionalComSucesso()
        {
            var adicional = new Adicional { Tipo = "Cheddar", Valor = 2.00m };

            _mockAdicionalRepository
                .Setup(x => x.Inserir(It.IsAny<Adicional>()))
                .Returns(() =>
                {
                    adicional.IdentificadorUnico = 1;
                    return adicional;
                });

            _cadastroAdicionalUseCase.Inserir(adicional);

            Assert.Equal(1, adicional.IdentificadorUnico);
            Assert.True(adicional.DataCadastro > DateTime.MinValue && adicional.DataCadastro < DateTime.Now);
        }

        [Fact]
        public void DeveLancarUmaExceptionCasoOAdicionalSejaDuplicada()
        {
            var adicional = new Adicional { Tipo = "Bacon", Valor = 2.00m , IdentificadorUnico = 0};

            var ex = Assert.Throws<AdicionalIncorretoUseCaseException>(() => _cadastroAdicionalUseCase.Inserir(adicional));
            Assert.Equal("O adicional Bacon ja existe no sistema", ex.Message);
        }

    }
}
