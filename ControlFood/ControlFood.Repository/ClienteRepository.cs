using AutoMapper;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.Repository.Entidades;
using ControlFood.UseCase.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Repository
{
    public class ClienteRepository : RepositoryBase<Dominio.Cliente>, IClienteRepository
    {
        private readonly IMapper _mapper;
        private readonly ControlFoodContext _context;

        public ClienteRepository(ControlFoodContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
            _context = context;
        }

        public override Dominio.Cliente BuscarPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public override List<Dominio.Cliente> BuscarTodos()
        {
            var clientesPersistidos = _context.Cliente
                                                .AsNoTracking()
                                                .ToList();

            return _mapper.Map<List<Dominio.Cliente>>(clientesPersistidos);
        }

        protected override object MapearDominioParaRepository(Dominio.Cliente cliente) =>
            _mapper.Map<Dominio.Cliente, Cliente>(cliente, opts => 
                opts.AfterMap((src, dest) => {
                    dest.Enderecos = new List<Endereco>
                    {
                        _mapper.Map<Endereco>(src.Endereco)
                    };
                }));
        
       protected override Dominio.Cliente MapearRepositoryParaDominio(object cliente) => _mapper.Map<Dominio.Cliente>(cliente);
    }
}
