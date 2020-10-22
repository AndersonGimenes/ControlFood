using AutoMapper;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.UseCase.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Repository
{
    public class ProdutoRepository : RepositoryBase<Dominio.Produto>, IProdutoRepository
    {
        private readonly IMapper _mapper;
        private readonly ControlFoodContext _context;

        public ProdutoRepository(ControlFoodContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public override Dominio.Produto BuscarPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public override List<Dominio.Produto> BuscarTodos()
        {
            var produtosPersistidos = _context.Produto
                                            .AsNoTracking()
                                            .Include(x => x.SubCategoria)
                                            .ToList();

            return _mapper.Map<List<Dominio.Produto>>(produtosPersistidos);
        }

        protected override object MapearDominioParaRepository(Dominio.Produto produto)
        {
            throw new System.NotImplementedException();
        }

        protected override Dominio.Produto MapearRepositoryParaDominio(object produtoPersistido) => _mapper.Map<Dominio.Produto>(produtoPersistido);
    }
}
