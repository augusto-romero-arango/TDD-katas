﻿namespace TddKatas.VendingMachine;

public class VendingMachine(List<Producto>? inventarioInicial = null)
{
    private static readonly Dictionary<Producto, double> ListaDePrecios = new()
    {
        { Producto.Chips, 0.5 },
        { Producto.Cola, 1 },
        { Producto.Candy, 0.65 }
    };

    private readonly List<Producto> _inventarioInicial = inventarioInicial ?? [];
    private readonly List<Coin> _monedasInsertadas = [];

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
        _monedasInsertadas.Add(monedaIngresada);
        
        return new VendingMachineRespuesta($"CURRENT AMOUNT: $ {CalcularMontoIngresado():F2}", []);
    }

    private double CalcularMontoIngresado()
    {
        return _monedasInsertadas.Select(m => m.Valor()).Sum();
    }

    public VendingMachineRespuesta RetornarMonedas()
    {
        var monedasARetornar = _monedasInsertadas.ToArray();
        _monedasInsertadas.Clear();
        
        return new VendingMachineRespuesta("INSERT COIN", monedasARetornar);
    }
}