using AutoMapper;
using ControlFood.Domain.Entidades;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.UseCase.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Repository
{
    public class AdicionalRepository : RepositoryBase<Dominio.Adicional>, IAdicionalRepository
    {
        private readonly IMapper _mapper;
        private readonly ControlFoodContext _context;

        public AdicionalRepository(ControlFoodContext context, IMapper mapper) : base(context)
        {

            _mapper = mapper;
            _context = context;
        }


        public override Adicional BuscarPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public override List<Adicional> BuscarTodos()
        {
            var adicionais = _context.Adicional
                                        .AsNoTracking()
                                        .ToList();

            return _mapper.Map<List<Adicional>>(adicionais);
        }

        protected override object MapearDominioParaRepository(Adicional adicionalDominio) => _mapper.Map<Entidades.Adicional>(adicionalDominio);

        protected override Adicional MapearRepositoryParaDominio(object adicionalPersistido) => _mapper.Map<Dominio.Adicional>(adicionalPersistido);
    }
}
