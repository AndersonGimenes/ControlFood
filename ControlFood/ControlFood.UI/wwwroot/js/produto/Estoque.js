class Estoque {

    constructor() {

        // input valor compra unitario
        $('#valor-compra-unitario-cadastro').mask('#.##0,00', { reverse: true });

        // input valor compra total
        $('#valor-compra-total-cadastro').mask('#.##0,00', { reverse: true });
    }

    cadastrar(el) {

        let elemento = el.parentNode.parentNode;

        let quantidade = $(elemento).find('#quantidade');
        let dataValidade = $(elemento).find('#data-validade');
        let valorCompraUnitario = $(elemento).find('#valor-compra-unitario');
        let valorCompraTotal = $(elemento).find('#valor-compra-total');

        let arrayElementos = [quantidade, valorCompraUnitario, valorCompraTotal, dataValidade];
        let arraySpans = [$("#span-valida-quantidade"), $("#span-valida-valor-compra-unitario"), $("#span-valida-valor-compra-total"), $("#span-valida-data-validade")];

        if (!Helper.validarCamposObrigatorios(arrayElementos, arraySpans))
            return;

        produto.produtoModel.identificadorUnico = $(elemento).find('#produto-identificador-unico').val();
        produto.produtoModel.estoque.quantidade = quantidade.val();
        produto.produtoModel.estoque.dataValidade = dataValidade.val();
        produto.produtoModel.estoque.valorCompraUnidade = valorCompraUnitario.val();
        produto.produtoModel.estoque.valorCompraTotal = valorCompraTotal.val();



        Helper.realizarChamadaAjax("Produto/CadastrarEstoque", produto.produtoModel, "POST");
    }

    consultar(el) {

        let elemento = el.parentNode.parentNode;

        produto.produtoModel.estoque.identificadorUnico = $(elemento).find('.identificador-unico').val();
        EstoqueModel.nomeProduto = $(elemento).find('.nome').text();

        Helper.realizarChamadaAjax('Produto/BuscarEstoque', produto.produtoModel.estoque, 'GET', this._acaoSucesso);
    }

    atualizar(el) {

        let elemento = el.parentNode.parentNode;

        let quantidade = $(elemento).find('#estoque-quantidade-modal');
        let dataValidade = $(elemento).find('#estoque-data-validade-modal');
        let valorCompraUnitario = $(elemento).find('#estoque-valor-compra-unitario-modal');
        let valorCompraTotal = $(elemento).find('#estoque-valor-compra-total-modal');

        let arrayElementos = [quantidade, valorCompraUnitario, valorCompraTotal, dataValidade];
        let arraySpans = [$("#span-valida-quantidade-modal"), $("#span-valida-valor-compra-unitario-modal"), $("#span-valida-valor-compra-total-modal"), $("#span-valida-data-validade-modal")];

        if (!Helper.validarCamposObrigatorios(arrayElementos, arraySpans))
            return;

        produto.produtoModel.estoque.identificadorUnico = $(elemento).find('#estoque-identificador-unico').val();
        produto.produtoModel.estoque.quantidade = quantidade.val();
        produto.produtoModel.estoque.dataValidade = dataValidade.val();
        produto.produtoModel.estoque.valorCompraUnidade = valorCompraUnitario.val();
        produto.produtoModel.estoque.valorCompraTotal = valorCompraTotal.val();

        Helper.realizarChamadaAjax('Produto/AtualizarEstoque', produto.produtoModel, 'PUT');
    }

    deletar(el) {

        let elemento = el.parentNode.parentNode;

        produto.produtoModel.estoque.identificadorUnico = $(elemento).find('.identificador-unico').val();

        Helper.realizarChamadaAjax('/Produto/DeletarEstoque', produto.produtoModel, 'DELETE');
    
    }

    popularModalCadastro(el) {

        let elemento = el.parentNode.parentNode;

        $('#modal-cadastro-estoque').modal('show');

        $('#estoque-produto-nome-modal').html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find('.nome').text()}'/>`);
        $('#produto-identificador-unico-modal').html(`<input id='produto-identificador-unico' type='hidden' value='${$(elemento).find('.identificador-unico').val()}'/>`);

        this._ajustarValorCompra($(elemento).find('#valor-compra-unitario'), $(elemento).find('#valor-compra-total'), $(elemento).find('#quantidade'));

    }

    popularModalAtualizar(el) {

        let elemento = el.parentNode.parentNode;

        let valorUnitario = Helper.formatarValorOutput($(elemento).find('.valor-compra-unitario').text());
        let valorTotal = Helper.formatarValorOutput($(elemento).find('.valor-compra-total').text());
        let dataValidade = Helper.formatarDataOutput($(elemento).find('.data-validade').text(), '/', '-');

        // mostrar modal
        $('#modal-atualizar-estoque').modal('show');

        // preenche os inputs formatados com os valores 
        $('#estoque-produto-nome-modal').text(EstoqueModel.nomeProduto)
        $('#estoque-quantidade-modal').val(parseInt($(elemento).find('.quantidade').text()));
        $('#estoque-valor-compra-unitario-modal').val(valorUnitario);
        $('#estoque-valor-compra-total-modal').val(valorTotal);
        $('#estoque-data-validade-modal').val(dataValidade);
        $('#estoque-identificador-unico-modal').html(`<input id='estoque-identificador-unico' type='hidden' value='${$(elemento).find('.identificador-unico').val()}'/>`);

        $('#estoque-valor-compra-unitario-modal').mask('#.##0,00', { reverse: true });
        $('#estoque-valor-compra-total-modal').mask('#.##0,00', { reverse: true });

        this._ajustarValorCompra($('#estoque-valor-compra-unitario-modal'), $('#estoque-valor-compra-total-modal'), $('#estoque-quantidade-modal'));
    }

    limparCampoInput = function () {
        $("#btn-close").click(function () {
            $("#quantidade-cadastro").val('');
            $("#data-validade-cadastro").val('');
            $("#valor-compra-unitario-cadastro").val('');
            $("#valor-compra-total-cadastro").val('');
        });
    }

    // metodos privados
    _acaoSucesso(response) {

        $('#modal-consultar-estoque').modal('show');

        $('#titulo-modal-consulta-estoque').text(`Consulta estoque ${EstoqueModel.nome}`);

        // monta html modal consultar estoque
        let html = response.map(produto =>
            `<tr>
                <td class='quantidade'> ${produto.estoque.quantidade}</td>
                <td class='valor-compra-unitario'>R$ ${produto.estoque.valorCompraUnidade.toFixed(2).toString().replace('.', ',')}</td>
                <td class='valor-compra-total'>R$ ${produto.estoque.valorCompraTotal.toFixed(2).toString().replace('.', ',')}</td>
                <td class='data-validade'>${Helper.formatarDataOutput(produto.estoque.dataValidade, '-', '/')}</td>
                <td><button type='button' class='btn btn-primary' onclick='produto.estoque.popularModalAtualizar(this);'>Editar</button></td>
                <td><button type='button' class='btn btn-danger' onclick='produto.estoque.deletar(this);'> Deletar</button></td>
                <td><input type = 'hidden' class='identificador-unico' value='${produto.estoque.identificadorUnico}'/></td>
            </tr>`
        ).join('');

        $('#render-lista-estoque').html(html);

        let somaQuantidade = response.reduce((total, produto) => total += produto.estoque.quantidade, 0);
        $('#quantidade-total').text(somaQuantidade);

    }

    _ajustarValorCompra(valorUnitario, valorTotal, quantidade) {

        // autocompleta o valor de compra total com base no valor unitario
        valorUnitario.blur(() => {

            let valorFormatado = valorUnitario.val().replace('.', '').replace(',', '.');

            var resultado = parseInt(quantidade.val()) * valorFormatado;

            valorTotal.val(resultado.toFixed(2).toString().replace('.', ','));

        });

        // autocompleta o valor de compra unitario com base no valor total
        valorTotal.blur(() => {

            let valorFormatado = valorTotal.val().replace('.', '').replace(',', '.');

            let resultado = valorFormatado / parseInt(quantidade.val());

            valorUnitario.val(resultado.toFixed(2).toString().replace('.', ','));
        });
    }
}