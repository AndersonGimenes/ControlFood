﻿namespace ControlFood.UI.Models
{
    public class SubCategoria
    {
        public int IdentificadorUnico { get; set; }
        public string Tipo { get; set; }
        public int Indicador { get; set; }
        public Categoria Categoria { get; set; }
    }
}