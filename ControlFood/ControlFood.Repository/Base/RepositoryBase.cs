using ControlFood.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ControlFood.Repository.Base
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        
        private readonly ControlFoodContext _context;
        
        public RepositoryBase(ControlFoodContext context)
        {
            _context = context;
        }

        public TEntity Atualizar(TEntity entity)
        {
            var objetoPersistencia = this.MapearDominioParaRepository(entity);

            _context.Add(objetoPersistencia);
            _context.SaveChanges();

            return MapearRepositoryParaDominio(objetoPersistencia);
        }

        public TEntity BuscarPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(TEntity entity)
        {
            var objetoPersistencia = this.MapearDominioParaRepository(entity);
            
            _context.Remove(objetoPersistencia);
            _context.SaveChanges();
        }

        public TEntity Inserir(TEntity entity)
        {
            var objetoPersistencia = this.MapearDominioParaRepository(entity);

            _context.Add(objetoPersistencia);
            _context.SaveChanges();

            return MapearRepositoryParaDominio(objetoPersistencia);
        }

        public abstract List<TEntity> BuscarTodos();
        protected abstract object MapearDominioParaRepository(TEntity entity);
        protected abstract TEntity MapearRepositoryParaDominio(object objeto);
       
    }
}
