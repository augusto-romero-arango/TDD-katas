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
            return VendingMachineRespuesta.SoldOut();

        var precio = ObtenerPrecioDe(producto);
        var totalIngresado = CalcularMontoIngresado();
        
        if (totalIngresado < precio)
            return VendingMachineRespuesta.Price(precio);

        if (totalIngresado == precio)
            return DispensarProducto(producto, []);

        var diferencia = totalIngresado - precio;
        
        var vueltas = CalcularCambio(diferencia);

        if (vueltas.Count == 0)
            return VendingMachineRespuesta.ExactChangeOnly();

        return DispensarProducto(producto, vueltas.ToArray());
    }

    private VendingMachineRespuesta DispensarProducto(Producto producto, Coin[] monedasRetornadas)
    {
        _inventarioInicial.Remove(producto);
        return VendingMachineRespuesta.ThankYou(producto, monedasRetornadas);
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
        return VendingMachineRespuesta.CurrentAmount(CalcularMontoIngresado());
    }

    private decimal CalcularMontoIngresado()
    {
        return _monedasInsertadas.Totalizar();
    }

    public VendingMachineRespuesta RetornarMonedas()
    {
        var monedasARetornar = _monedasInsertadas.ToArray();
        _monedasInsertadas.Clear();
        
        return VendingMachineRespuesta.InsertCoin(monedasARetornar);
    }
}