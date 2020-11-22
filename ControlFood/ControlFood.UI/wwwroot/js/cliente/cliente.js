$(document).ready(function () {
    var cliente = new Cliente();

    cliente.cadastrar(cliente);
    cliente.consultarEndereco(cliente);
});


class Cliente {

    constructor() {
        this._helper = new ComumHelper();
    }


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

    // Consulta estoque
    consultarEndereco = function (instanciaCliente) {

        $(".btn-consulta-endereco").click(function () {

            var elemento = this.parentNode.parentNode;

            var data = {
                IdentificadorUnico: instanciaCliente._helper.obterValorPorClasse(elemento, "identificador-unico")
            }

            instanciaCliente._helper.realizarChamadaAjax("Cliente/BuscarEndereco", data, "GET", null, instanciaCliente._acaoSucesso);

        });
      
    }

    // metodos privados
    _acaoSucesso = function (response) {
        var html = "";
        //var instanciaEstoque = Estoque.getInstancia();

        // mostrar modal
        $("#modal-consultar-endereco").modal("show");

        response.forEach(function (cliente) {

            html += "<tr>" +
                "<td class='cep'>" + cliente.endereco.cep + "</td>" +
                "<td class='logradouro'>" + cliente.endereco.logradouro + "</td>" +
                "<td class='numero'>" + cliente.endereco.numero + "</td>" +
                "<td class='complemento'>" + cliente.endereco.complemento + "</td>" +
                "<td class='info-apartamento-condominio'>" + cliente.endereco.infoApartamentoCondominio + "</td>" +
                "<td class='bairro'>" + cliente.endereco.bairro + "</td>" +
                "<td class='cidade'> " + cliente.endereco.cidade + "</td>" +
                "<td class='estado'>" + cliente.endereco.estado + "</td>" +
                "<td><button type='button' class='btn btn-primary btn-editar'>Editar</button></td>" +
                "<td><button type='button' class='btn btn-danger btn-deletar'> Deletar</button></td>" +
                "<td><input type = 'hidden' class='identificador-unico' value = '" + cliente.endereco.identificadorUnico + "' /></td>" +
                "</tr>"
        });

        $("#render-lista-endereco").html(html);

        //instanciaEstoque.popularModalAtualizar(instanciaEstoque);

        //instanciaEstoque.deletar(instanciaEstoque);
    }

}