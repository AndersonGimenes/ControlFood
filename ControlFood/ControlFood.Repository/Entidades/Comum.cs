using System;

namespace ControlFood.Repository.Entidades
{
    public abstract class Comum
    {
        public int Id { get; set; }
        public DateTime? DataAlteracao { get; protected set; }
    }
}
