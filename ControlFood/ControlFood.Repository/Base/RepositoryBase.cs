using ControlFood.Repository.Context;
using ControlFood.Repository.Entidades;
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

        public TEntity Atualizar(TEntity entity, List<string> propertiesName)
        {
            var objetoPersistencia = MapearDominioParaRepository(entity);

            _context.Attach(objetoPersistencia);

            propertiesName.ForEach(propertyName => { 
                _context.Entry(objetoPersistencia).Property(propertyName).IsModified = true; 
            });

            _context.Entry(objetoPersistencia).Property(nameof(Comum.DataAlteracao)).IsModified = true;
            
            _context.SaveChanges();

            return MapearRepositoryParaDominio(objetoPersistencia);
        }

        
        public void Deletar(TEntity entity)
        {
            var objetoPersistencia = this.MapearDominioParaRepository(entity);
            
            _context.Remove(objetoPersistencia);
            _context.SaveChanges();
        }

        public virtual TEntity Inserir(TEntity entity)
        {
            var objetoPersistencia = this.MapearDominioParaRepository(entity);

            _context.Add(objetoPersistencia);
            _context.SaveChanges();

            return MapearRepositoryParaDominio(objetoPersistencia);
        }

        public abstract List<TEntity> BuscarTodos();
        public abstract TEntity BuscarPorId(int id);
        protected abstract object MapearDominioParaRepository(TEntity entity);
        protected abstract TEntity MapearRepositoryParaDominio(object objeto);
       
    }
}
