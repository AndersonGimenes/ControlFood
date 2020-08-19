using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Moq;

namespace ControlFood.UnitTest.UseCase
{
    public class CadastroProdutoUseCaseTest
    {
        private readonly Mock<IGenericRepository<Produto>> _mockGeneciRepository;
        private readonly ICadastroProdutoUseCase _cadastroProduto;

        public CadastroProdutoUseCaseTest()
        {
            _mockGeneciRepository = new Mock<IGenericRepository<Produto>>();
            _cadastroProduto = new CadastroProdutoUseCase(_mockGeneciRepository.Object);
        }

    }
}
