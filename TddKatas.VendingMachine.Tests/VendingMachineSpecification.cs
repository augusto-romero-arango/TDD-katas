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
}

public class VendingMachine
{
    public VendingMachineRespuesta SelaccionarProducto(Producto producto)
    {
        throw new NotImplementedException();
    }
}

public enum Producto
{
    Chips
}

public record VendingMachineRespuesta(string Display);