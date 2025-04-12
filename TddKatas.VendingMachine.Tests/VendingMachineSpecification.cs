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
    [InlineData(Coin.Nickel, "CURRENT AMOUNT: $ 0.05")]
    public void InsertarMoneda_CuandoEsValida_Retorna_CURRENT_AMOUNT(Coin monedaIngresada, string displayEspeado)
    {
        var maquina = new VendingMachine();

        var respuesta = maquina.InsertarMoneda(monedaIngresada); 
        
        Assert.Equal(respuesta, new VendingMachineRespuesta(displayEspeado));
    }

    [Fact]
    public void InsertarMoneda_Cuando_Acumula_Monedas_Retorna_CURRENT_AMOUNT_Totalizado()
    {
        var maquina = new VendingMachine();
        _ = maquina.InsertarMoneda(Coin.Quarter);
        
        var respuesta = maquina.InsertarMoneda(Coin.Dime);
        
        Assert.Equal(respuesta, new VendingMachineRespuesta("CURRENT AMOUNT: $ 0.35"));
    }
}