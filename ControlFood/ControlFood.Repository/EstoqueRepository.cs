using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.Repository.Entidades;
using ControlFood.UseCase.Interface.Repository;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ControlFood.Repository
{
    public class EstoqueRepository : RepositoryBase<Dominio.Estoque>, IEstoqueRepository
    {
        private readonly IMapper _mapper;
        private readonly ControlFoodContext _context;


        public EstoqueRepository(ControlFoodContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
            _context = context; 
        }

        public override Dominio.Estoque BuscarPorId(int id) => default;

        public override List<Dominio.Estoque> BuscarTodos()
        {
            var estoquesPersistidos = _context.Estoque
                                                .AsNoTracking()
                                                .ToList();

            return _mapper.Map<List<Dominio.Estoque>>(estoquesPersistidos);
        }

        protected override object MapearDominioParaRepository(Dominio.Estoque estoque) => _mapper.Map<Estoque>(estoque);

        protected override Dominio.Estoque MapearRepositoryParaDominio(object estoque) => _mapper.Map<Dominio.Estoque>(estoque);
    }
}