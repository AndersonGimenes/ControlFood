﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ControlFood.UseCase.Interface.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Inserir(TEntity entity);
        TEntity Atualizar(TEntity entity);
        void Deletar(TEntity entity);
        TEntity BuscarPorId(int id);
    }
}
