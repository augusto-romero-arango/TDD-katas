namespace TddKatas.VendingMachine;

public class VendingMachine
{
    private static readonly Dictionary<Producto, double> ListaDePrecios = new()
    {
        { Producto.Chips, 0.5 },
        { Producto.Cola, 1 },
        { Producto.Candy, 0.65 }
    };

    private readonly List<Producto> _inventarioInicial;
    private double _saldoIngresado = 0;

    public VendingMachine()
    {
        _inventarioInicial = [];
    }
    public VendingMachine(List<Producto> inventarioInicial)
    {
        _inventarioInicial = inventarioInicial;
    }

    public VendingMachineRespuesta SeleccionarProducto(Producto producto)
    {
        if (_inventarioInicial.Contains(producto))
            return new VendingMachineRespuesta($"PRICE: $ {ObtenerPrecioDe(producto):F2}", []);

        return new VendingMachineRespuesta("SOLD OUT", []);
    }

    private static double ObtenerPrecioDe(Producto producto)
    {
        return  ListaDePrecios[producto];
    }

    public VendingMachineRespuesta InsertarMoneda(Coin monedaIngresada)
    {
        _saldoIngresado += monedaIngresada.Valor();
        
        return new VendingMachineRespuesta($"CURRENT AMOUNT: $ {_saldoIngresado:F2}", []);
    }

    public VendingMachineRespuesta RetornarMonedas()
    {
        throw new NotImplementedException();
    }
}