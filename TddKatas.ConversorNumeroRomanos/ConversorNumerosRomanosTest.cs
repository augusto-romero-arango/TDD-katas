using AwesomeAssertions;

namespace TddKatas.ConversorNumeroRomanos;

public class ConversorNumerosRomanosTest
{
    [Fact]
    public void Si_1_es_I()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(1);

        romano.Should().Be("I");
    }

    [Fact]
    public void Si_es_2_es_II()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(2);

        romano.Should().Be("II");
    }

    [Fact]
    public void Si_es_3_es_III()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(3);

        romano.Should().Be("III");
    }

    [Fact]
    public void Si_es_4_es_IV()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(4);

        romano.Should().Be("IV");
    }

    [Fact]
    public void Si_es_5_es_V()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(5);

        romano.Should().Be("V");
    }

    [Fact]
    public void Si_es_6_es_VI()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(6);

        romano.Should().Be("VI");
    }

    [Fact]
    public void Si_es_7_es_VII()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(7);

        romano.Should().Be("VII");
    }

    [Fact]
    public void Si_es_8_es_VIII()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(8);

        romano.Should().Be("VIII");
    }

    [Fact]
    public void Si_es_9_es_IX()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(9);

        romano.Should().Be("IX");
    }

    [Fact]
    public void Si_es_10_es_X()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(10);

        romano.Should().Be("X");
    }

    [Fact]
    public void Si_es_11_es_XI()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(11);

        romano.Should().Be("XI");
    }

    [Fact]
    public void Si_es_12_es_XII()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(12);

        romano.Should().Be("XII");
    }

    [Fact]
    public void Si_es_13_es_XIII()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(13);

        romano.Should().Be("XIII");
    }

    [Fact]
    public void Si_es_14_es_XIV()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(14);

        romano.Should().Be("XIV");
    }
    
    [Fact]
    public void Si_es_15_es_XV()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(15);

        romano.Should().Be("XV");
    }
    
    [Theory]
    [InlineData(16, "XVI")]
    [InlineData(17, "XVII")]
    [InlineData(18, "XVIII")]
    public void Si_es_16_17_18(int numero, string romanoEsperado)
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(numero);

        romano.Should().Be(romanoEsperado);
    }
    
    [Fact]
    public void Si_es_19_es_XIX()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(19);

        romano.Should().Be("XIX");
    }
    
    [Fact]
    public void Si_es_20_es_XX()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(20);

        romano.Should().Be("XX");
    }
    
}

public class ConversorDecimalARomanos
{
    public string Convertir(int numero)
    {
        string romano;
        
     
        if (numero is > 0 and <= 9)
        {
            string romano1;
            if (numero is > 0 and <= 3)
                romano1 = ConvertirFinalizadosDe1A3(numero, "");
            else if (numero == 4)
                romano1 = "IV";
            else if (numero == 5)
                romano1 = "V";
            else if(numero is > 5 and <= 8)
                romano1 = ConvertirFinalizadosDe1A3(numero, "V");
            else 
                romano1 =  "IX";
        
            romano = ""+romano1;
        }
        else if (numero == 10)
            romano = "X";
        else if (numero is >10 and <= 19)
        {
            string romano1;
            if (numero is > 10 and <= 13)
                romano1 = ConvertirFinalizadosDe1A3(numero, "X");
            else if (numero == 14)
                romano1 = "XIV";
            else if (numero == 15)
                romano1 = "XV";
            else if(numero is > 15 and <= 18)
                romano1 = ConvertirFinalizadosDe1A3(numero, "XV");
            else 
                romano1 =  "XIX";
        
            romano = romano1;
        }

        else
            return "XX";

        return romano;
    }

    

    private static string ConvertirFinalizadosDe1A3(int numero, string romanoAnterior)
    {
        int diferencia;
        if (romanoAnterior == "")
            diferencia = 0;
        else if (romanoAnterior == "V")
            diferencia = 5;
        else if(romanoAnterior == "X")
            diferencia = 10;
        else
            diferencia = 15;
        
        return romanoAnterior + new string('I', numero - diferencia);
    }
}