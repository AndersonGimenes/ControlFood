class ComumHelper
{

    obterTextoPorClasse = function(elemento, classe)
    {
        return $(elemento).find("."+classe).text();
    }

    obterValorPorClasse = function(elemento, classe)
    {
        return $(elemento).find("."+classe).val();
    }

    ajaxDeletarItem = function (url, elemento) {
        $.ajax({
            url: url,
            type: 'DELETE',
            data: {
                IdentificadorUnico: this.obterValorPorClasse(elemento, "identificador-unico"),
                Tipo: this.obterTextoPorClasse(elemento, "tipo")
            },
            success: function () {
                window.location.reload();
                $("#tipo").val('');
            },
            error: function (XMLHttpRequest) {
                alert('Erro: ' + XMLHttpRequest.responseText)
                console.log(XMLHttpRequest.responseText);

            }
        });
    }
}