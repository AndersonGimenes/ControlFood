using ControlFood.UI.Validation;

namespace ControlFood.UI.Models
{
    public class Categoria
    {
        public int IdentificadorUnico { get; set; }
        public string Tipo { get; set; }
        public string Mensagem { get; set; }

        public void IsValid()
        {
            var valida = new CategoriaValidation();
            valida.Validar(this);
        }
       
    }
}
