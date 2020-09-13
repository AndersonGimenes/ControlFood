﻿$(document).ready(function () {
    var categoria = new Categoria();
    categoria.deletar();
});

class Categoria {

    deletar = function () {
        $(".btn-deletar").click(function(){

            var elementoTr = this.parentNode.parentNode;

            $.ajax({
            url: "/Categoria/Deletar",
            type: 'post',
            data: {
                IdentificadorUnico: _obterId(elementoTr),
                Tipo: _obterTipo(elementoTr)
            }
            })
            .done(function (data) {
                window.location.reload();
                $("#Tipo").val('');
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