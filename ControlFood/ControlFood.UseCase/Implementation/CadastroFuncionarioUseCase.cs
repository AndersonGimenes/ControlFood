﻿using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroFuncionarioUseCase : CadastroBaseUseCase<Funcionario>, ICadastroFuncionarioUseCase
    {
        public CadastroFuncionarioUseCase(IGenericRepository<Funcionario> genericRepository)
            : base(genericRepository)
        {
        }
    }
}
