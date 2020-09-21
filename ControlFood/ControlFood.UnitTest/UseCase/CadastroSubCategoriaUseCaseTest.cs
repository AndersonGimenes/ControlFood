using ControlFood.Domain.Entidades;
using ControlFood.UnitTest.UseCase.Helpers;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ControlFood.UnitTest.UseCase
{
    public class CadastroSubCategoriaUseCaseTest
    {
        private readonly Mock<ISubCategoriaRepository> _mockSubCategoriaRepository;
        private readonly Mock<ICategoriaRepository> _mockCategoriaRepository;
        private readonly ICadastroSubCategoriaUseCase _CadastroSubCategoriaUseCase;
        private List<SubCategoria> subCategoriasPersistidasDepois;

        public CadastroSubCategoriaUseCaseTest()
        {
            _mockSubCategoriaRepository = new Mock<ISubCategoriaRepository>();
            _mockCategoriaRepository = new Mock<ICategoriaRepository>();
            _CadastroSubCategoriaUseCase = new CadastroSubCategoriaUseCase(_mockSubCategoriaRepository.Object, _mockCategoriaRepository.Object);

            _mockSubCategoriaRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaSubCategoriasPersistidas());

            _mockCategoriaRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaCategoriasPersistidas());
        }

        [Fact]
        public void DeveInserirUmaSubCategoriaComSucessoQuandoHouverUmaCategoriaVinculada()
        {
            var subCategoria = HelperMock.MockSubCategoria("Porção", 1);

            _mockSubCategoriaRepository
                .Setup(x => x.Inserir(It.IsAny<SubCategoria>()))
                .Returns(() => {
                    subCategoria.IdentificadorUnico = 1;
                    return subCategoria;
                });
            
            _CadastroSubCategoriaUseCase.Inserir(subCategoria);

            Assert.Equal(1, subCategoria.IdentificadorUnico);
        }

        [Fact]
        public void DeveLancarUmaExceptionCasoASubCategoriaSejaDuplicada()
        {
            var subCategoria = HelperMock.MockSubCategoria("Lanche", 1);
            
            var ex = Assert.Throws<SubCategoriaIncorretaUseCaseException>(() =>_CadastroSubCategoriaUseCase.Inserir(subCategoria));
            Assert.Equal("A sub-categoria Lanche ja existe no sistema", ex.Message);
        }

        [Fact]
        public void DeveLancarUmaExceptionCasoASubCategoriaNaoEstejaVinculadaAUmaCategoria()
        {
            var subCategoria = HelperMock.MockSubCategoria("Fritas", 5);
            
            var ex = Assert.Throws<SubCategoriaIncorretaUseCaseException>(() => _CadastroSubCategoriaUseCase.Inserir(subCategoria));
            Assert.Equal("Sub-categoria precisa estar vinculada a uma categoria", ex.Message);
        }

        [Fact]
        public void DeveBuscarTodasAsSubCategoriasPersistidas()
        {
            var categorias = _CadastroSubCategoriaUseCase.BuscarTodos();

            Assert.NotNull(categorias);
            Assert.True(categorias.Count > 0);
        }

        [Fact]
        public void DeveDeletarUmaSubCategoriaExistente()
        {
            var subCategoria = HelperMock.MockSubCategoria("Lanche", 1);
            var subCategoriasPersistidasAntes = HelperMock.MockListaSubCategoriasPersistidas().Count;

            _mockSubCategoriaRepository
                .Setup(x => x.Deletar(It.IsAny<SubCategoria>()))
                .Callback(() => subCategoriasPersistidasDepois = DeletarSubCategoriaExistente(subCategoria));

            _CadastroSubCategoriaUseCase.Deletar(subCategoria);

            Assert.True(subCategoriasPersistidasAntes > subCategoriasPersistidasDepois.Count);

        }

        #region [ METODOS PRIVADOS ]
        private List<SubCategoria> DeletarSubCategoriaExistente(SubCategoria subCategoria)
        {
            var subCategorias = HelperMock.MockListaSubCategoriasPersistidas();

            var existeSubCategoria = subCategorias.Any(s => s.IdentificadorUnico == subCategoria.IdentificadorUnico);
            var indice = subCategorias.FindIndex(s => s.IdentificadorUnico == subCategoria.IdentificadorUnico);

            if (existeSubCategoria)
            {
                subCategorias.RemoveAt(indice);
            }

            return subCategorias;
        }
       
        #endregion
    }
}
