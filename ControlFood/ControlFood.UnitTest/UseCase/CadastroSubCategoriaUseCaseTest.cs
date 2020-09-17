using ControlFood.Domain.Entidades;
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
        private readonly ICadastroSubCategoriaUseCase _CadastroSubCategoriaUseCase;
        private List<SubCategoria> subCategoriasPersistidasDepois;

        public CadastroSubCategoriaUseCaseTest()
        {
            _mockSubCategoriaRepository = new Mock<ISubCategoriaRepository>();
            _CadastroSubCategoriaUseCase = new CadastroSubCategoriaUseCase(_mockSubCategoriaRepository.Object);

            _mockSubCategoriaRepository
                .Setup(x => x.BuscarTodos())
                .Returns(MockSubCategoriasPersistidas());
        }

        [Fact]
        public void DeveInserirUmaSubCategoriaComSucessoQuandoHouverUmaCategoriaVinculada()
        {
            // passar o objeto subcategoria vinculada a uma categoria
            var subCategoria = MockSubCategoria("Porção", 1);

            _mockSubCategoriaRepository
                .Setup(x => x.Inserir(It.IsAny<SubCategoria>()))
                .Returns(AtualizarSubCategoria(subCategoria));
            
            _CadastroSubCategoriaUseCase.Inserir(subCategoria);

            Assert.Equal(1, subCategoria.IdentificadorUnico);
        }

        [Fact]
        public void DeveLancarUmaExceptionCasoASubCategoriaSejaDuplicada()
        {
            var subCategoria = MockSubCategoria("Lanche", 1);
            var subCategoriasPersistidas = MockSubCategoriasPersistidas();

            var ex = Assert.Throws<SubCategoriaIncorretaUseCaseException>(() =>_CadastroSubCategoriaUseCase.Inserir(subCategoria));
            Assert.Equal("A sub-categoria Lanche ja existe no sistema", ex.Message);
        }

        [Fact]
        public void DeveLancarUmaExceptionCasoASubCategoriaNaoEstejaVinculadaAUmaCategoria()
        {
            var subCategoria = MockSubCategoria("Fritas", 2);
            var categoriasPersistidas = MockCategoriasPersistidas();

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
            var subCategoria = MockSubCategoria("Lanche", 1);
            var subCategoriasPersistidasAntes = MockSubCategoriasPersistidas();

            _mockSubCategoriaRepository
                .Setup(x => x.Deletar(It.IsAny<SubCategoria>()))
                .Callback(() => subCategoriasPersistidasDepois = DeletarSubCategoriaExistente(subCategoria));

            _CadastroSubCategoriaUseCase.Deletar(subCategoria);

            Assert.True(subCategoriasPersistidasAntes.Count > subCategoriasPersistidasDepois.Count);

        }

        #region [ METODOS PRIVADOS ]
        private List<SubCategoria> DeletarSubCategoriaExistente(SubCategoria subCategoria)
        {
            var subCategorias = MockSubCategoriasPersistidas();

            var existeSubCategoria = subCategorias.Any(s => s.IdentificadorUnico == subCategoria.IdentificadorUnico);
            var indice = subCategorias.FindIndex(s => s.IdentificadorUnico == subCategoria.IdentificadorUnico);

            if (existeSubCategoria)
            {
                subCategorias.RemoveAt(indice);
            }

            return subCategorias;
        }

        private SubCategoria AtualizarSubCategoria(SubCategoria subCategoria)
        {
            subCategoria.IdentificadorUnico = 1;
            return subCategoria;
        }

        private SubCategoria MockSubCategoria(string tipo, int idCategoria)
        {
            var subCategoria = new SubCategoria
            {
                Tipo = tipo,
                IndicadorItemCozinha = true
            };

            subCategoria.Categoria = new Categoria { Tipo = "Alimento", IdentificadorUnico = idCategoria };

            return subCategoria;
        }
        
        private List<SubCategoria> MockSubCategoriasPersistidas() => 
            new List<SubCategoria> {
                MockSubCategoria("Lanche", 1),
                MockSubCategoria("Pastel", 1)
            };

        private List<Categoria> MockCategoriasPersistidas() =>        
            new List<Categoria>
            {
                new Categoria { Tipo = "Alimento", IdentificadorUnico = 1},
                new Categoria { Tipo = "Bebida", IdentificadorUnico = 2}
            };
        



        #endregion
    }
}
