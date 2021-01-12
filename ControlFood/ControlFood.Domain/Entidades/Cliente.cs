namespace ControlFood.Domain.Entidades
{
    public class Cliente : Pessoa
    {
        public void AtribuirMensagemCamposNaoInformado()
        {
            if(string.IsNullOrEmpty(Cpf))
                Cpf = Constantes.Mensagem.CampoNaoInformado;

            if (string.IsNullOrEmpty(Email))
                Email = Constantes.Mensagem.CampoNaoInformado;
        }
    }
}
