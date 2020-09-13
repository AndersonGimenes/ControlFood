using ControlFood.Domain.Entidades;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.UseCase.Interface.Repository;
using System.Collections.Generic;

namespace ControlFood.Repository
{
    public class SubCategoriaRepository :  RepositoryBase<SubCategoria>, ISubCategoriaRepository
    {
        public SubCategoriaRepository(ControlFoodContext context)
            : base(context)
        {
                
        }

        public override List<SubCategoria> BuscarTodos()
        {
            throw new System.NotImplementedException();
        }

        protected override object MapearDominioParaRepository(SubCategoria entity)
        {
            throw new System.NotImplementedException();
        }

        protected override SubCategoria MapearRepositoryParaDominio(object objeto)
        {
            throw new System.NotImplementedException();
        }
    }
}
