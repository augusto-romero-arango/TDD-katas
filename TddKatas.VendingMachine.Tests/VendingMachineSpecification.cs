namespace TddKatas.VendingMachine.Tests;

public class VendingMachineSpecification
{
    [Fact]
    public void SeleccionarProducto_CuandoNoHayInventario_Retorna_SOLD_OUT()
    {
        var maquina = new VendingMachine();

        var respuesta = maquina.SeleccionarProducto(Producto.Chips);

        Assert.Equal(respuesta, new VendingMachineRespuesta("SOLD OUT", [], null));
    }

    [Theory]
    [InlineData(Producto.Chips, "PRICE: $ 0.50")]
    [InlineData(Producto.Cola, "PRICE: $ 1.00")]
    [InlineData(Producto.Candy, "PRICE: $ 0.65")]
    public void SeleccionarProducto_CuandoHayInventario_Y_No_Hay_Dinero_Suficiente_Retorna_PRICE(
        Producto productoSolicitado, string displayEsperado)
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var maquina = new VendingMachine(inventarioInicial);

        var respuesta = maquina.SeleccionarProducto(productoSolicitado);

        Assert.Equal(respuesta, new VendingMachineRespuesta(displayEsperado, [], null));
    }
    
    

    [Theory]
    [InlineData(Coin.Quarter, "CURRENT AMOUNT: $ 0.25")]
    [InlineData(Coin.Dime, "CURRENT AMOUNT: $ 0.10")]
    [InlineData(Coin.Nickel, "CURRENT AMOUNT: $ 0.05")]
    public void InsertarMoneda_CuandoEsValida_Retorna_CURRENT_AMOUNT(Coin monedaIngresada, string displayEspeado)
    {
        var maquina = new VendingMachine();

        var respuesta = maquina.InsertarMoneda(monedaIngresada);

        Assert.Equal(respuesta, new VendingMachineRespuesta(displayEspeado, [], null));
    }

    [Fact]
    public void InsertarMoneda_Cuando_Acumula_Monedas_Retorna_CURRENT_AMOUNT_Totalizado()
    {
        var maquina = new VendingMachine();
        _ = maquina.InsertarMoneda(Coin.Quarter);

        var respuesta = maquina.InsertarMoneda(Coin.Dime);

        Assert.Equal(respuesta, new VendingMachineRespuesta("CURRENT AMOUNT: $ 0.35", [], null));
    }

    [Fact]
    public void RetornarMonedas_Devuelve_Monedas_Ingresadas()
    {
        var maquina = new VendingMachine();
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Dime);

        var respuesta = maquina.RetornarMonedas();

        Assert.Equivalent(respuesta, new VendingMachineRespuesta("INSERT COIN", [Coin.Quarter, Coin.Dime], null));
    }
    
    [Fact]
    public void RetornarMonedas_Devuelve_Monedas_Y_Desocupa_Monedas_Ingresadas_De_VendingMachine()
    {
        var maquina = new VendingMachine();
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.RetornarMonedas();
        var respuestaInsertarMonedas = maquina.InsertarMoneda(Coin.Dime);

        var respuesta = maquina.RetornarMonedas();

        Assert.Equivalent(respuestaInsertarMonedas, new VendingMachineRespuesta("CURRENT AMOUNT: $ 0.10", [], null));
        Assert.Equivalent(respuesta, new VendingMachineRespuesta("INSERT COIN", [Coin.Dime], null));
    }
    
    [Fact]
    public void SeleccionarProducto_CuandoHayInventario_Y_Hay_Dinero_Exacto_Retorna_Producto_Y_THANK_YOU()
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var maquina = new VendingMachine(inventarioInicial);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);

        var respuesta = maquina.SeleccionarProducto(Producto.Cola);

        Assert.Equal(respuesta, new VendingMachineRespuesta("THANK YOU", [], Producto.Cola));
    }
    
    [Fact]
    public void SeleccionarProducto_CuandoHayUnaVenta_DescuentaInventario()
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var maquina = new VendingMachine(inventarioInicial);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        // Compra la última unidad de Cola
        _ = maquina.SeleccionarProducto(Producto.Cola);
        
        var respuesta = maquina.SeleccionarProducto(Producto.Cola);

        Assert.Equal(respuesta, new VendingMachineRespuesta("SOLD OUT", [], null));
    }
    
  
    [Fact]
    public void SeleccionarProducto_Cuando_Dinero_Ingresado_Es_Mayor_Al_Precio_Y_No_Hay_Cambio_Retorna_EXACT_CHANGE_ONLY()
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var maquina = new VendingMachine(inventarioInicial);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        
        var respuesta = maquina.SeleccionarProducto(Producto.Candy);

        Assert.Equal(respuesta, new VendingMachineRespuesta("EXACT CHANGE ONLY", [], null));
    }
    
    [Fact]
    public void SeleccionarProducto_Cuando_Dinero_Ingresado_Es_Mayor_Al_Precio_Y_Hay_Cambio_Retorna_Venta_Con_Vueltas()
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var inventarioInicialMonedas = new List<Coin>() {Coin.Dime};    
        var maquina = new VendingMachine(inventarioInicial, inventarioInicialMonedas);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        
        var respuesta = maquina.SeleccionarProducto(Producto.Candy);

        Assert.Equivalent(respuesta, new VendingMachineRespuesta("THANK YOU", [Coin.Dime], Producto.Candy));
    }
    
    [Fact]
    public void SeleccionarProducto_Cuando_Dinero_Ingresado_Es_Mayor_Al_Precio_Y_Hay_Cambio_Con_Dos_Monedas_Retorna_Venta_Con_Vueltas()
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var inventarioInicialMonedas = new List<Coin>() {Coin.Nickel, Coin.Nickel};    
        var maquina = new VendingMachine(inventarioInicial, inventarioInicialMonedas);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        
        var respuesta = maquina.SeleccionarProducto(Producto.Candy);
    
        Assert.Equivalent(respuesta, new VendingMachineRespuesta("THANK YOU", [Coin.Nickel, Coin.Nickel], Producto.Candy));
    }
    
    
    
}