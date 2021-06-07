using AutoMapper;
using ControlFood.Domain.Entidades.Produto;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.Repository.Entidades;
using ControlFood.UseCase.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.Repository
{
    public class ProdutoRepository : RepositoryBase<ProdutoVenda>, IProdutoRepository
    {
        private readonly IMapper _mapper;
        private readonly ControlFoodContext _context;

        public ProdutoRepository(ControlFoodContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public override ProdutoVenda BuscarPorId(int id)
        {
            var produtosPersistido = _context.Produto
                                            .AsNoTracking()
                                            .Include(x => x.Categoria)
                                            .Include(x => x.Adicionais)
                                            .ThenInclude(x => x.Adicional)
                                            .First(x => x.Id == id);

            return MapearRepositoryParaDominio(produtosPersistido);
        }

        public override ProdutoVenda Inserir(ProdutoVenda produto)
        {
            var produtoPersistido = base.Inserir(produto);
            return AdicionarAdicionaisParaProduto(produto.AdicionaisIdentificadoresUnico.ToList(), produtoPersistido.IdentificadorUnico);
        }

        public override List<ProdutoVenda> BuscarTodos()
        {
            var produtosPersistidos = _context.Produto
                                            .AsNoTracking()
                                            .Include(x => x.Categoria)
                                            .Include(x => x.Adicionais)
                                            .ThenInclude(x => x.Adicional)
                                            .ToList();

            return _mapper.Map<List<ProdutoVenda>>(produtosPersistidos);
        }

        protected override object MapearDominioParaRepository(ProdutoVenda produto) => _mapper.Map<Models.ProdutoVenda>(produto);

        protected override ProdutoVenda MapearRepositoryParaDominio(object produtoPersistido) => _mapper.Map<ProdutoVenda>(produtoPersistido);

        private ProdutoVenda AdicionarAdicionaisParaProduto(List<int> adicionaisIdentificadores, int idProduto)
        {
            adicionaisIdentificadores.ForEach(id =>
            {
                var produtoAdicional = new ProdutoAdicional
                {
                    AdicionalId = id,
                    ProdutoId = idProduto
                };

                _context.ProdutoAdicional.Add(produtoAdicional);
            });

            _context.SaveChanges();

            return this.BuscarPorId(idProduto);
        }
    }
}
