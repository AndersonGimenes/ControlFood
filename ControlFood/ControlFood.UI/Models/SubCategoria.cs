using ControlFood.UI.Validation;

namespace ControlFood.UI.Models
{
    public class SubCategoria
    {
        public int IdentificadorUnico { get; set; }
        public string Tipo { get; set; }
        public int Indicador { get; set; }
        public string Mensagem { get; set; }
        public Categoria Categoria { get; set; }

        public void IsValid()
        {
            var valida = new SubCategoriaValidation();
            valida.Validar(this);
        }
    }
}
