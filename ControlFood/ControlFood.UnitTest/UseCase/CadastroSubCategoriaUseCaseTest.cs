using ControlFood.Domain.Entidades;
using ControlFood.UnitTest.Helpers;
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
        private readonly Mock<IProdutoRepository> _mockProdutoRepository;
        private readonly ICadastroSubCategoriaUseCase _cadastroSubCategoriaUseCase;
        private int subCategoriasPersistidasDepois;
        
        public CadastroSubCategoriaUseCaseTest()
        {
            _mockSubCategoriaRepository = new Mock<ISubCategoriaRepository>();
            _mockCategoriaRepository = new Mock<ICategoriaRepository>();
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            _cadastroSubCategoriaUseCase = new CadastroSubCategoriaUseCase(_mockSubCategoriaRepository.Object, _mockCategoriaRepository.Object, _mockProdutoRepository.Object);

            _mockSubCategoriaRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaSubCategoriasPersistidas());

            _mockCategoriaRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaCategoriasPersistidas());

            _mockProdutoRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaProdutosPersistidos());
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
            
            _cadastroSubCategoriaUseCase.Inserir(subCategoria);

            Assert.Equal(1, subCategoria.IdentificadorUnico);
        }

        [Fact]
        public void DeveLancarUmaExceptionCasoASubCategoriaSejaDuplicada()
        {
            var subCategoria = HelperMock.MockSubCategoria("Lanche", 1);
            
            var ex = Assert.Throws<SubCategoriaIncorretaUseCaseException>(() =>_cadastroSubCategoriaUseCase.Inserir(subCategoria));
            Assert.Equal("A sub-categoria Lanche ja existe no sistema", ex.Message);
        }

        [Fact]
        public void DeveLancarUmaExceptionCasoASubCategoriaNaoEstejaVinculadaAUmaCategoria()
        {
            var subCategoria = HelperMock.MockSubCategoria("Fritas", 5);
            
            var ex = Assert.Throws<SubCategoriaIncorretaUseCaseException>(() => _cadastroSubCategoriaUseCase.Inserir(subCategoria));
            Assert.Equal("Sub-categoria precisa estar vinculada a uma categoria", ex.Message);
        }

        [Fact]
        public void DeveBuscarTodasAsSubCategoriasPersistidas()
        {
            var categorias = _cadastroSubCategoriaUseCase.BuscarTodos();

            Assert.NotNull(categorias);
            Assert.True(categorias.Count > 0);
        }

        [Fact]
        public void DeveDeletarUmaSubCategoriaExistente()
        {
            var subCategoria = HelperMock.MockSubCategoria("Espetos", 1, idSubCategoria: 5);
            var subCategorias = HelperMock.MockListaSubCategoriasPersistidas();
            var subCategoriasPersistidasAntes = subCategorias.Count; 

            _mockSubCategoriaRepository
                .Setup(x => x.Deletar(It.IsAny<SubCategoria>()))
                .Callback(() => subCategoriasPersistidasDepois = HelperComum<SubCategoria>.DeletarRegistro(subCategoria, subCategorias, nameof(subCategoria.IdentificadorUnico)));

            _cadastroSubCategoriaUseCase.Deletar(subCategoria);

            Assert.True(subCategoriasPersistidasAntes > subCategoriasPersistidasDepois);
        }

        [Fact]
        public void DeveAtualizarUmaSubCategoriaExistenteComSucesso()
        {
            var subCategoria = HelperMock.MockSubCategoria("Suco", 2, tipoCategoria: "Bebida", idSubCategoria: 3);
            subCategoria.IndicadorItemBar = true;
            subCategoria.IndicadorItemCozinha = false;

            var subCategoriaPersistida = HelperMock.MockListaSubCategoriasPersistidas().First(x => x.IdentificadorUnico == subCategoria.IdentificadorUnico);

            _mockSubCategoriaRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns(subCategoriaPersistida);

            _mockSubCategoriaRepository
                .Setup(x => x.Atualizar(It.IsAny<SubCategoria>()))
                .Callback(() =>
                {
                    subCategoriaPersistida.IndicadorItemBar = subCategoria.IndicadorItemBar;
                    subCategoriaPersistida.IndicadorItemCozinha = subCategoria.IndicadorItemCozinha;
                });            

            _cadastroSubCategoriaUseCase.Atualizar(subCategoria);

            Assert.True(subCategoriaPersistida.IndicadorItemBar);
            Assert.False(subCategoriaPersistida.IndicadorItemCozinha);

        }

        [Fact]
        public void DadoIdentificadorUnicoDaCategoriaDoObjetoDeRequesicaoDiferenteAoIdentificadoUnicoDaCategoriaPersistidaDeveSerLancadaUmaException()
        {
            var subCategoria = HelperMock.MockSubCategoria("Suco", 5, tipoCategoria: "Bebida", idSubCategoria: 3);
            var subCategoriaPersistida = HelperMock.MockListaSubCategoriasPersistidas().First(x => x.IdentificadorUnico == subCategoria.IdentificadorUnico);

            _mockSubCategoriaRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns(subCategoriaPersistida);

            var ex = Assert.Throws<SubCategoriaIncorretaUseCaseException>(() => _cadastroSubCategoriaUseCase.Atualizar(subCategoria));
            Assert.Equal("O campo IdentificadorUnico não pode ser atualizado.", ex.Message);            
        }

        [Fact]
        public void DadoTipoDaSubCategoriaDoObjetoDeRequesicaoDiferenteAoTipoDaSubCategoriaPersistidaDeveSerLancadaUmaException()
        {
            var subCategoria = HelperMock.MockSubCategoria("Suquinho", 2, tipoCategoria: "Bebida", idSubCategoria: 3);
            var subCategoriaPersistida = HelperMock.MockListaSubCategoriasPersistidas().First(x => x.IdentificadorUnico == subCategoria.IdentificadorUnico);

            _mockSubCategoriaRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns(subCategoriaPersistida);

            var ex = Assert.Throws<SubCategoriaIncorretaUseCaseException>(() => _cadastroSubCategoriaUseCase.Atualizar(subCategoria));
            Assert.Equal("O campo Tipo não pode ser atualizado.", ex.Message);
        }

        [Fact]
        public void DeveLancarUmaExcepetionAoTentarDeletarUmaSubCategoriaVinculadaAUmProduto()
        {
            var subCategoria = HelperMock.MockSubCategoria("Lanche", 1, idSubCategoria: 1);

            var ex = Assert.Throws<SubCategoriaIncorretaUseCaseException>(() => _cadastroSubCategoriaUseCase.Deletar(subCategoria));

            Assert.Equal("Existe Produto vinculada a Sub-Categoria Lanche.", ex.Message);
        }

    }
}
