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
}