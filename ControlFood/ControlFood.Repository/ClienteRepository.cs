using AutoMapper;
using ControlFood.Repository.Base;
using ControlFood.Repository.Context;
using ControlFood.Repository.Entidades;
using ControlFood.UseCase.Interface.Repository;
using System.Collections.Generic;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.Repository
{
    public class ClienteRepository : RepositoryBase<Dominio.Cliente>, IClienteRepository
    {
        private readonly IMapper _mapper;

        public ClienteRepository(ControlFoodContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public override Dominio.Cliente BuscarPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public override List<Dominio.Cliente> BuscarTodos()
        {
            throw new System.NotImplementedException();
        }

        protected override object MapearDominioParaRepository(Dominio.Cliente cliente) => _mapper.Map<Cliente>(cliente);

        protected override Dominio.Cliente MapearRepositoryParaDominio(object cliente) => _mapper.Map<Dominio.Cliente>(cliente);
    }
}
