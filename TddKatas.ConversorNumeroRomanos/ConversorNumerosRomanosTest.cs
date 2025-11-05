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

    [Theory]
    [InlineData(21, "XXI")]
    [InlineData(24, "XXIV")]
    [InlineData(25, "XXV")]
    [InlineData(26, "XXVI")]
    [InlineData(29, "XXIX")]
    public void Si_estaentre21y29_es_XXconlasUnidades(int numero, string romanoEsperado)
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(numero);

        romano.Should().Be(romanoEsperado);
    }

    [Theory]
    [InlineData(30, "XXX")]
    [InlineData(31, "XXXI")]
    [InlineData(34, "XXXIV")]
    [InlineData(35, "XXXV")]
    [InlineData(36, "XXXVI")]
    [InlineData(39, "XXXIX")]
    public void Si_estaentre30y39_es_XXXconlasUnidades(int numero, string romanoEsperado)
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(numero);

        romano.Should().Be(romanoEsperado);
    }

    [Fact]
    public void Si_40_es_XL()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(40);

        romano.Should().Be("XL");
    }

    [Theory]
    [InlineData(41, "XLI")]
    [InlineData(44, "XLIV")]
    [InlineData(45, "XLV")]
    [InlineData(46, "XLVI")]
    [InlineData(49, "XLIX")]
    public void Si_estaentre41y49_es_XLconlasUnidades(int numero, string romanoEsperado)
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(numero);

        romano.Should().Be(romanoEsperado);
    }

    [Fact]
    public void Si_50_es_L()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(50);

        romano.Should().Be("L");
    }

    [Theory]
    [InlineData(51, "LI")]
    [InlineData(54, "LIV")]
    [InlineData(55, "LV")]
    [InlineData(56, "LVI")]
    [InlineData(59, "LIX")]
    public void Si_estaentre51y59_es_LconlasUnidades(int numero, string romanoEsperado)
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(numero);

        romano.Should().Be(romanoEsperado);
    }

    [Fact]
    public void Si_60_es_LX()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(60);

        romano.Should().Be("LX");
    }

    [Theory]
    [InlineData(61, "LXI")]
    [InlineData(64, "LXIV")]
    [InlineData(65, "LXV")]
    [InlineData(66, "LXVI")]
    [InlineData(69, "LXIX")]
    public void Si_estaentre61y69_es_LXXconlasUnidades(int numero, string romanoEsperado)
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(numero);

        romano.Should().Be(romanoEsperado);
    }

    [Fact]
    public void Si_70_es_LXX()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(70);

        romano.Should().Be("LXX");
    }

    [Theory]
    [InlineData(71, "LXXI")]
    [InlineData(74, "LXXIV")]
    [InlineData(75, "LXXV")]
    [InlineData(76, "LXXVI")]
    [InlineData(79, "LXXIX")]
    public void Si_estaentre71y79_es_LXXconlasUnidades(int numero, string romanoEsperado)
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(numero);

        romano.Should().Be(romanoEsperado);
    }

    [Fact]
    public void Si_80_es_LXXX()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(80);

        romano.Should().Be("LXXX");
    }

    [Theory]
    [InlineData(81, "LXXXI")]
    [InlineData(84, "LXXXIV")]
    [InlineData(85, "LXXXV")]
    [InlineData(86, "LXXXVI")]
    [InlineData(89, "LXXXIX")]
    public void Si_estaentre81y89_es_LXXXconlasUnidades(int numero, string romanoEsperado)
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(numero);

        romano.Should().Be(romanoEsperado);
    }

    [Fact]
    public void Si_90_es_XC()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(90);

        romano.Should().Be("XC");
    }

    [Theory]
    [InlineData(91, "XCI")]
    [InlineData(94, "XCIV")]
    [InlineData(95, "XCV")]
    [InlineData(96, "XCVI")]
    [InlineData(99, "XCIX")]
    public void Si_estaentre91y99_es_XCconlasUnidades(int numero, string romanoEsperado)
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(numero);

        romano.Should().Be(romanoEsperado);
    }

    [Fact]
    public void Si_100_es_C()
    {
        var decimalesARomanos = new ConversorDecimalARomanos();

        string romano = decimalesARomanos.Convertir(100);

        romano.Should().Be("C");
    }
}

public class ConversorDecimalARomanos
{
    public string Convertir(int numero)
    {
        return numero switch
        {
            > 0 and <= 9 => ConvertirUnidades(numero),
            >= 10 and <= 99 => ConvertirDecenas(numero),
            _ => "C"
        };
    }

    private static string ConvertirDecenas(int numero)
    {
        var decena = numero / 10 % 10;
        return decena switch
        {
            > 0 and <= 3 => new string('X', decena) + ConvertirUnidades(numero % 10),
            4 => "XL" + ConvertirUnidades(numero % 10),
            5 => "L" + ConvertirUnidades(numero % 10),
            >= 6 and <= 8 => "L" + new string('X', decena - 5) + ConvertirUnidades(numero % 10),
            9 => "XC" + ConvertirUnidades(numero % 10),
            _ => ""
        };
    }

    private static string ConvertirUnidades(int numero)
    {
        return numero switch
        {
            > 0 and <= 3 => new string('I', numero),
            4 => "IV",
            5 => "V",
            > 5 and <= 8 => "V" + new string('I', numero - 5),
            9 => "IX",
            _ => ""
        };
    }
}