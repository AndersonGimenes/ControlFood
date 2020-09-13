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
        public CadastroCategoriaUseCase(ICategoriaRepository categoriaRepository)
           : base(categoriaRepository)
        {
        }

        public void VerificarDuplicidade(Categoria categoriaDominio, List<Categoria> categorias)
        {
            string mensagemErro = string.Empty;

            if (categorias.Any(c => c.Tipo == categoriaDominio.Tipo))
                throw new CategoriaIncorretaUseCaseException(string.Format(Domain.Constantes.Mensagem.Validacao.CategoriaDuplicada, categoriaDominio.Tipo));
                
        }
    }
}
