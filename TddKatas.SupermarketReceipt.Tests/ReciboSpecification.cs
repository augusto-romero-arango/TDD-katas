using TddKatas.SupermarketReceipt.Descuentos;

namespace TddKatas.SupermarketReceipt.Tests;

public class ReciboSpecification
{
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_totalizar_en_3000()
    {
        var recibo = new Recibo();

        recibo.Adicionar("Cepillo de dientes");

        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     {Recibo.TextoTotalAPagar} $ 3.000
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_dos_cepillo_totalizar_en_6000()
    {
        var recibo = new Recibo();

        ComprarPorCantidades(recibo, 2, "Cepillo de dientes");
        
        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     {Recibo.TextoTotalAPagar} $ 6.000
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_jabon_totalizar_en_2000()
    {
        var recibo = new Recibo();

        recibo.Adicionar("Jabón");

        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Jabón: $ 2.000
                     {Recibo.TextoTotalAPagar} $ 2.000
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_lanzar_excepcion_cuando_adiciono_un_producto_no_existente()
    {
        var recibo = new Recibo();

        Action accionAdicionar = () => recibo
            .Adicionar("Cerveza");

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

        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     Jabón: $ 2.000
                     {Recibo.TextoTotalAPagar} $ 5.000
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_y_tiene_porcentaje_de_descuento()
    {
 
        IDescuento[] descuentosPorcentaje = {
            new DescuentoPorPorcentaje("Cepillo de dientes", 0.1m)
        };
        var recibo = new Recibo(descuentosPorcentaje);

        recibo.Adicionar("Cepillo de dientes");

        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     {Recibo.TextoDescuentosAplicados}
                     Cepillo de dientes (10 %): -$ 300
                     {Recibo.TextoTotalAPagar} $ 2.700
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_y_jabon_y_ambos_tienen_porcentaje_de_descuento()
    {

        IDescuento[] descuentosPorcentaje = {
            new DescuentoPorPorcentaje("Cepillo de dientes", 0.1m),
            new DescuentoPorPorcentaje("Jabón", 0.2m)
        };
        
        var recibo = new Recibo(descuentosPorcentaje);

        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Jabón");

        var reciboGenerado = recibo.ToString();
        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     Jabón: $ 2.000
                     {Recibo.TextoDescuentosAplicados}
                     Cepillo de dientes (10 %): -$ 300
                     Jabón (20 %): -$ 400
                     {Recibo.TextoTotalAPagar} $ 4.300
                     """, reciboGenerado);
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_dos_cepillos_con_descuento_genera_descuento_por_cada_cepillo()
    {

        IDescuento[] descuentosPorcentaje = {
            new DescuentoPorPorcentaje("Cepillo de dientes", 0.1m)
        };
        var recibo = new Recibo(descuentosPorcentaje);
        
        ComprarPorCantidades(recibo, 2, "Cepillo de dientes");
     
        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     {Recibo.TextoDescuentosAplicados}
                     Cepillo de dientes (10 %): -$ 300
                     Cepillo de dientes (10 %): -$ 300
                     {Recibo.TextoTotalAPagar} $ 5.400
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_dos_cepillos_y_hay_descuento_2X1_Paga_3000()
    {
        IDescuento[] descuentosPagaXLlevaY = {
            new DescuentoPagaXLlevaY("Cepillo de dientes", 2, 1)
        };

        var recibo = new Recibo(descuentosPagaXLlevaY);

        ComprarPorCantidades(recibo, 2, "Cepillo de dientes");

        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     {Recibo.TextoDescuentosAplicados}
                     Cepillo de dientes (2X1): -$ 3.000
                     {Recibo.TextoTotalAPagar} $ 3.000
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_tres_cepillos_y_hay_descuento_2X1_regalan_1_y_Paga_6000()
    {
        IDescuento[] descuentosPagaXLlevaY = {
            new DescuentoPagaXLlevaY("Cepillo de dientes", 2, 1)
        };

        var recibo = new Recibo(descuentosPagaXLlevaY);

        ComprarPorCantidades(recibo, 3, "Cepillo de dientes");

        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     {Recibo.TextoDescuentosAplicados}
                     Cepillo de dientes (2X1): -$ 3.000
                     {Recibo.TextoTotalAPagar} $ 6.000
                     """, recibo.ToString());
    }
    
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_cuatro_cepillos_y_hay_descuento_2X1_regalan_2_y_Paga_6000()
    {
 
        IDescuento[] descuentosPagaXLlevaY =
        [
            new DescuentoPagaXLlevaY("Cepillo de dientes", 2, 1)
        ];

        var recibo = new Recibo(descuentosPagaXLlevaY);

        ComprarPorCantidades(recibo, 4, "Cepillo de dientes");
 
        var reciboGeneado = recibo.ToString();
        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     {Recibo.TextoDescuentosAplicados}
                     Cepillo de dientes (2X1): -$ 3.000
                     Cepillo de dientes (2X1): -$ 3.000
                     {Recibo.TextoTotalAPagar} $ 6.000
                     """, reciboGeneado);
    }
    
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_tres_cepillos_y_hay_descuento_3X2_regalan_1_y_Paga_6000()
    {
 
        IDescuento[] descuentosPagaXLlevaY =
        [
            new DescuentoPagaXLlevaY("Cepillo de dientes", 3, 1)
        ];

        var recibo = new Recibo(descuentosPagaXLlevaY);

        ComprarPorCantidades(recibo, 3, "Cepillo de dientes");

        var reciboGeneado = recibo.ToString();
        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     {Recibo.TextoDescuentosAplicados}
                     Cepillo de dientes (3X2): -$ 3.000
                     {Recibo.TextoTotalAPagar} $ 6.000
                     """, reciboGeneado);
    }
    
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_cinco_cepillos_y_hay_descuento_5X3_regalan_2_y_Paga_9000()
    {
 
        IDescuento[] descuentosPagaXLlevaY =
        [
            new DescuentoPagaXLlevaY("Cepillo de dientes", 5, 2)
        ];

        var recibo = new Recibo(descuentosPagaXLlevaY);

        ComprarPorCantidades(recibo, 5, "Cepillo de dientes");

        var reciboGeneado = recibo.ToString();
        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     {Recibo.TextoDescuentosAplicados}
                     Cepillo de dientes (5X3): -$ 3.000
                     Cepillo de dientes (5X3): -$ 3.000
                     {Recibo.TextoTotalAPagar} $ 9.000
                     """, reciboGeneado);
    }
    
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_3_cepillos_y_hay_descuento_3_cepillos_por_7000()
    {
 
        IDescuento[] descuentos =
        [
            new DescuentoCompraXPorYDinero("Cepillo de dientes", 3, 7_000)
        ];

        var recibo = new Recibo(descuentos);

        ComprarPorCantidades(recibo, 3, "Cepillo de dientes");

        var reciboGeneado = recibo.ToString();
        Assert.Equal($"""
                     {Recibo.TextoEncabezadoRecibo}
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     {Recibo.TextoDescuentosAplicados}
                     Cepillo de dientes (Combo 3): -$ 2.000
                     {Recibo.TextoTotalAPagar} $ 7.000
                     """, reciboGeneado);
    }

    private static void ComprarPorCantidades(Recibo recibo, int cantidad, string producto)
    {
        for (var i = 0; i < cantidad; i++)
        {
            recibo.Adicionar(producto);
        }
        
    }
}

