$(document).ready(function () {
    var cliente = new Cliente();
    cliente.cadastrar(cliente);

});


class Cliente {

    constructor() {
        this._helper = new ComumHelper();
    }


    cadastrar = function (instanciaCliente) {

        $("#btn-cadastrar").click(function () {
            var elemento = this.parentNode;

            // validar campos obrigatorios
            var arrayElementos = [$("#nome"), $("#logradouro"), $("#cep"), $("#bairro"), $("#cidade") ];
            var arraySpans = [$("#span-valida-nome"), $("#span-valida-logradouro"), $("#span-valida-cep"), $("#span-valida-bairro"), $("#span-valida-cidade")];

            if (!instanciaCliente._helper.validarCamposObrigatorios(arrayElementos, arraySpans))
                return;

            // montar objetos request
            var endereco = {
                Logradouro: instanciaCliente._helper.obterValorPorId(elemento, "logradouro"),
                Numero: instanciaCliente._helper.obterValorPorId(elemento, "numero"),
                InfoApartamentoCondominio: instanciaCliente._helper.obterValorPorId(elemento, "condominio-apartamento"),
                Cep: instanciaCliente._helper.obterValorPorId(elemento, "cep"),
                Complemento: instanciaCliente._helper.obterValorPorId(elemento, "complemento"),
                Bairro: instanciaCliente._helper.obterValorPorId(elemento, "bairro"),
                Cidade: instanciaCliente._helper.obterValorPorId(elemento, "cidade"),
                Estado: instanciaCliente._helper.obterValorPorId(elemento, "estado")
            }

            var data = {
                Nome: instanciaCliente._helper.obterValorPorId(elemento, "nome"),
                Cpf: instanciaCliente._helper.obterValorPorId(elemento, "cpf"),
                TelefoneFixo: instanciaCliente._helper.obterValorPorId(elemento, "telefone-fixo"),
                TelefoneCelular: instanciaCliente._helper.obterValorPorId(elemento, "telefone-celular"),
                DataNascimento: instanciaCliente._helper.obterValorPorId(elemento, "data-nascimento"),
                Email: instanciaCliente._helper.obterValorPorId(elemento, "email"),
                Endereco: endereco
            }

            console.log(data);

            // realizar requisição
            instanciaCliente._helper.realizarChamadaAjax("Cliente/Cadastrar", data, "POST", instanciaCliente._helper);
        });
    }
}