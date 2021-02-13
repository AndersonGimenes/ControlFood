using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroCategoriaUseCase : CadastroBaseUseCase<Categoria>, ICadastroCategoriaUseCase
    {
        public CadastroCategoriaUseCase(ICategoriaRepository categoriaRepository)
           : base(categoriaRepository)
        {
            // substituir por produtos
            //_subCategoriaRepository = subCategoriaRepository;
        }

        public override Categoria Inserir(Categoria categoria)
        {
            var catrgorias = base.BuscarTodos();

            CadastroCategoriaUseCaseValidation.ValidarRegrasParaInserir(categoria, catrgorias);

            return base.Inserir(categoria);
        }

        public override void Deletar(Categoria categoria)
        {
            // substituir por produto
            //var subCategorias = _subCategoriaRepository.BuscarTodos();

            //CadastroCategoriaUseCaseValidation.ValidarRegrasParaDeletar(categoria, subCategorias);

            base.Deletar(categoria);
        }
    }
}
