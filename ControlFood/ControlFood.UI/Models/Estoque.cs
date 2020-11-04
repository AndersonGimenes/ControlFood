using ControlFood.UI.Validation;
using System;

namespace ControlFood.UI.Models
{
    public class Estoque
    {
        public int IdentificadorUnico { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }
        public decimal ValorCompraUnidade { get; set; }
        public decimal ValorCompraTotal { get; set; }

        public void IsValid()
        {
            var valida = new EstoqueValidation();
            valida.Validar(this);
        }
    }
}
