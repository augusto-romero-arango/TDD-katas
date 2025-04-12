namespace TddKatas.VendingMachine;

public class Monedero(List<Coin>? inventarioInicialDeMonedas = null)
{
    private readonly List<Coin> _inventarioMonedas = inventarioInicialDeMonedas ?? [];
    private readonly List<Coin> _monedasInsertadas = [];

    public decimal CalcularValorIngresado()
    {
        return _monedasInsertadas.Totalizar();
    }

    public void IngresarMonedasAInventario(Coin monedaIngresada)
    {
        _monedasInsertadas.Add(monedaIngresada);
        _inventarioMonedas.Add(monedaIngresada);
    }

    public Coin[] RetornarMonedasRecienIngresadas()
    {
        _monedasInsertadas.ForEach(coin => _inventarioMonedas.Remove(coin));
        
        var monedasARetornar = _monedasInsertadas.ToArray();
        _monedasInsertadas.Clear();
        return monedasARetornar;
    }
    public bool TryDarVueltas(decimal totalIngresado, decimal precio, out Coin[] vueltas)
    {
        var diferencia = totalIngresado - precio;
        vueltas = CalcularVueltas(diferencia).ToArray();

        return vueltas.Totalizar() == diferencia;
    }
    
    private List<Coin> CalcularVueltas(decimal diferencia)
    {
        return _inventarioMonedas
            .ObtenerInferioresA(diferencia)
            .ObtenerHastaCompletar(diferencia);
    }
}