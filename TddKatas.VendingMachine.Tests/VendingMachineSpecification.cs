namespace TddKatas.VendingMachine.Tests;

public class VendingMachineSpecification
{
    [Fact]
    public void SeleccionarProducto_CuandoNoHayInventario_Retorna_SOLD_OUT()
    {
        var maquina = new VendingMachine();

        var respuesta = maquina.SeleccionarProducto(Producto.Chips); 
        
        Assert.Equal(respuesta, new VendingMachineRespuesta("SOLD OUT"));
    }
    
    [Theory]
    [InlineData(Producto.Chips, "PRICE: $ 0.50")]
    [InlineData(Producto.Cola, "PRICE: $ 1.00")]
    [InlineData(Producto.Candy, "PRICE: $ 0.65")]
    public void SeleccionarProducto_CuandoHayInventario_Y_No_Hay_Dinero_Suficiente_Retorna_PRICE(Producto productoSolicitado, string displayEsperado)
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var maquina = new VendingMachine(inventarioInicial);

        var respuesta = maquina.SeleccionarProducto(productoSolicitado); 
        
        Assert.Equal(respuesta, new VendingMachineRespuesta(displayEsperado));
    }
    
    [Theory]
    [InlineData(Coin.Quarter, "CURRENT AMOUNT: $ 0.25")]
    [InlineData(Coin.Dime, "CURRENT AMOUNT: $ 0.10")]
    public void InsertarMoneda_CuandoEsValida_Retorna_CURRENT_AMOUNT(Coin monedaIngresada, string displayEspeado)
    {
        var maquina = new VendingMachine();

        var respuesta = maquina.InsertarMoneda(monedaIngresada); 
        
        Assert.Equal(respuesta, new VendingMachineRespuesta(displayEspeado));
    }
}

public enum Coin
{
    Quarter = 25,
    Dime = 10,
}

public class VendingMachine
{
    private readonly List<Producto> _inventarioInicial;

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
            return new VendingMachineRespuesta($"PRICE: $ {ObtenerPrecioDe(producto):F2}");

        return new VendingMachineRespuesta("SOLD OUT");
    }

    private static double ObtenerPrecioDe(Producto producto)
    {
        var listaDePrecios = new Dictionary<Producto, double>
        {
            { Producto.Chips, 0.5 },
            { Producto.Cola, 1 },
            { Producto.Candy, 0.65 }
        };
        
        return  listaDePrecios[producto];
    }

    public VendingMachineRespuesta InsertarMoneda(Coin monedaIngresada)
    {
        var valorMoneda = (int)monedaIngresada / 100.0;
        return new VendingMachineRespuesta($"CURRENT AMOUNT: $ {valorMoneda:F2}");
    }
}

public enum Producto
{
    Chips,
    Cola,
    Candy
}

public record VendingMachineRespuesta(string Display);