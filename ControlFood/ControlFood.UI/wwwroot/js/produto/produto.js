$(document).ready(function () {
    var produto = new Produto();
    produto.checkIgrediente();
    produto.checkEstoque();
});

class Produto {
    checkIgrediente = function () {
        var helper = new ComumHelper();

        helper.checkHiddenOnOff("#especifico-igredientes", "#span-especifico-igredientes");
    }

    checkEstoque = function () {
        var helper = new ComumHelper();

        helper.checkHiddenOnOff("#especifico-estoque", "#span-especifico-estoque");
    }

}