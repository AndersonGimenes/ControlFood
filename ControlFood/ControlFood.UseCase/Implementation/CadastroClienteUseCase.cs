using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroClienteUseCase : CadastroBaseUseCase<Cliente>, ICadastroClienteUseCase
    {
        private readonly ICadastroEnderecoUseCase _cadastroEnderecoUseCase;

        public CadastroClienteUseCase(IClienteRepository clienteRepository, ICadastroEnderecoUseCase cadastroEnderecoUseCase)
           : base(clienteRepository)
        {
            _cadastroEnderecoUseCase = cadastroEnderecoUseCase;
        }

        public void AtualizarCliente(Cliente cliente)
        {
            var camposAtualizacao = new List<string>
            {
                nameof(cliente.DataNascimento),
                nameof(cliente.Email),
                nameof(cliente.Nome),
                nameof(cliente.TelefoneCelular),
                nameof(cliente.TelefoneFixo),
            };

            // CadastroPessoaUseCaseValidation.ValidarRegrasParaAtualizar(cliente);

            base.Atualizar(cliente, camposAtualizacao);
            cliente.Enderecos.ForEach(x => _cadastroEnderecoUseCase.Atualizar(x));
        }

        public Cliente BuscarPorIdentificacao(Cliente cliente) =>
            base.BuscarPorIdentificacao(cliente, nameof(cliente.IdentificadorUnico));

        public override Cliente Inserir(Cliente cliente)
        {
            var clientes = base.BuscarTodos()
                               .Cast<Pessoa>()
                               .Where(x => x.Cpf != null)
                               .ToList();

            CadastroPessoaUseCaseValidation.ValidarRegrasParaInserir(cliente, clientes);

            cliente.AtribuirMensagemCamposNaoInformado();
            cliente.Enderecos.ForEach(x => x.AtribuirMensagemCamposNaoInformado());

            return base.Inserir(cliente);
        }
    }
}
