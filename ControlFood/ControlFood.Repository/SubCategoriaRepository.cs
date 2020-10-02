using AutoMapper;
using Dominio = ControlFood.Domain.Entidades;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.UseCase.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.Repository
{
    public class SubCategoriaRepository :  RepositoryBase<Dominio.SubCategoria>, ISubCategoriaRepository
    {
        private readonly ControlFoodContext _context;
        private readonly IMapper _mapper;

        public SubCategoriaRepository(ControlFoodContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public override List<Dominio.SubCategoria> BuscarTodos()
        {
            var subCategoriasPersistidas = _context.SubCategoria
                                                .AsNoTracking()
                                                .Include(x => x.Categoria)                             
                                                .ToList();

            return _mapper.Map<List<Dominio.SubCategoria>>(subCategoriasPersistidas);
        }

        protected override object MapearDominioParaRepository(Dominio.SubCategoria subCategoriaDominio) => _mapper.Map<Entidades.SubCategoria>(subCategoriaDominio);

        protected override Dominio.SubCategoria MapearRepositoryParaDominio(object subCategoriaPersistida) => _mapper.Map<Dominio.SubCategoria>(subCategoriaPersistida);
    }
}
