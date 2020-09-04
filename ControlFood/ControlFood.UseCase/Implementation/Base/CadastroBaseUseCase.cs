using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase.Base;

namespace ControlFood.UseCase.Implementation.Base
{
    public abstract class CadastroBaseUseCase<T> : ICadastroBaseUseCase<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;

        public CadastroBaseUseCase(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public void Atualizar(T entidade)
        {
            _genericRepository.Atualizar(entidade);
        }

        public T Inserir(T entidade)
        {
            return _genericRepository.Inserir(entidade);
        }

        public T BuscarPorIdentificacao(T entidade, string propertyName)
        {
            var propriedades = entidade.GetType().GetProperties();

            foreach (var prop in propriedades)
            {
                if (prop.Name.Equals(propertyName))
                    return _genericRepository.BuscarPorId((int)prop.GetValue(entidade));
            }

            return default;
        }
    }
}
