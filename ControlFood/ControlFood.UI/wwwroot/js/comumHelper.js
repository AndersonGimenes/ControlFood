class ComumHelper {

    obterTextoPorClasse = function (elemento, classe) {
        return $(elemento).find("." + classe).text();
    }

    obterTextoPorId = function (elemento, classe) {
        return $(elemento).find("#" + classe).text();
    }

    obterValorPorClasse = function (elemento, classe) {
        return $(elemento).find("." + classe).val();
    }

    obterValorPorId = function (elemento, id) {
        return $(elemento).find("#" + id).val();
    }

    validarCamposObrigatorios = function (arrayElementos, arraySpan) {

        var retorno = true;
        var index = 0;

        arrayElementos.forEach(function (elemento) {

            var valor = $(elemento).val();
            var span = arraySpan[index];

            if ($.trim(valor) === "") {
                $(span).html("<p class='text-danger'>Campo obrigatório !<p/>");
                retorno = false;
            }
            else {
                $(span).html("");
            }

            index++;
        });

        return retorno;
    }

    realizarChamadaAjax = function (url, data, acao, instanciaComumHelper, acaoSucesso) {

        if (acaoSucesso == null)
            acaoSucesso = instanciaComumHelper._acaoSucessoDefault

        $.ajax({
            url: url,
            type: acao,
            data: data,
            success: acaoSucesso,

            error: function (XMLHttpRequest) {
                $.confirm({
                    title: 'Erro',
                    type: 'red',
                    content: XMLHttpRequest.responseText,
                    buttons: {
                        confirm: function () {
                            console.log(XMLHttpRequest.responseText);
                        }
                    }
                });
            }
        });
    }

    mascaraValorMonetario = function (element) {
        element.mask('#.##0,00', { reverse: true });
    }

    formatarValorInput = function (valor) {
        var valorSemCaracteres = valor.replace(',', '').replace('.', '');

        var duasUltimasCasas = valorSemCaracteres.substring(valorSemCaracteres.length, valorSemCaracteres.length - 2);

        var valorInvertido = valorSemCaracteres.split('').reverse().join('');

        var demaisCasasInvertido = valorInvertido.substring(valorInvertido.length, 2);

        var demaisCasas = demaisCasasInvertido.split('').reverse().join('');

        return parseFloat(demaisCasas + '.' + duasUltimasCasas);
    }

    formatarValorOutput = function (valor) {
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

    formatarDataOutput(data) {
        var arrayData = data.substring(0, 10).split('-');
        return arrayData[2] + "/" + arrayData[1] + "/" + arrayData[0];
    }

    //metodos privados
    _acaoSucessoDefault = function (response) {
        if (response.mensagem === '' || response.mensagem === undefined) {
            window.location.reload();
            return;
        }
        $.confirm({
            title: 'Sucesso',
            type: 'blue',
            content: response.mensagem,
            buttons: {
                confirm: function () {
                    window.location.reload();
                }
            }
        });
    }
}