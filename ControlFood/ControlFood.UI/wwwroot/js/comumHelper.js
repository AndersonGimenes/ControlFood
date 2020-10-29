class ComumHelper {

    obterTextoPorClasse = function (elemento, classe) {
        return $(elemento).find("." + classe).text();
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

    realizarChamadaAjax = function (url, data, acao) {
        $.ajax({
            url: url,
            type: acao,
            data: data,
            success: function (response) {
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
            },
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
}