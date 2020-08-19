﻿using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.UseCase.Base;

namespace ControlFood.UseCase.Interface.UseCase
{
    public interface ICadastroClienteUseCase : ICadastroBaseUseCase<Cliente>
    {
        Cliente BuscarPorIdentificacao(Cliente cliente);
    }
}
