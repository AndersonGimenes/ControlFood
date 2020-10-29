$(document).ready(function () {
    var produto = new Produto();
    produto.cadastrar(produto);
    produto.preencherCadastroEstoque(produto);
    produto.cadastrarEstoque(produto);
    produto.ajustarValorCompra(produto);
});

class Produto {

    constructor() {
        this._helper = new ComumHelper();
    }

    cadastrar = function (instanciaProduto) {

        $("#btn-cadastrar").click(function () {
            var elemento = this.parentNode;

            var arrayElementos = [$("#nome"), $("#codigo-interno"), $("#valor-venda")];
            var arraySpans = [$("#span-valida-nome"), $("#span-valida-codigo-interno"), $("#span-valida-valor-venda")];

            if (!instanciaProduto._helper.validarCamposObrigatorios(arrayElementos, arraySpans))
                return;            

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

            instanciaProduto._helper.realizarChamadaAjax("Produto/Cadastrar", data, "POST");
        });
    }

    preencherCadastroEstoque = function (instanciaProduto) {
        $(".btn-cadastro-estoque").click(function () {
            var elementoTr = this.parentNode.parentNode;

            var produto = {
                Nome: instanciaProduto._helper.obterTextoPorClasse(elementoTr, "nome"),
                IdentificadorUnico: instanciaProduto._helper.obterValorPorClasse(elementoTr, "identificador-unico")
            }

            $("#modal-cadastro-estoque").modal("show");

            $("#produto-nome").html("<input type='text' class='form-control' disabled='disabled' value='" + produto.Nome + "'/>");
            $("#span-produto-identificador-unico").html("<input id='produto-identificador-unico' type='hidden' value='" + produto.IdentificadorUnico + "'/>");
            
        });
    }

    cadastrarEstoque = function (instanciaProduto) {
        $("#btn-cadastrar-estoque").click(function () {
            var elementoTr = this.parentNode.parentNode;

            var arrayElementos = [$("#quantidade"), $("#valor-compra-unitario"), $("#valor-compra-total"), $("#data-validade")];
            var arraySpans = [$("#span-valida-quantidade"), $("#span-valida-valor-compra-unitario"), $("#span-valida-valor-compra-total"), $("#span-valida-data-validade")];

            if (!instanciaProduto._helper.validarCamposObrigatorios(arrayElementos, arraySpans))
                return; 

            var valorUnidade = instanciaProduto._helper.obterValorPorId(elementoTr, "valor-compra-unitario");

            var estoque = {
                Quantidade : instanciaProduto._helper.obterValorPorId(elementoTr, "quantidade"),
                DataValidade: instanciaProduto._helper.obterValorPorId(elementoTr, "data-validade"),
                ValorCompraUnidade: valorUnidade,
                ValorCompraTotal: instanciaProduto._helper.obterValorPorId(elementoTr, "valor-compra-total")
            }

            var data = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorId(elementoTr, "produto-identificador-unico"),
                Estoque: estoque
            }   

            instanciaProduto._helper.realizarChamadaAjax("Produto/CadastrarEstoque", data, "POST");

        });
    }

    ajustarValorCompra = function (instanciaProduto) {
        $("#valor-compra-unitario").mask('#.##0,00', { reverse: true });
      
        // autocompleta o valor de compra total com base no valor unitario
        $("#valor-compra-unitario").blur(function () {
            var elementoTr = this.parentNode.parentNode;
           
            var estoque = {
                Quantidade: instanciaProduto._helper.obterValorPorId(elementoTr, "quantidade"),
                ValorCompraUnidade: instanciaProduto._helper.obterValorPorId(elementoTr, "valor-compra-unitario"),
                ValorCompraTotal: instanciaProduto._helper.obterValorPorId(elementoTr, "valor-compra-total")
            }

            var valorFormatado = instanciaProduto._formatarValorInput(estoque.ValorCompraUnidade);

            var resultado = parseInt(estoque.Quantidade) * valorFormatado;

            $("#valor-compra-total").val(instanciaProduto._formatarValorOutput(resultado));

        });

        $("#valor-compra-total").mask('#.##0,00', { reverse: true });

        // autocompleta o valor de compra unitario com base no valor total
        $("#valor-compra-total").blur(function () {
            var elementoTr = this.parentNode.parentNode;

            var estoque = {
                Quantidade: instanciaProduto._helper.obterValorPorId(elementoTr, "quantidade"),
                ValorCompraUnidade: instanciaProduto._helper.obterValorPorId(elementoTr, "valor-compra-unitario"),
                ValorCompraTotal: instanciaProduto._helper.obterValorPorId(elementoTr, "valor-compra-total")
            }

            var valorFormatado = instanciaProduto._formatarValorInput(estoque.ValorCompraTotal);

            var resultado = valorFormatado / parseInt(estoque.Quantidade);

            $("#valor-compra-unitario").val(instanciaProduto._formatarValorOutput(resultado));
            
        });
               
    }

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