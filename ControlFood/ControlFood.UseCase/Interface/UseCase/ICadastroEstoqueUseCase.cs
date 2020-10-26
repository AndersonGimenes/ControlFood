﻿using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.UseCase.Base;

namespace ControlFood.UseCase.Interface.UseCase
{
    public interface ICadastroEstoqueUseCase : IGenericCadastroUseCase<Estoque>
    {
        Produto InserirEstoque(Produto produto);
    }
}
