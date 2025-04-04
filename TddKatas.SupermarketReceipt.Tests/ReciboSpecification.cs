﻿namespace TddKatas.SupermarketReceipt.Tests;

public class ReciboSpecification
{
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_totalizar_en_3000()
    {
        var recibo = new Recibo();

        recibo.Adicionar("Cepillo de dientes");

        Assert.Equal(@"Factura
Cepillo de dientes: $ 3.000
TOTAL A PAGAR: $ 3.000", recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_jabon_totalizar_en_2000()
    {
        var recibo = new Recibo();

        recibo.Adicionar("Jabón");

        Assert.Equal(@"Factura
Jabón: $ 2.000
TOTAL A PAGAR: $ 2.000", recibo.ToString());
    }

    [Fact]
    public void Debe_lanzar_excepcion_cuando_adiciono_un_producto_no_existente()
    {
        var recibo = new Recibo();

        Action accionAdicionar = () => recibo.Adicionar("Cerveza");
        
        var ex = Assert.Throws<ArgumentException>(accionAdicionar);
        Assert.Equal("El producto Cerveza no existe en el sistema.", ex.Message);
    }

    [Fact]
    public void Debe_lanzar_excepcion_cuando_adiciono_un_producto_es_vacio()
    {
        var recibo = new Recibo();

        Action accionAdicionar = () => recibo.Adicionar("");
        
        var ex = Assert.Throws<ArgumentException>(accionAdicionar);
        Assert.Equal("Debe ingresar un producto.", ex.Message);
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_y_un_jabon_totalizar_en_5000()
    {
        var recibo = new Recibo();

        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Jabón");

        Assert.Equal(@"Factura
Cepillo de dientes: $ 3.000
Jabón: $ 2.000
TOTAL A PAGAR: $ 5.000", recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_y_tiene_porcentaje_de_descuento()
    {
        Dictionary<string, decimal> descuentos = new()
        {
            {"Cepillo de dientes", 0.1m}
        };
        var recibo = new Recibo(descuentos);

        recibo.Adicionar("Cepillo de dientes");

        Assert.Equal(@"Factura
Cepillo de dientes: $ 3.000
Cepillo de dientes (10 %): -$ 300
TOTAL A PAGAR: $ 2.700", recibo.ToString());
    }
    
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_y_jabon_y_ambos_porcentaje_de_descuento()
    {
        Dictionary<string, decimal> descuentos = new()
        {
            {"Cepillo de dientes", 0.1m},
            {"Jabón", 0.2m},
        };
        var recibo = new Recibo(descuentos);

        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Jabón");

        Assert.Equal(@"Factura
Cepillo de dientes: $ 3.000
Jabón: $ 2.000
Cepillo de dientes (10 %): -$ 300
Jabón (20 %): -$ 400
TOTAL A PAGAR: $ 4.300", recibo.ToString());
    }
}