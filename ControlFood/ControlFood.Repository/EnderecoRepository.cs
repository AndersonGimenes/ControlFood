using ControlFood.Repository.Base;
using Dominio = ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.Repository.Context;
using AutoMapper;
using System.Collections.Generic;
using ControlFood.Repository.Entidades;

namespace ControlFood.Repository
{
    public class EnderecoRepository : RepositoryBase<Dominio.Endereco>, IEnderecoRepository
    {
        private readonly IMapper _mapper;
        
        public EnderecoRepository(ControlFoodContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;        
        }

        public override Dominio.Endereco BuscarPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public override List<Dominio.Endereco> BuscarTodos()
        {
            throw new System.NotImplementedException();
        }

        protected override object MapearDominioParaRepository(Dominio.Endereco endereco) => _mapper.Map<Endereco>(endereco);

        protected override Dominio.Endereco MapearRepositoryParaDominio(object endereco) => _mapper.Map<Dominio.Endereco>(endereco);
    }
}
