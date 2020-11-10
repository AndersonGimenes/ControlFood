$(document).ready(function () {
    var produto = new Produto();

    produto.cadastrar(produto);
    produto.deletar(produto);
    produto.popularModalAtualizar(produto);
    produto.atualizar(produto);
});

class Produto {

    constructor() {
        this._helper = new ComumHelper();

        // input valor venda produto
        this._helper.mascaraValorMonetario($("#valor-venda"));
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
            instanciaProduto._helper.realizarChamadaAjax("Produto/Cadastrar", data, "POST", instanciaProduto._helper);
        });
    }

    popularModalAtualizar = function (instanciaProduto) {

        $(".btn-editar").click(function () {
            var elemento = this.parentNode.parentNode;

            //montar objeto produto
            var subCategoria = {
                Tipo: instanciaProduto._helper.obterTextoPorClasse(elemento, "tipo-sub-categoria")
            }

            var produto = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorClasse(elemento, "identificador-unico"),
                CodigoInterno: instanciaProduto._helper.obterTextoPorClasse(elemento, "codigo-interno"),
                Nome: instanciaProduto._helper.obterTextoPorClasse(elemento, "nome"),
                ValorVenda: instanciaProduto._helper.obterTextoPorClasse(elemento, "valor-venda")
            }

            var valor = instanciaProduto._helper.formatarValorOutput(parseFloat(produto.ValorVenda.replace('R$', '').replace(',', '.')));

            // mostrar modal
            $("#modal-atualizar").modal("show");

            // preencher nome produto e identificador do produto
            $("#span-sub-categoria-tipo-atualizar").html("<input type='text' class='form-control' disabled='disabled' value='" + subCategoria.Tipo + "'/>");
            $("#span-produto-nome-atualizar").html("<input type='text' class='form-control nome' disabled='disabled' value='" + produto.Nome + "'/>");
            $("#span-produto-codigo-atualizar").html("<input type='text' class='form-control codigo-interno' disabled='disabled' value='" + produto.CodigoInterno + "'/>");
            $("#span-valor-venda-atualizar").html("<span id='span-valida-valor-venda'></span><input type='text' class='form-control valor-venda' value='" + valor + "'/>");
            $("#span-identificador-unico").html("<input type='hidden' class='form-control identificador-unico' value='" + produto.IdentificadorUnico + "'/>");
            
            // input valor venda produto
            instanciaProduto._helper.mascaraValorMonetario($(".valor-venda"));
        });
    }

    atualizar = function (instanciaProduto) {
        $("#btn-atualizar").click(function () {
            var elemento = this.parentNode.parentNode;

            console.log(elemento);

            var data = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorClasse(elemento, "identificador-unico"),
                CodigoInterno: instanciaProduto._helper.obterValorPorClasse(elemento, "codigo-interno"),
                Nome: instanciaProduto._helper.obterValorPorClasse(elemento, "nome"),
                ValorVenda: instanciaProduto._helper.obterValorPorClasse(elemento, "valor-venda")
            }

            if (!instanciaProduto._helper.validarCamposObrigatorios([$(elemento).find(".valor-venda")], $(elemento).find("#span-valida-valor-venda")))
                return;

            instanciaProduto._helper.realizarChamadaAjax("/Produto/Atualizar", data, "PUT", instanciaProduto._helper);

        });
    }

    deletar = function (instanciaProduto) {
        $(".btn-deletar").click(function () {
            var elemento = this.parentNode.parentNode;

            var data = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorClasse(elemento, "identificador-unico"),
            }

            instanciaProduto._helper.realizarChamadaAjax("/Produto/Deletar", data, "DELETE", instanciaProduto._helper);
        });
    }
}