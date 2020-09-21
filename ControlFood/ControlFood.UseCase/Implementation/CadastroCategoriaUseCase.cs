using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using System.Collections.Generic;
using System.Linq;

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
            this.VerificarDuplicidade(categoria, base.BuscarTodos());

            return base.Inserir(categoria);
        }

        public override void Deletar(Categoria categoria)
        {
            var subCategorias = _subCategoriaRepository.BuscarTodos();
            this.VerificarSubCategoriaVinculada(categoria, subCategorias);

            base.Deletar(categoria);
        }

        private void VerificarSubCategoriaVinculada(Categoria categoria, List<SubCategoria> subCategorias)
        {
            if (subCategorias.Any(x => x.Categoria.IdentificadorUnico == categoria.IdentificadorUnico))
                throw new CategoriaIncorretaUseCaseException(string.Format(Domain.Constantes.Mensagem.Validacao.CategoriaVinculadaASubCategoria, categoria.Tipo));
        }

        private void VerificarDuplicidade(Categoria categoria, List<Categoria> categorias)
        {
            if (categorias.Any(c => c.Tipo == categoria.Tipo))
                throw new CategoriaIncorretaUseCaseException(string.Format(Domain.Constantes.Mensagem.Validacao.CategoriaDuplicada, categoria.Tipo));
        }
    }
}
