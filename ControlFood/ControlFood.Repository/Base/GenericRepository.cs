using ControlFood.UseCase.Interface.Repository;

namespace ControlFood.Repository.Base
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public TEntity Atualizar(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public TEntity BuscarPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public TEntity Inserir(TEntity entity)
        {
            // mapper generico
            throw new System.NotImplementedException();
        }
    }
}
