$(document).ready(function () {
    var cliente = new Cliente();

    cliente.cadastrar(cliente);
    cliente.consultarEndereco(cliente);
    cliente.validarEmail();
});


class Cliente {

    constructor() {
        this._helper = new ComumHelper();

        // Mascara CPF
        $('#cpf').mask('000.000.000-00', { reverse: true });

        // Mascara CEP
        $('#cep').mask('00000-000');

        // Mascara Fone Fixo
        $('#telefone-fixo').mask('(00) 0000-0000');

        // Mascara Fone Celular
        $('#telefone-celular').mask('(00) 00000-0000');
    }

    static _instanciaCliente;
    static setInstancia(instancia) { this._instanciaCliente = instancia };
    static getInstancia() { return this._instanciaCliente };

    cadastrar = function (instanciaCliente) {

        $("#btn-cadastrar").click(function () {
            var elemento = this.parentNode;

            // validar campos obrigatorios
            var arrayElementos = [$("#nome"), $("#logradouro"), $("#cep"), $("#bairro"), $("#cidade")];
            var arraySpans = [$("#span-valida-nome"), $("#span-valida-logradouro"), $("#span-valida-cep"), $("#span-valida-bairro"), $("#span-valida-cidade")];

            if (!instanciaCliente._helper.validarCamposObrigatorios(arrayElementos, arraySpans))
                return;

            // montar objetos request
            var endereco = {
                Logradouro: instanciaCliente._helper.obterValorPorId(elemento, "logradouro"),
                Numero: instanciaCliente._helper.obterValorPorId(elemento, "numero"),
                InfoApartamentoCondominio: instanciaCliente._helper.obterValorPorId(elemento, "condominio-apartamento"),
                Cep: instanciaCliente._helper.obterValorPorId(elemento, "cep").replace('-', ''),
                Complemento: instanciaCliente._helper.obterValorPorId(elemento, "complemento"),
                Bairro: instanciaCliente._helper.obterValorPorId(elemento, "bairro"),
                Cidade: instanciaCliente._helper.obterValorPorId(elemento, "cidade"),
                Estado: instanciaCliente._helper.obterValorPorId(elemento, "estado")
            }

            var data = {
                Nome: instanciaCliente._helper.obterValorPorId(elemento, "nome"),
                Cpf: instanciaCliente._helper.obterValorPorId(elemento, "cpf").replace('.', '').replace('.', '').replace('-', ''),
                TelefoneFixo: instanciaCliente._helper.obterValorPorId(elemento, "telefone-fixo").replace('(', '').replace(')', '').replace(' ', '').replace('-', ''),
                TelefoneCelular: instanciaCliente._helper.obterValorPorId(elemento, "telefone-celular").replace('(', '').replace(')', '').replace(' ', '').replace('-', ''),
                DataNascimento: instanciaCliente._helper.obterValorPorId(elemento, "data-nascimento"),
                Email: instanciaCliente._helper.obterValorPorId(elemento, "email"),
                Enderecos: [endereco]
            }

            // realizar requisição
            instanciaCliente._helper.realizarChamadaAjax("Cliente/Cadastrar", data, "POST", instanciaCliente._helper);
        });
    }

    // Consulta estoque
    consultarEndereco = function (instanciaCliente) {

        $(".btn-consulta-endereco").click(function () {

            var elemento = this.parentNode.parentNode;

            var data = {
                IdentificadorUnico: instanciaCliente._helper.obterValorPorClasse(elemento, "identificador-unico"),
            }

             Cliente.setInstancia(instanciaCliente);

            instanciaCliente._helper.realizarChamadaAjax("Cliente/BuscarEndereco", data, "GET", null, instanciaCliente._acaoSucesso);

        });

    }

    validarEmail() {
        $("#email").blur(function () {
            var valor = $("#email").val();

            if (valor != null && valor != 'undefined' && valor != "") {
                if (valor.indexOf('@') == -1 || valor.indexOf('.') == -1)
                    $("#span-valida-email").html("<p class='text-danger'>O formato do e-mail esta incorreto !<p/>")
                else {
                    $("#span-valida-email").html("");
                }
            }
        });
    }

    // metodos privados
    _acaoSucesso = function (response) {
        var html = "";

        // mostrar modal
        $("#modal-consultar-endereco").modal("show");

        var instanciaCliente = Cliente.getInstancia();

        response.enderecos.forEach(function (endereco) {

            html += "<tr>" +
                        "<td class='cep'>" + instanciaCliente._formatarCep(endereco.cep) + "</td>" +
                        "<td class='logradouro'>" + endereco.logradouro + "</td>" +
                        "<td class='numero'>" + endereco.numero + "</td>" +
                        "<td class='complemento'>" + endereco.complemento + "</td>" +
                        "<td class='info-apartamento-condominio'>" + endereco.infoApartamentoCondominio + "</td>" +
                        "<td class='bairro'>" + endereco.bairro + "</td>" +
                        "<td class='cidade'> " + endereco.cidade + "</td>" +
                        "<td class='estado'>" + endereco.estado + "</td>" +
                        "<td><button type='button' class='btn btn-primary btn-editar'>Editar</button></td>" +
                        "<td><button type='button' class='btn btn-danger btn-deletar'> Deletar</button></td>" +
                        "<td><input type = 'hidden' class='identificador-unico' value = '" + endereco.identificadorUnico + "' /></td>" +
                    "</tr>"
        });

        $("#render-lista-endereco").html(html);

    } 

    _formatarCep = function (cep) {
        var primeiraSequencia = cep.substring(0, 5);
        var segundaSequencia = cep.substring(5, 8);

        return primeiraSequencia + '-' + segundaSequencia;
    }
}