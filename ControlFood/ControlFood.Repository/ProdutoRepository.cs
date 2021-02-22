using AutoMapper;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.Repository.Entidades;
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
            var produtosPersistido = _context.Produto
                                            .AsNoTracking()
                                            .Include(x => x.Categoria)
                                            .Include(x => x.Adicionais)
                                            .ThenInclude(x => x.Adicional)
                                            .First(x => x.Id == id);

            return MapearRepositoryParaDominio(produtosPersistido);
        }

        public override Dominio.Produto Inserir(Dominio.Produto produto)
        {
            var produtoPersistido = base.Inserir(produto);
            return AdicionarAdicionaisParaProduto(produto.Adicionais, produtoPersistido.IdentificadorUnico);
        }

        public override List<Dominio.Produto> BuscarTodos()
        {
            var produtosPersistidos = _context.Produto
                                            .AsNoTracking()
                                            .Include(x => x.Categoria)
                                            .Include(x => x.Adicionais)
                                            .ThenInclude(x => x.Adicional)
                                            .ToList();

            return _mapper.Map<List<Dominio.Produto>>(produtosPersistidos);
        }

        protected override object MapearDominioParaRepository(Dominio.Produto produto) => _mapper.Map<Entidades.Produto>(produto);

        protected override Dominio.Produto MapearRepositoryParaDominio(object produtoPersistido) => _mapper.Map<Dominio.Produto>(produtoPersistido);

        private Dominio.Produto AdicionarAdicionaisParaProduto(List<Dominio.Adicional> adicionais, int idProduto)
        {
            adicionais.ForEach(a =>
            {
                var produtoAdicional = new ProdutoAdicional
                {
                    AdicionalId = a.IdentificadorUnico,
                    ProdutoId = idProduto
                };

                _context.ProdutoAdicional.Add(produtoAdicional);
            });

            _context.SaveChanges();

            return this.BuscarPorId(idProduto);
        }
    }
}
