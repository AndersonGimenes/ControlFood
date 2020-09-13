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
    public class CadastroCategoriaUseCaseTest
    {
        private readonly Mock<ICategoriaRepository> _mockCategoriaRepository;
        private readonly ICadastroCategoriaUseCase _cadastroCategoria;
        private List<Categoria> categoriasPersistidasDepois;

        public CadastroCategoriaUseCaseTest()
        {
            _mockCategoriaRepository = new Mock<ICategoriaRepository>();
            _cadastroCategoria = new CadastroCategoriaUseCase(_mockCategoriaRepository.Object);
        }

        [Fact]
        public void DeveInserirUmaCategoriaNoSistemaComSucesso()
        {
            var categoria = new Categoria { Tipo = "Alimento" };

            _mockCategoriaRepository
                .Setup(x => x.Inserir(It.IsAny<Categoria>()))
                .Returns(AdicionarIdentificador(categoria));

            _cadastroCategoria.Inserir(categoria);

            Assert.Equal(1, categoria.IdentificadorUnico);
        }

        [Fact]
        public void DeveAtualizarOsDadosDaCategoriaNoSistemaComSucesso()
        {
            var categoriaRequest = new Categoria { Tipo = "Suprimento", IdentificadorUnico = 1 };
            Categoria categoriaBase = default;

            _mockCategoriaRepository
                .Setup(x => x.Atualizar(It.IsAny<Categoria>()))
                .Callback(() => {
                    categoriaBase = MockClienteAtualizar(categoriaRequest);
                });

            _cadastroCategoria.Atualizar(categoriaRequest);

            Assert.Equal("Suprimento", categoriaBase.Tipo);
        }

        [Fact]
        public void DeveBuscarOsDadosDaCategoriaNoSistemaComSucesso()
        {
            Categoria categoriaRequest = new Categoria { IdentificadorUnico = 1 };

            _mockCategoriaRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns(MockCategoriaBuscar(categoriaRequest.IdentificadorUnico));

            var retorno = _cadastroCategoria.BuscarPorIdentificacao(categoriaRequest, nameof(categoriaRequest.IdentificadorUnico));

            Assert.True(retorno != null);
            Assert.Equal("Alimento", retorno.Tipo);
        }

        [Fact]
        public void DeveBuscarTodasAsCategoriasCadastradas()
        {
            _mockCategoriaRepository
                .Setup(x => x.BuscarTodos())
                .Returns(MockListaCategorias());

            var retorno = _cadastroCategoria.BuscarTodos();

            Assert.True(retorno != null);
            Assert.True(retorno.Count > 0);
        }

        [Fact]
        public void DeveDeletarUmaCategoriaExistente()
        {
            var categoria = new Categoria { Tipo = "Bebida", IdentificadorUnico = 2 };
            var categoriasPersistidasAntes = MockListaCategorias();
            
            _mockCategoriaRepository
                .Setup(x => x.Deletar(It.IsAny<Categoria>()))
                .Callback(() => categoriasPersistidasDepois = DeletarCategoriaExistente(categoria));

            _cadastroCategoria.Deletar(categoria);

            Assert.True(categoriasPersistidasAntes.Count > categoriasPersistidasDepois.Count);
            
        }

        [Fact]
        public void DeveLancarUmaExceptionCasoACategoriaSejaDuplicada()
        {
            var categoriaRequest = new Categoria { Tipo = "Alimento", IdentificadorUnico = 0 };

            var ex = Assert.Throws<CategoriaIncorretaUseCaseException>(() => _cadastroCategoria.VerificarDuplicidade(categoriaRequest, MockListaCategorias()));
            Assert.Equal("A categoria Alimento ja existe no sistema", ex.Message);

        }

        #region [ METODOS PRIVADOS ]
        private List<Categoria> DeletarCategoriaExistente(Categoria categoria)
        {
            var categorias = MockListaCategorias();

            var existeCategoria = categorias.Any(c => c.IdentificadorUnico == categoria.IdentificadorUnico);
            var indice = categorias.FindIndex(c => c.IdentificadorUnico == categoria.IdentificadorUnico);

            if (existeCategoria)
            {
                categorias.RemoveAt(indice);
            }

            return categorias;
        }

        private List<Categoria> MockListaCategorias()
        {
            return new List<Categoria>
            {
                new Categoria{IdentificadorUnico = 1, Tipo = "Alimento"},
                new Categoria{IdentificadorUnico = 2, Tipo = "Bebida"},
                new Categoria{IdentificadorUnico = 3, Tipo = "Sobremesa"}
            };
        }

        private Categoria MockCategoriaBuscar(int identificadorUnico)
        {
            var categoriaBase = new Categoria { Tipo = "Alimento", IdentificadorUnico = 1 };

            if (identificadorUnico == categoriaBase.IdentificadorUnico)
                return categoriaBase;

            return default;
        }

        private Categoria AdicionarIdentificador(Categoria categoria)
        {
            categoria.IdentificadorUnico = 1;
            return categoria;
        }

        private Categoria MockClienteAtualizar(Categoria categoriaRequest)
        {
            var categoriaBase = new Categoria { Tipo = "Alimento", IdentificadorUnico = 1 };

            if (categoriaRequest.IdentificadorUnico == categoriaBase.IdentificadorUnico)
            {
                categoriaBase.Tipo = categoriaRequest.Tipo;
            }

            return categoriaBase;
        }
        #endregion
    }
}
