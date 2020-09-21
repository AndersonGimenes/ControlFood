$(document).ready(function () {
    var subCategoria = new SubCategoria();
    subCategoria.deletar();
});

class SubCategoria {

    deletar = function () {
        $(".btn-deletar").click(function(){

            var elementoTr = this.parentNode.parentNode;

            $.ajax({
                url: "/SubCategoria/Deletar",
                type: 'DELETE',
                data: {
                    IdentificadorUnico: _obterId(elementoTr),
                    Tipo: _obterTipo(elementoTr)
                },
                success: function () {
                    window.location.reload();
                    $("#Tipo").val('');
                },
                error: function (XMLHttpRequest) {
                    alert('Erro: ' + XMLHttpRequest.responseText)
                    console.log(XMLHttpRequest.responseText);
                    
                }
            });
        });

        // Funções privadas
        function _obterTipo(elementoTr) {
            return elementoTr.firstElementChild.textContent;
        }

        function _obterId(elementoTr) {
            return elementoTr.lastElementChild.value;
        }
    }
}