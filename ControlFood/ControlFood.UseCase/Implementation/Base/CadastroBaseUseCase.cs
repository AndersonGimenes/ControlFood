using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Implementation.Base
{
    public abstract class CadastroBaseUseCase<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;

        public CadastroBaseUseCase(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public virtual T Inserir(T entidade) => _genericRepository.Inserir(entidade);

        public void Atualizar(T entidade, List<string> propertiesName)
        {
            entidade.GetType().GetProperties().First(p => p.Name == nameof(Comum.DataAlteracao)).SetValue(entidade, DateTime.Now);

            _genericRepository.Atualizar(entidade, propertiesName);
        }

        public T BuscarPorIdentificacao(T entidade, string propertyName)
        {
            var valor = (int)entidade.GetType().GetProperties().First(p => p.Name == propertyName).GetValue(entidade);

            return _genericRepository.BuscarPorId(valor);
        }

        public List<T> BuscarTodos() => _genericRepository.BuscarTodos();

        public virtual void Deletar(T entidade)
        {
            _genericRepository.Deletar(entidade);
        }

    }
}
