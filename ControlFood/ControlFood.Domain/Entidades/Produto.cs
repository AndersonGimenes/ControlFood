using System;
using System.Collections.Generic;

namespace ControlFood.Domain.Entidades
{
    public class Produto : Comum
    {
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenda { get; set; }
        public DateTime? DataValidade { get; set; }
        public Categoria Categoria { get; set; }
        public List<Adicional> Adicionais { get; set; }
    }
}
