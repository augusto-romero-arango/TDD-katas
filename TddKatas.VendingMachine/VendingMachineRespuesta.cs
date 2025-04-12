namespace TddKatas.VendingMachine;

public record VendingMachineRespuesta
{
    public string Display { get; init; }
    public Coin[] MonedasRetornadas { get; init; }
    public Producto? ProductoEntregado { get; init; }
    private VendingMachineRespuesta(string display, Coin[] monedasRetornadas, Producto? productoEntregado)
    {
        Display = display;
        MonedasRetornadas = monedasRetornadas;
        ProductoEntregado = productoEntregado;
    }

    public static VendingMachineRespuesta SoldOut()
    {
        return new VendingMachineRespuesta("SOLD OUT", [], null);
    }
    
    public static VendingMachineRespuesta ThankYou( Producto producto, Coin[] monedasRetornadas)
    {
        return new VendingMachineRespuesta("THANK YOU", monedasRetornadas, producto);
    }

    public static VendingMachineRespuesta Price(decimal precio)
    {
        return new VendingMachineRespuesta($"PRICE: $ {precio:F2}", [], null);
    }

    public static VendingMachineRespuesta ExactChangeOnly()
    {
        return new VendingMachineRespuesta("EXACT CHANGE ONLY", [], null);
    }
    
    public static VendingMachineRespuesta CurrentAmount(decimal monto)
    {
        return new VendingMachineRespuesta($"CURRENT AMOUNT: $ {monto:F2}", [], null);
    }
    
    public static VendingMachineRespuesta InsertCoin(Coin[] monedasARetornar)
    {
        return new VendingMachineRespuesta("INSERT COIN", monedasARetornar, null);
    }

    public static VendingMachineRespuesta InvalidCoin(Coin moneda)
    {
        return new VendingMachineRespuesta("INSERT COIN", [moneda], null);
    }

}