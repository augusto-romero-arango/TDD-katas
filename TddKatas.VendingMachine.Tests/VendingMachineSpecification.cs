namespace TddKatas.VendingMachine.Tests;

public class VendingMachineSpecification
{
    [Fact]
    public void SeleccionarProducto_CuandoNoHayInventario_Retorna_SOLD_OUT()
    {
        var maquina = new VendingMachine();

        var respuesta = maquina.SeleccionarProducto(Producto.Chips);

        Assert.Equal(respuesta, VendingMachineRespuesta.SoldOut());
    }

    [Theory]
    [InlineData(Producto.Chips, 0.5)]
    [InlineData(Producto.Cola, 1)]
    [InlineData(Producto.Candy, 0.65)]
    public void SeleccionarProducto_CuandoHayInventario_Y_No_Hay_Dinero_Suficiente_Retorna_PRICE(
        Producto productoSolicitado, decimal precioEsperado)
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var maquina = new VendingMachine(inventarioInicial);

        var respuesta = maquina.SeleccionarProducto(productoSolicitado);

        Assert.Equal(respuesta, VendingMachineRespuesta.Price(precioEsperado));
    }


    [Theory]
    [InlineData(Coin.Quarter)]
    [InlineData(Coin.Dime)]
    [InlineData(Coin.Nickel)]
    public void InsertarMoneda_CuandoEsValida_Retorna_CURRENT_AMOUNT(Coin monedaIngresada)
    {
        var maquina = new VendingMachine();

        var respuesta = maquina.InsertarMoneda(monedaIngresada);

        Assert.Equal(respuesta, VendingMachineRespuesta.CurrentAmount(monedaIngresada.Valor()));
    }

    [Fact]
    public void InsertarMoneda_CuandoEsInvalida_Retorna_INSERT_COIN_Y_Devuelve_Moneda()
    {
        var maquina = new VendingMachine();

        var respuesta = maquina.InsertarMoneda(Coin.Penny);
        
        Assert.Equivalent(respuesta, VendingMachineRespuesta.InvalidCoin(Coin.Penny));
    }

    [Fact]
    public void InsertarMoneda_Cuando_Acumula_Monedas_Retorna_CURRENT_AMOUNT_Totalizado()
    {
        var maquina = new VendingMachine();
        _ = maquina.InsertarMoneda(Coin.Quarter);

        var respuesta = maquina.InsertarMoneda(Coin.Dime);

        Assert.Equal(respuesta, VendingMachineRespuesta.CurrentAmount(0.35m));
    }

    [Fact]
    public void RetornarMonedas_Devuelve_Monedas_Ingresadas()
    {
        var maquina = new VendingMachine();
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Dime);

        var respuesta = maquina.RetornarMonedas();

        Assert.Equivalent(respuesta, VendingMachineRespuesta.InsertCoin([Coin.Quarter, Coin.Dime]));
    }

    [Fact]
    public void RetornarMonedas_Devuelve_Monedas_Y_Desocupa_Monedas_Ingresadas_De_VendingMachine()
    {
        var maquina = new VendingMachine();
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.RetornarMonedas();
        var respuestaInsertarMonedas = maquina.InsertarMoneda(Coin.Dime);

        var respuesta = maquina.RetornarMonedas();

        Assert.Equivalent(respuestaInsertarMonedas, VendingMachineRespuesta.CurrentAmount(0.10m));
        Assert.Equivalent(respuesta, VendingMachineRespuesta.InsertCoin([Coin.Dime]));
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

        Assert.Equal(respuesta,VendingMachineRespuesta.ThankYou(Producto.Cola, []));
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

        Assert.Equal(respuesta, VendingMachineRespuesta.SoldOut());
    }


    [Fact]
    public void
        SeleccionarProducto_Cuando_Dinero_Ingresado_Es_Mayor_Al_Precio_Y_No_Hay_Cambio_Retorna_EXACT_CHANGE_ONLY()
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var maquina = new VendingMachine(inventarioInicial);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);

        var respuesta = maquina.SeleccionarProducto(Producto.Candy);

        Assert.Equal(respuesta, VendingMachineRespuesta.ExactChangeOnly());
    }
    
    [Fact]
    public void
        SeleccionarProducto_Cuando_Dinero_Ingresado_Es_Mayor_Al_Precio_Y_No_Hay_Cambio_Exacto_Retorna_EXACT_CHANGE_ONLY()
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var maquina = new VendingMachine(inventarioInicial, [Coin.Nickel]);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);

        var respuesta = maquina.SeleccionarProducto(Producto.Candy);

        Assert.Equal(respuesta, VendingMachineRespuesta.ExactChangeOnly());
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

        Assert.Equivalent(respuesta, VendingMachineRespuesta.ThankYou(Producto.Candy, [Coin.Dime]));
    }


    [Theory]
    [InlineData(new[] {Coin.Nickel, Coin.Nickel, Coin.Dime}, new []{Coin.Dime})]
    [InlineData(new[] {Coin.Nickel, Coin.Nickel}, new []{Coin.Nickel, Coin.Nickel})]
    [InlineData(new[] {Coin.Nickel, Coin.Nickel, Coin.Quarter}, new []{Coin.Nickel, Coin.Nickel})]
    public void
        SeleccionarProducto_Cuando_Dinero_Ingresado_Es_Mayor_Al_Precio_Y_Hay_Cambio_Prefiere_Retorna_Venta_Con_Vueltas_De_Mayor_Denominacion(
            Coin[] saldoInicial, Coin[] monedasRetornadas)
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var maquina = new VendingMachine(inventarioInicial, saldoInicial.ToList());
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);

        var respuesta = maquina.SeleccionarProducto(Producto.Candy);

        Assert.Equivalent(respuesta, VendingMachineRespuesta.ThankYou(Producto.Candy, monedasRetornadas));
    }

    [Fact]
    public void SeleccionarProducto_Cunado_Ingresa_Monedas_De_Sobra_Devuelve_Producto_Y_Vueltas()
    {
        var inventarioInicial = new List<Producto>() {Producto.Chips, Producto.Cola, Producto.Candy};
        var maquina = new VendingMachine(inventarioInicial);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);
        _ = maquina.InsertarMoneda(Coin.Quarter);

        var respuesta = maquina.SeleccionarProducto(Producto.Chips);
        
        Assert.Equivalent(respuesta, VendingMachineRespuesta.ThankYou(Producto.Chips, [Coin.Quarter, Coin.Quarter]));
    }
    
    
}