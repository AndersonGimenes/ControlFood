$(document).ready(function () {
    var produto = new Produto();
    produto.cadastrar(produto);
    produto.preencherCadastroEstoque(produto);
    produto.cadastrarEstoque(produto);
    produto.ajustarValorCompra(produto);
    produto.limparCampoInput();
    produto.deletar(produto);
    produto.editar(produto);
    produto.atualizar(produto);
});

class Produto {

    constructor() {
        this._helper = new ComumHelper();

        // input valor venda produto
        this._helper.mascaraValorMonetario($("#valor-venda"));

        // modal cadastro estoque
        // input valor compra unitario
        this._helper.mascaraValorMonetario($("#valor-compra-unitario"));

        // input valor compra total
        this._helper.mascaraValorMonetario($("#valor-compra-total"));
        
    }

    cadastrar = function (instanciaProduto) {

        $("#btn-cadastrar").click(function () {
            var elemento = this.parentNode;

            // validar campos obrigatorios
            var arrayElementos = [$("#nome"), $("#codigo-interno"), $("#valor-venda")];
            var arraySpans = [$("#span-valida-nome"), $("#span-valida-codigo-interno"), $("#span-valida-valor-venda")];

            if (!instanciaProduto._helper.validarCamposObrigatorios(arrayElementos, arraySpans))
                return;            

            // montar objetos request
            var subCategoria = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorId(elemento, "identificador-unico-sub-categoria-nova")
            }

            var estoque = {
                ValorCompraUnidade: instanciaProduto._helper.obterValorPorId(elemento, "valor-compra-unitario"),
                ValorCompraTotal: instanciaProduto._helper.obterValorPorId(elemento, "valor-compra-total"),
                DataValidade: instanciaProduto._helper.obterValorPorId(elemento, "data-validade"),
                Quantidade: instanciaProduto._helper.obterValorPorId(elemento, "quantidade")
            }

            var data = {
                CodigoInterno: instanciaProduto._helper.obterValorPorId(elemento, "codigo-interno"),
                Nome: instanciaProduto._helper.obterValorPorId(elemento, "nome"),
                ValorVenda: instanciaProduto._helper.obterValorPorId(elemento, "valor-venda"),
                SubCategoria: subCategoria,
                Estoque: estoque
            }

            // realizar requisição
            instanciaProduto._helper.realizarChamadaAjax("Produto/Cadastrar", data, "POST");
        });
    }

    preencherCadastroEstoque = function (instanciaProduto) {

        $(".btn-cadastro-estoque").click(function () {
            var elemento = this.parentNode.parentNode;

            //montar objeto produto
            var produto = {
                Nome: instanciaProduto._helper.obterTextoPorClasse(elemento, "nome"),
                IdentificadorUnico: instanciaProduto._helper.obterValorPorClasse(elemento, "identificador-unico")
            }

            // mostrar modal
            $("#modal-cadastro-estoque").modal("show");

            // preencher nome produto e identificador do produto
            $("#produto-nome").html("<input type='text' class='form-control' disabled='disabled' value='" + produto.Nome + "'/>");
            $("#span-produto-identificador-unico").html("<input id='produto-identificador-unico' type='hidden' value='" + produto.IdentificadorUnico + "'/>");
            
        });
    }

    cadastrarEstoque = function (instanciaProduto) {

        $("#btn-cadastrar-estoque").click(function () {
            var elemento = this.parentNode.parentNode;

            // validar campos obrigatorios
            var arrayElementos = [$("#quantidade"), $("#valor-compra-unitario"), $("#valor-compra-total"), $("#data-validade")];
            var arraySpans = [$("#span-valida-quantidade"), $("#span-valida-valor-compra-unitario"), $("#span-valida-valor-compra-total"), $("#span-valida-data-validade")];

            if (!instanciaProduto._helper.validarCamposObrigatorios(arrayElementos, arraySpans))
                return; 

            // montar objetos request
            var estoque = {
                Quantidade : instanciaProduto._helper.obterValorPorId(elemento, "quantidade"),
                DataValidade: instanciaProduto._helper.obterValorPorId(elemento, "data-validade"),
                ValorCompraUnidade: instanciaProduto._helper.obterValorPorId(elemento, "valor-compra-unitario"),
                ValorCompraTotal: instanciaProduto._helper.obterValorPorId(elemento, "valor-compra-total")
            }

            var data = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorId(elemento, "produto-identificador-unico"),
                Estoque: estoque
            }   

            // realizar requisição
            instanciaProduto._helper.realizarChamadaAjax("Produto/CadastrarEstoque", data, "POST");

        });
    }

    editar = function (instanciaProduto) {

        $(".btn-editar").click(function () {
            var elemento = this.parentNode.parentNode;

            //montar objeto produto
            var subCategoria = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorClasse(elemento, "identificador-unico-sub-categoria"),
                Tipo: instanciaProduto._helper.obterTextoPorClasse(elemento, "tipo-sub-categoria")
            }

            var produto = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorClasse(elemento, "identificador-unico"),
                CodigoInterno: instanciaProduto._helper.obterTextoPorClasse(elemento, "codigo-interno"),
                Nome: instanciaProduto._helper.obterTextoPorClasse(elemento, "nome"),
                ValorVenda: instanciaProduto._helper.obterTextoPorClasse(elemento, "valor-venda")
            }

            var valor = instanciaProduto._formatarValorOutput(parseFloat(produto.ValorVenda.replace('R$', '')));

            // mostrar modal
            $("#modal-atualizar").modal("show");

            // preencher nome produto e identificador do produto
            $("#span-sub-categoria-tipo").html("<input type='text' class='form-control' disabled='disabled' value='" + subCategoria.Tipo + "'/>");
            $("#span-produto-nome-atualizar").html("<input type='text' class='form-control nome' disabled='disabled' value='" + produto.Nome + "'/>");
            $("#span-produto-codigo").html("<input type='text' class='form-control codigo-interno' disabled='disabled' value='" + produto.CodigoInterno + "'/>");
            $("#span-valor-venda-atualizar").html("<span id='span-valida-valor-venda'></span><input type='text' class='form-control valor-venda' value='" + valor + "'/>");
            $("#span-identificador-unico").html("<input type='hidden' class='form-control identificador-unico' value='" + produto.IdentificadorUnico + "'/>");
            $("#span-identificador-unico-sub-categoria").html("<input type='hidden' class='form-control identificador-unico-sub-categoria' value='" + subCategoria.IdentificadorUnico + "'/>");

            // input valor venda produto
            instanciaProduto._helper.mascaraValorMonetario($(".valor-venda"));
        });
    }

    atualizar = function (instanciaProduto) {
        $("#btn-atualizar").click(function () {
            var elemento = this.parentNode.parentNode;

            console.log(elemento);

            var subCategoria = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorClasse(elemento, "identificador-unico-sub-categoria")
            }

            var data = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorClasse(elemento, "identificador-unico"),
                CodigoInterno: instanciaProduto._helper.obterValorPorClasse(elemento, "codigo-interno"),
                Nome: instanciaProduto._helper.obterValorPorClasse(elemento, "nome"),
                ValorVenda: instanciaProduto._helper.obterValorPorClasse(elemento, "valor-venda"),
                SubCategoria: subCategoria
            }

            if (!instanciaProduto._helper.validarCamposObrigatorios([$(elemento).find(".valor-venda")], $(elemento).find("#span-valida-valor-venda")))
                return;

            instanciaProduto._helper.realizarChamadaAjax("/Produto/Atualizar", data, "PUT");

        });
    }

    deletar = function (instanciaProduto) {
        $(".btn-deletar").click(function () {
            var elemento = this.parentNode.parentNode;

            var data = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorClasse(elemento, "identificador-unico"),
            }

            instanciaProduto._helper.realizarChamadaAjax("/Produto/Deletar", data, "DELETE");
        });
    }

    ajustarValorCompra = function (instanciaProduto) {
        // autocompleta o valor de compra total com base no valor unitario
        $("#valor-compra-unitario").blur(function () {

            var elemento = this.parentNode.parentNode;

            // monta objeto estoque
            var estoque = {
                Quantidade: instanciaProduto._helper.obterValorPorId(elemento, "quantidade"),
                ValorCompraUnidade: instanciaProduto._helper.obterValorPorId(elemento, "valor-compra-unitario"),
                ValorCompraTotal: instanciaProduto._helper.obterValorPorId(elemento, "valor-compra-total")
            }

            // validar campo quantidade antes do calculo
            if (!instanciaProduto._helper.validarCamposObrigatorios([$("#quantidade"), $("#valor-compra-unitario")], [$("#span-valida-quantidade"), $("#span-valida-valor-compra-unitario")]))
                return;

            // formata valor para calculo 
            var valorFormatado = instanciaProduto._formatarValorInput(estoque.ValorCompraUnidade);

            // calculo 
            var resultado = parseInt(estoque.Quantidade) * valorFormatado;

            // devolve valor calculado e formatado para tela
            $("#valor-compra-total").val(instanciaProduto._formatarValorOutput(resultado));

        });

        // autocompleta o valor de compra unitario com base no valor total
        $("#valor-compra-total").blur(function () {

            var elemento = this.parentNode.parentNode;

            // monta objeto estoque
            var estoque = {
                Quantidade: instanciaProduto._helper.obterValorPorId(elemento, "quantidade"),
                ValorCompraUnidade: instanciaProduto._helper.obterValorPorId(elemento, "valor-compra-unitario"),
                ValorCompraTotal: instanciaProduto._helper.obterValorPorId(elemento, "valor-compra-total")
            }

            // validar campo quantidade antes do calculo
            if (!instanciaProduto._helper.validarCamposObrigatorios([$("#quantidade"), $("#valor-compra-total")], [$("#span-valida-quantidade"), $("#span-valida-valor-compra-total")]))
                return;

            // formata valor para calculo
            var valorFormatado = instanciaProduto._formatarValorInput(estoque.ValorCompraTotal);

            // calculo
            var resultado = valorFormatado / parseInt(estoque.Quantidade);

            // devolve valor calculado e formatado para tela
            $("#valor-compra-unitario").val(instanciaProduto._formatarValorOutput(resultado));
            
        });
               
    }

    limparCampoInput = function () {
        $("#btn-close").click(function () {
            $("#quantidade").val('');
            $("#data-validade").val('');
            $("#valor-compra-unitario").val('');
            $("#valor-compra-total").val('');
        });
    }

    // metodos privados
    _formatarValorInput = function (valor) {
        var valorSemCaracteres = valor.replace(',', '').replace('.', '');

        var duasUltimasCasas = valorSemCaracteres.substring(valorSemCaracteres.length, valorSemCaracteres.length - 2);

        var valorInvertido = valorSemCaracteres.split('').reverse().join('');

        var demaisCasasInvertido = valorInvertido.substring(valorInvertido.length, 2);

        var demaisCasas = demaisCasasInvertido.split('').reverse().join('');

        return parseFloat(demaisCasas + '.' + duasUltimasCasas);
    }

    _formatarValorOutput = function (valor) {
        var valorSemCaracteres = valor.toFixed(2).toString().replace('.', '');

        if (valorSemCaracteres.length <= 5)
            return valor.toFixed(2).toString().replace('.', ',');

        var duasUltimasCasas = valorSemCaracteres.substring(valorSemCaracteres.length, valorSemCaracteres.length - 2);

        var valorInvertido = valorSemCaracteres.split('').reverse().join('');

        var demaisCasasInvertido = valorInvertido.substring(valorInvertido.length, 2);

        var concatSequencia = '';
        var concatSequenciaPonto = '';
        var concatResultado = '';

        demaisCasasInvertido.split('').forEach(function (numero) {
                                   
            concatSequencia += numero;
            
            if (concatSequencia.length == 3) {
                concatSequenciaPonto += concatSequencia + ".";
                concatSequencia = "";
            } 

            concatResultado = concatSequenciaPonto + concatSequencia;
        });

        var demaisCasas = concatResultado.split('').reverse().join('');

        return demaisCasas + ',' + duasUltimasCasas;
    }
}