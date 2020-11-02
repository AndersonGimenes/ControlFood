using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroCategoriaUseCase : CadastroBaseUseCase<Categoria>, ICadastroCategoriaUseCase
    {
        private readonly ISubCategoriaRepository _subCategoriaRepository;

        public CadastroCategoriaUseCase(ICategoriaRepository categoriaRepository, ISubCategoriaRepository subCategoriaRepository)
           : base(categoriaRepository)
        {
            _subCategoriaRepository = subCategoriaRepository;
        }

        public override Categoria Inserir(Categoria categoria)
        {
            var catrgorias = base.BuscarTodos();

            CadastroCategoriaUseCaseValidation.ValidarRegrasParaInserir(categoria, catrgorias);

            return base.Inserir(categoria);
        }

        public override void Deletar(Categoria categoria)
        {
            var subCategorias = _subCategoriaRepository.BuscarTodos();

            CadastroCategoriaUseCaseValidation.ValidarRegrasParaDeletar(categoria, subCategorias);

            base.Deletar(categoria);
        }
    }
}
