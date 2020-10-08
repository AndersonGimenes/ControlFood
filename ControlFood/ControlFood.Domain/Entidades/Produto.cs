﻿namespace ControlFood.Domain.Entidades
{
    public class Produto
    {
        public Produto()
        {
            this.SubCategoria = new SubCategoria();
            this.Estoque = new Estoque();
        }
        public int IdentificadorUnico { get; set; }
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public SubCategoria SubCategoria { get; set; }
        public Estoque Estoque { get; set; }
    }
}
