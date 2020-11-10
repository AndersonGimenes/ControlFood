﻿using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.UseCase.Base;
using System.Collections.Generic;

namespace ControlFood.UseCase.Interface.UseCase
{
    public interface ICadastroEstoqueUseCase : IGenericCadastroUseCase<Estoque>
    {
        Produto InserirEstoque(Produto produto);
        List<Produto> BuscarDadosProdutoXEstoques(Produto produto);
        Produto AtualizarEstoque(Produto produto);
    }
}
