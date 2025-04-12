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
    
    [Fact]
    public void SeleccionarProducto_CuandoHayInventario_Y_No_Hay_Dinero_Suficiente_Retorna_PRICE()
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips};
        var maquina = new VendingMachine(inventarioInicial);

        var respuesta = maquina.SelaccionarProducto(Producto.Chips); 
        
        Assert.Equal(respuesta, new VendingMachineRespuesta("PRICE: $ 0.50"));
    }
}

public class VendingMachine
{
    public VendingMachine()
    {
        
    }
    public VendingMachine(List<Producto> inventarioInicial)
    {
    }

    public VendingMachineRespuesta SelaccionarProducto(Producto producto)
    {
        return new VendingMachineRespuesta("SOLD OUT");
    }
}

public enum Producto
{
    Chips
}

public record VendingMachineRespuesta(string Display);