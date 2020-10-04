﻿using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroProdutoUseCase : CadastroBaseUseCase<Produto>, ICadastroProdutoUseCase
    {
        public CadastroProdutoUseCase(IGenericRepository<Produto> genericRepository)
            : base(genericRepository)
        {
        }
    }
}
