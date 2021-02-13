using ControlFood.Domain.Entidades;
using ControlFood.UnitTest.Helpers;
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
    public class CadastroCategoriaUseCaseTest
    {
        private readonly Mock<ICategoriaRepository> _mockCategoriaRepository;
        private readonly ICadastroCategoriaUseCase _cadastroCategoria;
        private int categoriasPersistidasDepois;

        public CadastroCategoriaUseCaseTest()
        {
            _mockCategoriaRepository = new Mock<ICategoriaRepository>();
            //_mockSubCategoriaRepository = new Mock<ISubCategoriaRepository>();
            //_cadastroCategoria = new CadastroCategoriaUseCase(_mockCategoriaRepository.Object, _mockSubCategoriaRepository.Object);

            _mockCategoriaRepository
               .Setup(x => x.BuscarTodos())
               .Returns(HelperMock.MockListaCategoriasPersistidas());

            // substituir por mock de categorias
            //_mockSubCategoriaRepository
            //    .Setup(x => x.BuscarTodos())
            //    .Returns(HelperMock.MockListaSubCategoriasPersistidas());
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
            Assert.True(categoria.DataCadastro > DateTime.MinValue && categoria.DataCadastro < DateTime.Now);
        }

        [Fact]
        public void DeveLancarUmaExceptionCasoACategoriaSejaDuplicada()
        {
            var categoriaRequest = new Categoria { Tipo = "Alimento", IdentificadorUnico = 0 };

            var ex = Assert.Throws<CategoriaIncorretaUseCaseException>(() => _cadastroCategoria.Inserir(categoriaRequest));
            Assert.Equal("A categoria Alimento ja existe no sistema", ex.Message);

        }
        [Fact]
        public void DeveDeletarUmaCategoriaExistente()
        {
            var categoria = new Categoria { Tipo = "CategoriaTeste", IdentificadorUnico = 4 };
            var categorias = HelperMock.MockListaCategoriasPersistidas();
            var categoriasPersistidasAntes = categorias.Count;


            _mockCategoriaRepository
                .Setup(x => x.Deletar(It.IsAny<Categoria>()))
                .Callback(() => categoriasPersistidasDepois = HelperComum<Categoria>.DeletarRegistro(categoria, categorias, nameof(categoria.IdentificadorUnico)));

            _cadastroCategoria.Deletar(categoria);

            Assert.True(categoriasPersistidasAntes > categoriasPersistidasDepois);

        }

        [Fact]
        public void DeveLancarExceptionAoTentarDeletarUmaCategoriaQueTenhaUmaSubCategoriaVinculadaAMesma()
        {
            var categoria = new Categoria { Tipo = "Alimento", IdentificadorUnico = 1 };

            var ex = Assert.Throws<CategoriaIncorretaUseCaseException>(() => _cadastroCategoria.Deletar(categoria));
            Assert.Equal("Existe Sub-categoria vinculada a Categoria Alimento.", ex.Message);
        }
    }
}
