﻿using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.UseCase.Base;

namespace ControlFood.UseCase.Interface.UseCase
{
    public interface ICadastroProdutoUseCase : IGenericCadastroUseCase<Produto>
    {
        void AtualizarProduto(Produto produto);
    }
}
