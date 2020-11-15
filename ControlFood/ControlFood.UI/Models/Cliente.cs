using ControlFood.UI.Validation;

namespace ControlFood.UI.Models
{
    public class Cliente : Pessoa
    {
        public Endereco Endereco { get; set; }

        public void IsValid()
        {
            var valida = new ClienteValidation();
            valida.Validar(this);
        }
    }
}
