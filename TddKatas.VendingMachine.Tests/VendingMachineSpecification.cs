namespace TddKatas.VendingMachine.Tests;

public class VendingMachineSpecification
{
    [Fact]
    public void SeleccionarProducto_CuandoNoHayInventario_Retorna_SOLD_OUT()
    {
        var maquina = new VendingMachine();

        var respuesta = maquina.SelaccionarProducto(Producto.Chips); 
        
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

        var respuesta = maquina.SelaccionarProducto(productoSolicitado); 
        
        Assert.Equal(respuesta, new VendingMachineRespuesta(displayEsperado));
    }
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

    public VendingMachineRespuesta SelaccionarProducto(Producto producto)
    {
        if(_inventarioInicial.Contains(producto))
            if(producto == Producto.Chips)
                return new VendingMachineRespuesta("PRICE: $ 0.50");
            else if(producto == Producto.Cola)
                return new VendingMachineRespuesta("PRICE: $ 1.00");
            else
                return new VendingMachineRespuesta("PRICE: $ 0.65");
        
        return new VendingMachineRespuesta("SOLD OUT");
    }
}

public enum Producto
{
    Chips,
    Cola,
    Candy
}

public record VendingMachineRespuesta(string Display);