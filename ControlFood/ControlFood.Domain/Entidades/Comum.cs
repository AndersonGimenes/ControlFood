﻿using System;

namespace ControlFood.Domain.Entidades
{
    public abstract class Comum
    {
        public int IdentificadorUnico { get; set; }
        public DateTime? DataAlteracao { get; set; }

    }
}
