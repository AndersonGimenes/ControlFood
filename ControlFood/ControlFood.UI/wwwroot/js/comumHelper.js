class ComumHelper
{

    obterTipo = function(elementoTr)
    {
        return elementoTr.firstElementChild.textContent;
    }

    obterId = function(elementoTr)
    {
        return elementoTr.lastElementChild.value;
    }
}