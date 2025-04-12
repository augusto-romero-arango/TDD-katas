namespace TddKatas.VendingMachine;

public class VendingMachine(List<Producto>? inventarioInicial = null, List<Coin> inventarioInicialMonedas = null)
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

        if (CalcularMontoIngresado() < ObtenerPrecioDe(producto))
            return new VendingMachineRespuesta($"PRICE: $ {ObtenerPrecioDe(producto):F2}", [], null);

        if (CalcularMontoIngresado() == ObtenerPrecioDe(producto))
        {
            _inventarioInicial.Remove(producto);
            return new VendingMachineRespuesta("THANK YOU", [], producto);
        }

        var diferencia = CalcularMontoIngresado() - ObtenerPrecioDe(producto);
        
        var vueltas = CalcularCambio(diferencia);

        if (vueltas.Count == 0)
            return new VendingMachineRespuesta("EXACT CHANGE ONLY", [], null);

        return new VendingMachineRespuesta("THANK YOU", vueltas.ToArray(), producto);
    }

    private List<Coin> CalcularCambio(decimal diferencia)
    {
        var monedasParaVueltas = _inventarioMonedas
            .Where(m => diferencia % m.Valor()  == 0)
            .OrderByDescending(m => m.Valor())
            .ToArray();
        
        var vueltas = new List<Coin>();

        foreach (var moneda in monedasParaVueltas)
        {
            vueltas.Add(moneda);
            diferencia -= moneda.Valor();
            
            if (diferencia == 0)
                break;
        }

        return vueltas;
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
        return _monedasInsertadas.Select(m => m.Valor()).Sum();
    }

    public VendingMachineRespuesta RetornarMonedas()
    {
        var monedasARetornar = _monedasInsertadas.ToArray();
        _monedasInsertadas.Clear();

        return new VendingMachineRespuesta("INSERT COIN", monedasARetornar, null);
    }
}