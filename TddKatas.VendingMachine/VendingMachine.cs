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
        var precio = ObtenerPrecioDe(producto);
        var totalIngresado = CalcularValorIngresado();
        var tieneProducto = _inventarioInicial.Contains(producto);
        var puedeDarVueltas = TryDarVueltas(totalIngresado, precio, out var vueltas);

        return (tieneProducto, totalIngresado, puedeDarVueltas) switch
        {
            (tieneProducto: false, _, _) => VendingMachineRespuesta.SoldOut(),
            (tieneProducto: true, var valorIngresado, _) when valorIngresado < precio => VendingMachineRespuesta.Price(precio),
            (tieneProducto: true, var valorIngresado, _) when valorIngresado == precio => DispensarProducto(producto, []),
            (tieneProducto: true, _, puedeDarVueltas: true) => DispensarProducto(producto, vueltas),
            _ => VendingMachineRespuesta.ExactChangeOnly()
        };
    }

    public VendingMachineRespuesta InsertarMoneda(Coin monedaIngresada)
    {
        _monedasInsertadas.Add(monedaIngresada);
        _inventarioMonedas.Add(monedaIngresada);
        
        return VendingMachineRespuesta.CurrentAmount(CalcularValorIngresado());
    }

    public VendingMachineRespuesta RetornarMonedas()
    {
        _monedasInsertadas.ForEach(coin => _inventarioMonedas.Remove(coin));
        
        var monedasARetornar = _monedasInsertadas.ToArray();
        _monedasInsertadas.Clear();
        
        return VendingMachineRespuesta.InsertCoin(monedasARetornar);
    }

    private bool TryDarVueltas(decimal totalIngresado, decimal precio, out Coin[] vueltas)
    {
        var diferencia = totalIngresado - precio;
        vueltas = CalcularVueltas(diferencia).ToArray();

        return vueltas.Totalizar() == diferencia;
    }

    private VendingMachineRespuesta DispensarProducto(Producto producto, Coin[] monedasRetornadas)
    {
        _inventarioInicial.Remove(producto);
        return VendingMachineRespuesta.ThankYou(producto, monedasRetornadas);
    }


    
    private List<Coin> CalcularVueltas(decimal diferencia)
    {
        return _inventarioMonedas
            .ObtenerInferioresA(diferencia)
            .ObtenerHastaCompletar(diferencia);
    }

    private static decimal ObtenerPrecioDe(Producto producto)
    {
        return ListaDePrecios[producto];
    }
    private decimal CalcularValorIngresado()
    {
        return _monedasInsertadas.Totalizar();
    }
}