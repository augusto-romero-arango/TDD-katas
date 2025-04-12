namespace TddKatas.VendingMachine;

internal class Monedero(List<Coin>? inventarioInicialDeMonedas = null)
{
    private readonly List<Coin> _inventarioMonedas = inventarioInicialDeMonedas ?? [];
    private readonly List<Coin> _monedasInsertadas = [];

    public decimal CalcularValorInsertado()
    {
        return _monedasInsertadas.Totalizar();
    }

    public void InsertarMonedasAInventario(Coin monedaIngresada)
    {
        _monedasInsertadas.Add(monedaIngresada);
        _inventarioMonedas.Add(monedaIngresada);
    }

    public Coin[] RetornarMonedasRecienIngresadas()
    {
        QuitarMonedasRecienInsertadasDelInventario();

        return VaciarMonedasRecienInsertadas();
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

    private Coin[] VaciarMonedasRecienInsertadas()
    {
        var monedasARetornar = _monedasInsertadas.ToArray();
        _monedasInsertadas.Clear();
        
        return monedasARetornar;
    }

    private void QuitarMonedasRecienInsertadasDelInventario()
    {
        _monedasInsertadas.ForEach(coin => _inventarioMonedas.Remove(coin));
    }

    
}