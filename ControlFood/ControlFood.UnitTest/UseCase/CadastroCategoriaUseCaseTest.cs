using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Implementation;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ControlFood.UnitTest.UseCase
{
    public class CadastroCategoriaUseCaseTest
    {
        private readonly Mock<ICategoriaRepository> _mockGeneciRepository;
        private readonly ICadastroCategoriaUseCase _cadastroCategoria;

        public CadastroCategoriaUseCaseTest()
        {
            _mockGeneciRepository = new Mock<ICategoriaRepository>();
            _cadastroCategoria = new CadastroCategoriaUseCase(_mockGeneciRepository.Object);
        }

        [Fact]
        public void DeveInserirUmaCategoriaNoSistemaComSucesso()
        {
            var categoria = new Categoria { Tipo = "Alimento" };

            _mockGeneciRepository
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

            _mockGeneciRepository
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

            _mockGeneciRepository
                .Setup(x => x.BuscarPorId(It.IsAny<int>()))
                .Returns(MockCategoriaBuscar(categoriaRequest.IdentificadorUnico));

            var retorno = _cadastroCategoria.BuscarPorIdentificacao(categoriaRequest, nameof(categoriaRequest.IdentificadorUnico));

            Assert.True(retorno != null);
            Assert.Equal("Alimento", retorno.Tipo);
        }

        [Fact]
        public void DeveBuscarTodasAsCategoriasCadastradas()
        {
            _mockGeneciRepository
                .Setup(x => x.BuscarTodos())
                .Returns(MockListaCategorias());

            var retorno = _cadastroCategoria.BuscarTodos();

            Assert.True(retorno != null);
            Assert.True(retorno.Count > 0);
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
    }
}
