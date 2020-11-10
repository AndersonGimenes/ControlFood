using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;
using System.Collections.Generic;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroSubCategoriaUseCase : CadastroBaseUseCase<SubCategoria>, ICadastroSubCategoriaUseCase
    {
        private ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;

        public CadastroSubCategoriaUseCase(ISubCategoriaRepository subCategoriaRepository, ICategoriaRepository categoriaRepository, IProdutoRepository produtoRepository)
            : base(subCategoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
        }

        public override SubCategoria Inserir(SubCategoria subCategoria)
        {
            var categorias = _categoriaRepository.BuscarTodos();
            var subCategorias = base.BuscarTodos();

            CadastroSubCategoriaUseCaseValidation.ValidarRegrasParaInserir(subCategoria, categorias, subCategorias);

            return base.Inserir(subCategoria);
        }

        public void AtualizarSubCategoria(SubCategoria subCategoria)
        {
            base.Atualizar(subCategoria, new List<string> { nameof(subCategoria.IndicadorItemBar), nameof(subCategoria.IndicadorItemCozinha) });
        }

        public override void Deletar(SubCategoria subCategoria)
        {
            var produtos = _produtoRepository.BuscarTodos();

            CadastroSubCategoriaUseCaseValidation.ValidarRegrasParaDeletar(subCategoria, produtos);

            base.Deletar(subCategoria);
        }


    }
}
