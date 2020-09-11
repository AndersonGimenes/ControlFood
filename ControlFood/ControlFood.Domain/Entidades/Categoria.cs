using ControlFood.Domain.Validation;

namespace ControlFood.Domain.Entidades
{
    public class Categoria
    {
        public int IdentificadorUnico { get; set; }

        public string Tipo { get; set; }

        public void IsValid()
        {
            var validate = new CategoriaValidation();
            validate.Validacao(this);
        }
    }
}