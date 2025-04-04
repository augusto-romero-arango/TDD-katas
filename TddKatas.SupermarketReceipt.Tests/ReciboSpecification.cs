using System.Xml.Schema;

namespace TddKatas.SupermarketReceipt.Tests;

public class ReciboSpecification
{
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_totalizar_en_3000()
    {
        var recibo = new Recibo();

        recibo.Adicionar("Cepillo de dientes");

        Assert.Equal("""
                     Factura
                     Cepillo de dientes: $ 3.000
                     TOTAL A PAGAR: $ 3.000
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_dos_cepillo_totalizar_en_6000()
    {
        var recibo = new Recibo();

        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Cepillo de dientes");

        Assert.Equal("""
                     Factura
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     TOTAL A PAGAR: $ 6.000
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_jabon_totalizar_en_2000()
    {
        var recibo = new Recibo();

        recibo.Adicionar("Jabón");

        Assert.Equal("""
                     Factura
                     Jabón: $ 2.000
                     TOTAL A PAGAR: $ 2.000
                     """, recibo.ToString());
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

        Assert.Equal("""
                     Factura
                     Cepillo de dientes: $ 3.000
                     Jabón: $ 2.000
                     TOTAL A PAGAR: $ 5.000
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_y_tiene_porcentaje_de_descuento()
    {
 
        DescuentoPorPorcentaje[] descuentosPorcentaje = new[]
        {
            new DescuentoPorPorcentaje("Cepillo de dientes", 0.1m, TipoDescuento.Porcentaje)
        };
        var recibo = new Recibo(descuentosPorcentaje);

        recibo.Adicionar("Cepillo de dientes");

        Assert.Equal("""
                     Factura
                     Cepillo de dientes: $ 3.000
                     DESCUENTOS APLICADOS:
                     Cepillo de dientes (10 %): -$ 300
                     TOTAL A PAGAR: $ 2.700
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_un_cepillo_y_jabon_y_ambos_tienen_porcentaje_de_descuento()
    {

        DescuentoPorPorcentaje[] descuentosPorcentaje = new[]
        {
            new DescuentoPorPorcentaje("Cepillo de dientes", 0.1m, TipoDescuento.Porcentaje),
            new DescuentoPorPorcentaje("Jabón", 0.2m, TipoDescuento.Porcentaje)
        };
        
        var recibo = new Recibo(descuentosPorcentaje);

        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Jabón");

        var reciboGenerado = recibo.ToString();
        Assert.Equal("""
                     Factura
                     Cepillo de dientes: $ 3.000
                     Jabón: $ 2.000
                     DESCUENTOS APLICADOS:
                     Cepillo de dientes (10 %): -$ 300
                     Jabón (20 %): -$ 400
                     TOTAL A PAGAR: $ 4.300
                     """, reciboGenerado);
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_dos_cepillos_con_descuento_genera_descuento_por_cada_cepillo()
    {

        DescuentoPorPorcentaje[] descuentosPorcentaje = new[]
        {
            new DescuentoPorPorcentaje("Cepillo de dientes", 0.1m, TipoDescuento.Porcentaje)
        };
        var recibo = new Recibo(descuentosPorcentaje);

        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Cepillo de dientes");

        Assert.Equal("""
                     Factura
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     DESCUENTOS APLICADOS:
                     Cepillo de dientes (10 %): -$ 300
                     Cepillo de dientes (10 %): -$ 300
                     TOTAL A PAGAR: $ 5.400
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_dos_cepillos_y_hay_descuento_2X1_Paga_3000()
    {
        DescuentoPagaXLlevaY[] descuentosPagaXLlevaY = new[]
        {
            new DescuentoPagaXLlevaY("Cepillo de dientes", 2, 1, TipoDescuento.LlevaXPagaY)
        };
        
        var descuento2X1 = new Dictionary<string, (int UnidadesAComprar, int UnidadesGratis)>
        {
            {
                "Cepillo de dientes",
                (2, 1)
            }
        };

        var recibo = new Recibo(descuento2X1, descuentosPagaXLlevaY);

        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Cepillo de dientes");

        Assert.Equal("""
                     Factura
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     DESCUENTOS APLICADOS:
                     Cepillo de dientes (2X1): -$ 3.000
                     TOTAL A PAGAR: $ 3.000
                     """, recibo.ToString());
    }

    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_tres_cepillos_y_hay_descuento_2X1_regalan_1_y_Paga_6000()
    {
        var descuento2X1 = new Dictionary<string, (int UnidadesAComprar, int UnidadesGratis)>
        {
            {
                "Cepillo de dientes",
                (2, 1)
            }
        };
        var descuentosPagaXLlevaY = new[]
        {
            new DescuentoPagaXLlevaY("Cepillo de dientes", 2, 1, TipoDescuento.LlevaXPagaY)
        };

        var recibo = new Recibo(descuento2X1, descuentosPagaXLlevaY);

        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Cepillo de dientes");

        Assert.Equal("""
                     Factura
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     DESCUENTOS APLICADOS:
                     Cepillo de dientes (2X1): -$ 3.000
                     TOTAL A PAGAR: $ 6.000
                     """, recibo.ToString());
    }
    
    [Fact]
    public void Debe_emitir_un_recibo_cuando_adiciono_cuatro_cepillos_y_hay_descuento_2X1_regalan_2_y_Paga_6000()
    {
        var descuento2X1 = new Dictionary<string, (int UnidadesAComprar, int UnidadesGratis)>
        {
            {
                "Cepillo de dientes",
                (2, 1)
            }
        };
        var descuentosPagaXLlevaY = new[]
        {
            new DescuentoPagaXLlevaY("Cepillo de dientes", 2, 1, TipoDescuento.LlevaXPagaY)
        };

        var recibo = new Recibo(descuento2X1, descuentosPagaXLlevaY);

        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Cepillo de dientes");
        recibo.Adicionar("Cepillo de dientes");

        var reciboGeneado = recibo.ToString();
        Assert.Equal("""
                     Factura
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     Cepillo de dientes: $ 3.000
                     DESCUENTOS APLICADOS:
                     Cepillo de dientes (2X1): -$ 3.000
                     Cepillo de dientes (2X1): -$ 3.000
                     TOTAL A PAGAR: $ 6.000
                     """, reciboGeneado);
    }
}