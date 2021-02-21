using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroAdicionalUseCase : CadastroBaseUseCase<Adicional>, ICadastroAdicionalUseCase
    {
        public CadastroAdicionalUseCase(IAdicionalRepository adicionalRepository) : 
            base(adicionalRepository)
        {
        }

        public override Adicional Inserir(Adicional adicionalRequisicao)
        {
            var adicionaisPersistidas = base.BuscarTodos();

            CadastroAdicionalUseCaseValidation.ValidarRegrasParaInserir(adicionalRequisicao, adicionaisPersistidas);

            return base.Inserir(adicionalRequisicao);
        }
    }
}
