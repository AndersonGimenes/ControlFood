class Estoque {

    constructor() {
        this._helper = new ComumHelper();
    }

    // propriedaes e metodos estaticos
    static _nomeProduto;
    static setNome(nome) { this._nomeProduto = nome };
    static getNome() { return this._nomeProduto };

    static _instanciaProduto;
    static setInstancia(instancia) { this._instanciaProduto = instancia };
    static getInstancia() { return this._instanciaProduto };

    popularModalCadastro = function (instanciaEstoque) {

        $(".btn-cadastro-estoque").click(function () {
            var elemento = this.parentNode.parentNode;

            //montar objeto produto
            var produto = {
                Nome: instanciaEstoque._helper.obterTextoPorClasse(elemento, "nome"),
                IdentificadorUnico: instanciaEstoque._helper.obterValorPorClasse(elemento, "identificador-unico")
            }

            // mostrar modal
            $("#modal-cadastro-estoque").modal("show");

            // preencher nome produto e identificador do produto
            $("#span-produto-nome-cadastro-estoque").html("<input type='text' class='form-control' disabled='disabled' value='" + produto.Nome + "'/>");
            $("#span-produto-identificador-unico").html("<input id='produto-identificador-unico' type='hidden' value='" + produto.IdentificadorUnico + "'/>");

        });
    }

    cadastrar = function (instanciaEstoque) {

        $("#btn-cadastrar-estoque").click(function () {
            var elemento = this.parentNode.parentNode;

            // validar campos obrigatorios
            var arrayElementos = [$("#quantidade-cadastro"), $("#valor-compra-unitario-cadastro"), $("#valor-compra-total-cadastro"), $("#data-validade-cadastro")];
            var arraySpans = [$("#span-valida-quantidade"), $("#span-valida-valor-compra-unitario"), $("#span-valida-valor-compra-total"), $("#span-valida-data-validade")];

            if (!instanciaEstoque._helper.validarCamposObrigatorios(arrayElementos, arraySpans))
                return;

            // montar objetos request
            var estoque = {
                Quantidade: instanciaEstoque._helper.obterValorPorId(elemento, "quantidade-cadastro"),
                DataValidade: instanciaEstoque._helper.obterValorPorId(elemento, "data-validade-cadastro"),
                ValorCompraUnidade: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-unitario-cadastro"),
                ValorCompraTotal: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-total-cadastro")
            }

            var data = {
                IdentificadorUnico: instanciaEstoque._helper.obterValorPorId(elemento, "produto-identificador-unico"),
                Estoque: estoque
            }

            // realizar requisição
            instanciaEstoque._helper.realizarChamadaAjax("Produto/CadastrarEstoque", data, "POST", instanciaEstoque._helper);

        });
    }

    consultar = function (instanciaEstoque) {
        $(".btn-consulta-estoque").click(function () {

            var elemento = this.parentNode.parentNode;

            var data = {
                Nome: instanciaEstoque._helper.obterTextoPorClasse(elemento, "nome"),
                IdentificadorUnico: instanciaEstoque._helper.obterValorPorClasse(elemento, "identificador-unico")
            }

            Estoque.setNome(data.Nome);
            Estoque.setInstancia(instanciaEstoque);

            instanciaEstoque._helper.realizarChamadaAjax("Produto/BuscarEstoque", data, "GET", null, instanciaEstoque.acaoSucesso);
        });

    }

    ajustarValorCompra = function (instanciaEstoque) {
        // autocompleta o valor de compra total com base no valor unitario
        $("#valor-compra-unitario-cadastro").blur(function () {

            var elemento = this.parentNode.parentNode;

            // monta objeto estoque
            var estoque = {
                Quantidade: instanciaEstoque._helper.obterValorPorId(elemento, "quantidade-cadastro"),
                ValorCompraUnidade: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-unitario-cadastro"),
                ValorCompraTotal: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-total-cadastro")
            }

            // validar campo quantidade antes do calculo
            if (!instanciaEstoque._helper.validarCamposObrigatorios([$("#quantidade-cadastro"), $("#valor-compra-unitario-cadastro")], [$("#span-valida-quantidade"), $("#span-valida-valor-compra-unitario")]))
                return;

            // formata valor para calculo 
            var valorFormatado = instanciaEstoque._helper.formatarValorInput(estoque.ValorCompraUnidade);

            // calculo 
            var resultado = parseInt(estoque.Quantidade) * valorFormatado;

            // devolve valor calculado e formatado para tela
            $("#valor-compra-total-cadastro").val(instanciaEstoque._helper.formatarValorOutput(resultado));

        });

        // autocompleta o valor de compra unitario com base no valor total
        $("#valor-compra-total-cadastro").blur(function () {

            var elemento = this.parentNode.parentNode;

            // monta objeto estoque
            var estoque = {
                Quantidade: instanciaEstoque._helper.obterValorPorId(elemento, "quantidade-cadastro"),
                ValorCompraUnidade: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-unitario-cadastro"),
                ValorCompraTotal: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-total-cadastro")
            }

            // validar campo quantidade antes do calculo
            if (!instanciaEstoque._helper.validarCamposObrigatorios([$("#quantidade-cadastro"), $("#valor-compra-total-cadastro")], [$("#span-valida-quantidade"), $("#span-valida-valor-compra-total")]))
                return;

            // formata valor para calculo
            var valorFormatado = instanciaEstoque._helper.formatarValorInput(estoque.ValorCompraTotal);

            // calculo
            var resultado = valorFormatado / parseInt(estoque.Quantidade);

            // devolve valor calculado e formatado para tela
            $("#valor-compra-unitario-cadastro").val(instanciaEstoque._helper.formatarValorOutput(resultado));

        });

    }

    limparCampoInput = function () {
        $("#btn-close").click(function () {
            $("#quantidade-cadastro").val('');
            $("#data-validade-cadastro").val('');
            $("#valor-compra-unitario-cadastro").val('');
            $("#valor-compra-total-cadastro").val('');
        });
    }

    acaoSucesso = function (response) {
        var html = "";
        var somaQuantidade = 0;
        var instanciaEstoque = Estoque.getInstancia();

        $("#modal-consultar-estoque").modal("show");

        $("#titulo-modal-consulta-estoque").text("Consulta estoque " + Estoque.getNome());

        response.forEach(function (estoque) {

            html += "<tr>" +
                        "<td>" + estoque.quantidade + "</td>" +
                        "<td>R$ " + instanciaEstoque._helper.formatarValorOutput(estoque.valorCompraUnidade) + "</td>" +
                        "<td>R$ " + instanciaEstoque._helper.formatarValorOutput(estoque.valorCompraTotal) + "</td>" +
                        "<td>" + instanciaEstoque._helper.formatarDataOutput(estoque.dataValidade) + "</td>" +
                        "<td><button type='button' class='btn btn-primary btn-editar-estoque'>Editar</button></td>" +
                        "<td><button type='button' class='btn btn-danger btn-deletar-estoque'> Deletar</button></td>" +
                    "</tr>";

            somaQuantidade += parseInt(estoque.quantidade);

        });

        $("#quantidade-total").text(somaQuantidade);

        $("#render-lista-estoque").html(html);
    }
}