using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using Xunit;

namespace ControlFood.UnitTest.Domain
{
    public class CategoriaValidacaoTest
    {
        [Fact]
        public void DeveLancarUmArgumentoInvalidoDomainExceptionQuandoNaoInformadoUmTipoDeCategoria()
        {
            var categoria = new Categoria();

            var ex = Assert.Throws<ArgumentoInvalidoDomainException>(() => categoria.IsValid());
            Assert.Equal("O campo Tipo deve ser preenchido", ex.Message);
        }
    }
}
