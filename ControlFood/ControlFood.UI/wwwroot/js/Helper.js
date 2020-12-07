class Helper {

    static validarCamposObrigatorios(arrayElementos, arraySpan) {

        let retorno = true;
        let index = 0;

        arrayElementos.forEach(function (elemento) {

            let valor = $(elemento).val();
            let span = arraySpan[index];

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

    static realizarChamadaAjax(url, data, acao, acaoSucesso) {

        if (acaoSucesso == null)
            acaoSucesso = Helper._acaoSucessoDefault;

        $.ajax({
            url: url,
            type: acao,
            data: data,
            success: acaoSucesso,

            error: (XMLHttpRequest) => {
                $.confirm({
                    title: 'Erro',
                    type: 'red',
                    content: XMLHttpRequest.responseText,
                    buttons: {
                        confirm: () => {
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

    formatarDataOutput(data, separadorSplit, separadorData ) {
        var arrayData = data.substring(0, 10).split(separadorSplit);
        return arrayData[2] + separadorData + arrayData[1] + separadorData + arrayData[0];
    }

    //metodos privados
    static _acaoSucessoDefault(response) {
        // inclusao de novo cadastro cai no if
        if (response.mensagem === '' || response.mensagem === undefined) {
            window.location.reload();
            return;
        }

        $.confirm({
            title: 'Sucesso',
            type: 'blue',
            content: response.mensagem,
            buttons: {
                confirm: () => window.location.reload()
            }
        });
    }
}