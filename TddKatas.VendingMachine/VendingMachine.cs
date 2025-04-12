namespace TddKatas.VendingMachine;

public class VendingMachine(List<Producto>? inventarioInicial = null, List<Coin> inventarioInicialMonedas = null)
{
    private static readonly Dictionary<Producto, double> ListaDePrecios = new()
    {
        {Producto.Chips, 0.5},
        {Producto.Cola, 1},
        {Producto.Candy, 0.65}
    };

    private readonly List<Producto> _inventarioInicial = inventarioInicial ?? [];
    private readonly List<Coin> _inventarioMonedas = inventarioInicialMonedas ?? [];
    private readonly List<Coin> _monedasInsertadas = [];

    public VendingMachineRespuesta SeleccionarProducto(Producto producto)
    {
        if (_inventarioInicial.Contains(producto) == false)
            return new VendingMachineRespuesta("SOLD OUT", [], null);

        if (CalcularMontoIngresado() < ObtenerPrecioDe(producto))
            return new VendingMachineRespuesta($"PRICE: $ {ObtenerPrecioDe(producto):F2}", [], null);

        if (CalcularMontoIngresado() == ObtenerPrecioDe(producto))
        {
            _inventarioInicial.Remove(producto);
            return new VendingMachineRespuesta("THANK YOU", [], producto);
        }

        var diferencia = CalcularMontoIngresado() - CalcularMontoIngresado();
        var vueltas = _inventarioMonedas
            .Where(m => m.Valor() == diferencia)
            .ToArray();

        if (vueltas.Length == 0)
            return new VendingMachineRespuesta("EXACT CHANGE ONLY", [], null);

        return new VendingMachineRespuesta("THANK YOU", vueltas, producto);
    }

    private static double ObtenerPrecioDe(Producto producto)
    {
        return ListaDePrecios[producto];
    }

    public VendingMachineRespuesta InsertarMoneda(Coin monedaIngresada)
    {
        _monedasInsertadas.Add(monedaIngresada);

        return new VendingMachineRespuesta($"CURRENT AMOUNT: $ {CalcularMontoIngresado():F2}", [], null);
    }

    private double CalcularMontoIngresado()
    {
        return _monedasInsertadas.Select(m => m.Valor()).Sum();
    }

    public VendingMachineRespuesta RetornarMonedas()
    {
        var monedasARetornar = _monedasInsertadas.ToArray();
        _monedasInsertadas.Clear();

        return new VendingMachineRespuesta("INSERT COIN", monedasARetornar, null);
    }
}