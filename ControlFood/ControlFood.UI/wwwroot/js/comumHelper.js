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

    validarCamposObrigatorios = function () {
        var valor = $("#tipo").val();

        if ($.trim(valor) === "") {
            $("#span-tipo").html("<p class='text-danger'>Campo obrigatório !<p/>");
            return false;
        }

        return true;
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
}