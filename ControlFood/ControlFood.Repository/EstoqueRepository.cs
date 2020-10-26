using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.Repository.Entidades;
using ControlFood.UseCase.Interface.Repository;
using System.Collections.Generic;

namespace ControlFood.Repository
{
    public class EstoqueRepository : RepositoryBase<Dominio.Estoque>, IEstoqueRepository
    {
        private readonly IMapper _mapper;
        
        public EstoqueRepository(ControlFoodContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public override Dominio.Estoque BuscarPorId(int id) => default;

        public override List<Dominio.Estoque> BuscarTodos() => default;

        protected override object MapearDominioParaRepository(Dominio.Estoque estoque) => _mapper.Map<Estoque>(estoque);

        protected override Dominio.Estoque MapearRepositoryParaDominio(object estoque) => _mapper.Map<Dominio.Estoque>(estoque);
    }
}