using ControlFood.UI.Validation;
using System.Collections.Generic;

namespace ControlFood.UI.Models
{
    public class Cliente : Pessoa
    {
        public Cliente()
        {
            Enderecos = new List<Endereco>();
        }
        public List<Endereco> Enderecos { get; set; }
        
        public void IsValid()
        {
            var valida = new ClienteValidation();
            valida.Validar(this);
        }
    }
}
