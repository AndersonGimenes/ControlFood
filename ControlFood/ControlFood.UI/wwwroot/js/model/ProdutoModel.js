class ProdutoModel {
    constructor() {
        this.subCategoria = new SubCategoriaModel();
        this.estoque = new EstoqueModel();
    }

    identificadorUnico;
    codigoInterno;
    nome;
    valorVenda;
}