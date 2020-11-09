$(document).ready(function () {
    var estoque = new Estoque();
    estoque.popularModalCadastro(estoque);
    estoque.cadastrar(estoque);

    estoque.ajustarValorCompra(estoque, "cadastro");
    estoque.limparCampoInput();

    estoque.consultar(estoque);
    estoque.atualizar(estoque);
    
});

class Estoque {

    constructor() {
        this._helper = new ComumHelper();

        // modal cadastro estoque
        // input valor compra unitario
        this._helper.mascaraValorMonetario($("#valor-compra-unitario-cadastro"));

        // input valor compra total
        this._helper.mascaraValorMonetario($("#valor-compra-total-cadastro"));
    }

    // propriedaes e metodos estaticos
    static _nomeProduto;
    static setNome(nome) { this._nomeProduto = nome };
    static getNome() { return this._nomeProduto };

    static _instanciaProduto;
    static setInstancia(instancia) { this._instanciaProduto = instancia };
    static getInstancia() { return this._instanciaProduto };

    // Novo cadastro estoque
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

    // Consulta estoque
    consultar = function (instanciaEstoque) {

        $(".btn-consulta-estoque").click(function () {

            var elemento = this.parentNode.parentNode;

            var data = {
                Nome: instanciaEstoque._helper.obterTextoPorClasse(elemento, "nome"),
                IdentificadorUnico: instanciaEstoque._helper.obterValorPorClasse(elemento, "identificador-unico")
            }

            Estoque.setNome(data.Nome);
            Estoque.setInstancia(instanciaEstoque);

            instanciaEstoque._helper.realizarChamadaAjax("Produto/BuscarEstoque", data, "GET", null, instanciaEstoque._acaoSucesso);

        });

    }

    // Atualizar estoque
    popularModalAtualizar = function (instanciaEstoque) {

        $(".btn-editar-estoque").click(function () {
            var elemento = this.parentNode.parentNode;
            var elementoModalPai = elemento.parentNode.parentNode.parentNode.parentNode;
            
            //montar objeto produto
            var estoque = {
                Quantidade: instanciaEstoque._helper.obterTextoPorClasse(elemento, "quantidade-atualiza"),
                IdentificadorUnico: instanciaEstoque._helper.obterValorPorClasse(elemento, "identificador-unico-atualiza"),
                DataValidade: instanciaEstoque._helper.obterTextoPorClasse(elemento, "data-validade-atualiza"),
                ValorCompraUnidade: instanciaEstoque._helper.obterTextoPorClasse(elemento, "valor-compra-unitario-atualiza"), 
                ValorCompraTotal: instanciaEstoque._helper.obterTextoPorClasse(elemento, "valor-compra-total-atualiza"),
                NomeProduto: instanciaEstoque._helper.obterTextoPorId(elementoModalPai, "titulo-modal-consulta-estoque") 
            }

            var valorUnitario = instanciaEstoque._helper.formatarValorOutput(parseFloat(estoque.ValorCompraUnidade.replace('R$ ', '').replace(',', '.')));
            var valorTotal = instanciaEstoque._helper.formatarValorOutput(parseFloat(estoque.ValorCompraTotal.replace('R$ ', '').replace(',', '.')));
            var dataValidade = instanciaEstoque._helper.formatarDataOutput(estoque.DataValidade, "/", "-");

            // mostrar modal
            $("#modal-atualizar-estoque").modal("show");

            // preenche os inputs formatados com os valores
            $("#quantidade-atualiza").val(parseInt(estoque.Quantidade));
            $("#valor-compra-unitario-atualiza").val(valorUnitario);
            $("#valor-compra-total-atualiza ").val(valorTotal);
            $("#data-validade-atualiza").val(dataValidade);
            $("#span-estoque-identificador-unico-atualiza").html("<input id='estoque-identificador-unico' type='hidden' value='" + estoque.IdentificadorUnico + "'/>");
            
            instanciaEstoque._helper.mascaraValorMonetario($("#valor-compra-unitario-atualiza"));
            instanciaEstoque._helper.mascaraValorMonetario($("#valor-compra-total-atualiza "));

            instanciaEstoque.ajustarValorCompra(instanciaEstoque, "atualiza");

        });
    }

    atualizar = function (instanciaEstoque) {

        $("#btn-atualiza-estoque").click(function () {
            var elemento = this.parentNode.parentNode;

            // validar campos obrigatorios
            var arrayElementos = [$("#quantidade-atualiza"), $("#valor-compra-unitario-atualiza"), $("#valor-compra-total-atualiza"), $("#data-validade-atualiza")];
            var arraySpans = [$("#span-valida-quantidade"), $("#span-valida-valor-compra-unitario"), $("#span-valida-valor-compra-total"), $("#span-valida-data-validade")];

            if (!instanciaEstoque._helper.validarCamposObrigatorios(arrayElementos, arraySpans))
                return;

            // montar objetos request
            var estoque = {
                IdentificadorUnico: instanciaEstoque._helper.obterValorPorId(elemento, "estoque-identificador-unico"),
                Quantidade: instanciaEstoque._helper.obterValorPorId(elemento, "quantidade-atualiza"),
                DataValidade: instanciaEstoque._helper.obterValorPorId(elemento, "data-validade-atualiza"),
                ValorCompraUnidade: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-unitario-atualiza"),
                ValorCompraTotal: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-total-atualiza")
            }

            var data = {
                Estoque: estoque
            }

            console.log(data);

            // realizar requisição
            instanciaEstoque._helper.realizarChamadaAjax("Produto/AtualizarEstoque", data, "PUT", instanciaEstoque._helper);

        });
    }
    // Metodos publicos complementares
    ajustarValorCompra = function (instanciaEstoque, complementoId) {
        // autocompleta o valor de compra total com base no valor unitario
        $("#valor-compra-unitario-" + complementoId).blur(function () {

            var elemento = this.parentNode.parentNode;

            // monta objeto estoque
            var estoque = {
                Quantidade: instanciaEstoque._helper.obterValorPorId(elemento, "quantidade-" + complementoId),
                ValorCompraUnidade: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-unitario-" + complementoId),
                ValorCompraTotal: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-total-" + complementoId)
            }

            // validar campo quantidade antes do calculo
            if (!instanciaEstoque._helper.validarCamposObrigatorios([$("#quantidade-" + complementoId), $("#valor-compra-unitario-" + complementoId)], [$("#span-valida-quantidade"), $("#span-valida-valor-compra-unitario")]))
                return;

            // formata valor para calculo 
            var valorFormatado = instanciaEstoque._helper.formatarValorInput(estoque.ValorCompraUnidade);

            // calculo 
            var resultado = parseInt(estoque.Quantidade) * valorFormatado;

            // devolve valor calculado e formatado para tela
            $("#valor-compra-total-" + complementoId).val(instanciaEstoque._helper.formatarValorOutput(resultado));

        });

        // autocompleta o valor de compra unitario com base no valor total
        $("#valor-compra-total-" + complementoId).blur(function () {

            var elemento = this.parentNode.parentNode;

            // monta objeto estoque
            var estoque = {
                Quantidade: instanciaEstoque._helper.obterValorPorId(elemento, "quantidade-" + complementoId),
                ValorCompraUnidade: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-unitario-" + complementoId),
                ValorCompraTotal: instanciaEstoque._helper.obterValorPorId(elemento, "valor-compra-total-" + complementoId)
            }

            // validar campo quantidade antes do calculo
            if (!instanciaEstoque._helper.validarCamposObrigatorios([$("#quantidade-" + complementoId), $("#valor-compra-total-" + complementoId)], [$("#span-valida-quantidade"), $("#span-valida-valor-compra-total")]))
                return;

            // formata valor para calculo
            var valorFormatado = instanciaEstoque._helper.formatarValorInput(estoque.ValorCompraTotal);

            // calculo
            var resultado = valorFormatado / parseInt(estoque.Quantidade);

            // devolve valor calculado e formatado para tela
            $("#valor-compra-unitario-" + complementoId).val(instanciaEstoque._helper.formatarValorOutput(resultado));

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

    // metodos privados
    _acaoSucesso = function (response) {
        var html = "";
        var somaQuantidade = 0;
        var instanciaEstoque = Estoque.getInstancia();

        $("#modal-consultar-estoque").modal("show");

        $("#titulo-modal-consulta-estoque").text("Consulta estoque " + Estoque.getNome());

        response.forEach(function (produto) {

            html += "<tr>" +
                        "<td class='quantidade-atualiza'>" + produto.estoque.quantidade + "</td>" +
                        "<td class='valor-compra-unitario-atualiza'>R$ " + instanciaEstoque._helper.formatarValorOutput(produto.estoque.valorCompraUnidade) + "</td>" +
                        "<td class='valor-compra-total-atualiza'>R$ " + instanciaEstoque._helper.formatarValorOutput(produto.estoque.valorCompraTotal) + "</td>" +
                        "<td class='data-validade-atualiza'>" + instanciaEstoque._helper.formatarDataOutput(produto.estoque.dataValidade, "-", "/") + "</td>" +
                        "<td><button type='button' class='btn btn-primary btn-editar-estoque'>Editar</button></td>" +
                        "<td><button type='button' class='btn btn-danger btn-deletar-estoque'> Deletar</button></td>" +
                        "<td><input type = 'hidden' class='identificador-unico-atualiza' value = '" + produto.estoque.identificadorUnico + "' /></td>" +
                    "</tr>"                     

            somaQuantidade += parseInt(produto.estoque.quantidade);
                        
        });

        $("#quantidade-total").text(somaQuantidade);

        $("#render-lista-estoque").html(html);

        instanciaEstoque.popularModalAtualizar(instanciaEstoque);
    }
}