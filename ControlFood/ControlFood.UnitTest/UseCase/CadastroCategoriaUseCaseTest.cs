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
    public class CadastroCategoriaUseCaseTest
    {
        private readonly Mock<ICategoriaRepository> _mockCategoriaRepository;        
        private readonly Mock<ISubCategoriaRepository> _mockSubCategoriaRepository;
        private readonly ICadastroCategoriaUseCase _cadastroCategoria;
        private List<Categoria> categoriasPersistidasDepois;

        public CadastroCategoriaUseCaseTest()
        {
            _mockCategoriaRepository = new Mock<ICategoriaRepository>();
            _mockSubCategoriaRepository = new Mock<ISubCategoriaRepository>();
            _cadastroCategoria = new CadastroCategoriaUseCase(_mockCategoriaRepository.Object, _mockSubCategoriaRepository.Object);

            _mockCategoriaRepository
               .Setup(x => x.BuscarTodos())
               .Returns(HelperMock.MockListaCategoriasPersistidas());

            _mockSubCategoriaRepository
                .Setup(x => x.BuscarTodos())
                .Returns(HelperMock.MockListaSubCategoriasPersistidas());
        }

        [Fact]
        public void DeveInserirUmaCategoriaNoSistemaComSucesso()
        {
            var categoria = new Categoria { Tipo = "Fritas" };

            _mockCategoriaRepository
                .Setup(x => x.Inserir(It.IsAny<Categoria>()))
                .Returns(() =>
                {
                    categoria.IdentificadorUnico = 1;
                    return categoria;
                });

            _cadastroCategoria.Inserir(categoria);

            Assert.Equal(1, categoria.IdentificadorUnico);
        }

        [Fact]
        public void DeveLancarUmaExceptionCasoACategoriaSejaDuplicada()
        {
            var categoriaRequest = new Categoria { Tipo = "Alimento", IdentificadorUnico = 0 };

            var ex = Assert.Throws<CategoriaIncorretaUseCaseException>(() => _cadastroCategoria.Inserir(categoriaRequest));
            Assert.Equal("A categoria Alimento ja existe no sistema", ex.Message);

        }

        [Fact]
        public void DeveBuscarTodasAsCategoriasCadastradas()
        {
            var retorno = _cadastroCategoria.BuscarTodos();

            Assert.NotNull(retorno);
            Assert.True(retorno.Count > 0);
        }

        [Fact]
        public void DeveDeletarUmaCategoriaExistente()
        {
            var categoria = new Categoria { Tipo = "CategoriaTeste", IdentificadorUnico = 4 };
            var categoriasPersistidasAntes = HelperMock.MockListaCategoriasPersistidas().Count;
            
            _mockCategoriaRepository
                .Setup(x => x.Deletar(It.IsAny<Categoria>()))
                .Callback(() => categoriasPersistidasDepois = DeletarCategoriaExistente(categoria));

            _cadastroCategoria.Deletar(categoria);

            Assert.True(categoriasPersistidasAntes > categoriasPersistidasDepois.Count);
            
        }

        [Fact]
        public void DeveLancarExceptionAoTentarDeletarUmaCategoriaQueTenhaUmaSubCategoriaVinculadaAMesma()
        {
            var categoria = new Categoria { Tipo = "Alimento", IdentificadorUnico = 1 };

            var ex = Assert.Throws<CategoriaIncorretaUseCaseException>(() => _cadastroCategoria.Deletar(categoria));
            Assert.Equal("Existe Sub-categoria vinculada a Categoria Alimento.", ex.Message);
        }

        #region [ METODOS PRIVADOS ]
        
        private List<Categoria> DeletarCategoriaExistente(Categoria categoria)
        {
            var categorias = HelperMock.MockListaCategoriasPersistidas();

            var existeCategoria = categorias.Any(c => c.IdentificadorUnico == categoria.IdentificadorUnico);
            var indice = categorias.FindIndex(c => c.IdentificadorUnico == categoria.IdentificadorUnico);

            if (existeCategoria)
                categorias.RemoveAt(indice);
            
            return categorias;
        }

        #endregion
    }
}
