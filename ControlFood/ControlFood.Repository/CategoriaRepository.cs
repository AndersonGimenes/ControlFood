using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.UseCase.Interface.Repository;
using System.Collections.Generic;
using ControlFood.Domain.Entidades;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ControlFood.Repository
{
    public class CategoriaRepository : RepositoryBase<Dominio.Categoria>, ICategoriaRepository
    {
        private readonly IMapper _mapper;
        private readonly ControlFoodContext _context;

        public CategoriaRepository(ControlFoodContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public override List<Dominio.Categoria> BuscarTodos()
        {
            var categoriasPersistidas = _context.Categoria
                                            .AsNoTracking()
                                            .ToList();

            return _mapper.Map<List<Dominio.Categoria>>(categoriasPersistidas);
        }

        protected override object MapearDominioParaRepository(Dominio.Categoria categoriaDominio) => _mapper.Map<Entidades.Categoria>(categoriaDominio);

        protected override Dominio.Categoria MapearRepositoryParaDominio(object categoriaPersistida) => _mapper.Map<Dominio.Categoria>(categoriaPersistida);

    }
}
