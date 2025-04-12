namespace TddKatas.VendingMachine;

public class VendingMachine(List<Producto>? inventarioInicial = null, List<Coin>? inventarioInicialMonedas = null)
{
    private static readonly Dictionary<Producto, decimal> ListaDePrecios = new()
    {
        {Producto.Chips, 0.5m},
        {Producto.Cola, 1m},
        {Producto.Candy, 0.65m}
    };

    private readonly List<Producto> _inventarioInicial = inventarioInicial ?? [];
    private readonly List<Coin> _inventarioMonedas = inventarioInicialMonedas ?? [];
    private readonly List<Coin> _monedasInsertadas = [];

    public VendingMachineRespuesta SeleccionarProducto(Producto producto)
    {
        
        if (_inventarioInicial.Contains(producto) == false)
            return new VendingMachineRespuesta("SOLD OUT", [], null);

        var precio = ObtenerPrecioDe(producto);
        var totalIngresado = CalcularMontoIngresado();
        
        if (totalIngresado < precio)
            return new VendingMachineRespuesta($"PRICE: $ {precio:F2}", [], null);

        if (totalIngresado == precio)
        {
            _inventarioInicial.Remove(producto);
            return new VendingMachineRespuesta("THANK YOU", [], producto);
        }

        var diferencia = totalIngresado - precio;
        
        var vueltas = CalcularCambio(diferencia);

        if (vueltas.Count == 0)
            return new VendingMachineRespuesta("EXACT CHANGE ONLY", [], null);

        return new VendingMachineRespuesta("THANK YOU", vueltas.ToArray(), producto);
    }

    private List<Coin> CalcularCambio(decimal diferencia)
    {
        return _inventarioMonedas
            .InferioresA(diferencia)
            .ObtenerHastaCompletar(diferencia);
    }

    private static decimal ObtenerPrecioDe(Producto producto)
    {
        return ListaDePrecios[producto];
    }

    public VendingMachineRespuesta InsertarMoneda(Coin monedaIngresada)
    {
        _monedasInsertadas.Add(monedaIngresada);

        return new VendingMachineRespuesta($"CURRENT AMOUNT: $ {CalcularMontoIngresado():F2}", [], null);
    }

    private decimal CalcularMontoIngresado()
    {
        return _monedasInsertadas.Totalizar();
    }

    public VendingMachineRespuesta RetornarMonedas()
    {
        var monedasARetornar = _monedasInsertadas.ToArray();
        _monedasInsertadas.Clear();

        return new VendingMachineRespuesta("INSERT COIN", monedasARetornar, null);
    }
}