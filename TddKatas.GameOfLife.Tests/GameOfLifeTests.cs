using AwesomeAssertions;

namespace TddKatas.GameOfLife.Tests;

public class GameOfLifeTests
{
    [Fact]
    public void UnaCelulaSola_MuerePorInfrapoblacion()
    {
        var celulas = new bool [5, 5];
        celulas[3, 3] = true;
        var gameOfLife = new JuegoDeLaVida(celulas);

        gameOfLife.NuevaGeneracion();

        gameOfLife.Universo().Should().BeEquivalentTo(new bool[5, 5]);
    }

    [Fact]
    public void UnaCelulaConUnVecino_MuerePorInfrapoblacion()
    {
        var celulas = new bool [5, 5];
        celulas[3, 3] = true;
        celulas[3, 4] = true;
        var gameOfLife = new JuegoDeLaVida(celulas);

        gameOfLife.NuevaGeneracion();

        gameOfLife.Universo().Should().BeEquivalentTo(new bool[5, 5]);
    }

    [Fact]
    public void UnaCelulaConDosVecinos_Vive()
    {
        var celulas = new bool [5, 5];
        celulas[3, 3] = true;
        celulas[2, 4] = true;
        celulas[4, 2] = true;
        var gameOfLife = new JuegoDeLaVida(celulas);

        gameOfLife.NuevaGeneracion();

        var generacionEsperada = new bool[5, 5];
        generacionEsperada[3, 3] = true;
        gameOfLife.Universo().Should().BeEquivalentTo(generacionEsperada);
    }

    [Fact]
    public void UnaCelulaConTresVecinos_Vive()
    {
        var celulas = new bool [5, 5];
        celulas[3, 3] = true;
        celulas[2, 4] = true;
        celulas[4, 2] = true;
        celulas[2, 2] = true;
        var gameOfLife = new JuegoDeLaVida(celulas);

        gameOfLife.NuevaGeneracion();

        var generacionEsperada = new bool[5, 5];
        generacionEsperada[3, 3] = true;
        generacionEsperada[2, 3] = true;
        generacionEsperada[3, 2] = true;
        gameOfLife.Universo().Should().BeEquivalentTo(generacionEsperada);
    }

    [Fact]
    public void UnaCelulaConCuatroVecinos_MuerePorSobrepoblacion()
    {
        var celulas = new bool [5, 5];
        celulas[3, 3] = true;
        celulas[3, 4] = true;
        celulas[4, 4] = true;
        celulas[4, 3] = true;
        celulas[2, 2] = true;
        var gameOfLife = new JuegoDeLaVida(celulas);

        gameOfLife.NuevaGeneracion();

        var generacionEsperada = new bool[5, 5];
        generacionEsperada[3, 4] = true;
        generacionEsperada[4, 4] = true;
        generacionEsperada[4, 3] = true;
        generacionEsperada[2, 3] = true;
        generacionEsperada[3, 2] = true;

        // [3,3] muere por sobrepoblacion
        // [2,2] muere por infrapoblacion

        gameOfLife.Universo().Should().BeEquivalentTo(generacionEsperada);
    }

    [Fact]
    public void UnaCelulaMuertaConTresVecinos_Revive()
    {
        var celulas = new bool [5, 5];
        celulas[3, 3] = true;
        celulas[2, 4] = true;
        celulas[4, 4] = true;
        celulas[4, 2] = true;
        celulas[2, 2] = true;
        var gameOfLife = new JuegoDeLaVida(celulas);

        gameOfLife.NuevaGeneracion();

        var generacionEsperada = new bool[5, 5];
        generacionEsperada[3, 4] = true;
        generacionEsperada[2, 3] = true;
        generacionEsperada[3, 2] = true;
        generacionEsperada[4, 3] = true;


        gameOfLife.Universo().Should().BeEquivalentTo(generacionEsperada);
    }
}

public class JuegoDeLaVida
{
    private bool[,] _celulas;

    public JuegoDeLaVida(bool[,] celulas)
    {
        _celulas = celulas;
    }

    public void NuevaGeneracion()
    {
        _celulas = CalcularNuevaGeneracion();
    }

    private bool[,] CalcularNuevaGeneracion()
    {
        var longitudEnX = _celulas.GetLength(0);
        var longitudEnY = _celulas.GetLength(1);

        bool[,] nuevaGeneracion = new bool[longitudEnX, longitudEnY];


        for (int x = 0; x < longitudEnX; x++)
        {
            for (int y = 0; y < longitudEnY; y++)
            {
                int vecinasVivas = ContarVecinasVivas(x, y);

                nuevaGeneracion[x, y] = EstaVivaEnLaSiguienteGeneracion(x, y, vecinasVivas);
            }
        }

        return nuevaGeneracion;
    }

    private bool EstaVivaEnLaSiguienteGeneracion(int x, int y, int vecinasVivas)
    {
        return EstaViva(x, y)
            ? Sobrevive(vecinasVivas)
            : Renace(vecinasVivas);
    }

    private bool EstaViva(int x, int y)
    {
        return _celulas[x, y];
    }

    private static bool Renace(int vecinasVivas)
    {
        return vecinasVivas == 3;
    }

    private static bool Sobrevive(int vecinasVivas)
    {
        return !HayInfraPoblacion(vecinasVivas) && !HaySobrePoblacion(vecinasVivas);
    }

    private int ContarVecinasVivas(int x, int y)
    {
        int vecinas = 0;

        var aLaDerecha = x + 1;
        var aLaIzquierda = x - 1;
        var arriba = y + 1;
        var debajo = y - 1;

        if (HayCelulaVivaEn(aLaDerecha, y)) vecinas++;
        if (HayCelulaVivaEn(aLaIzquierda, y)) vecinas++;
        if (HayCelulaVivaEn(x, arriba)) vecinas++;
        if (HayCelulaVivaEn(x, debajo)) vecinas++;
        if (HayCelulaVivaEn(aLaDerecha, arriba)) vecinas++;
        if (HayCelulaVivaEn(aLaDerecha, debajo)) vecinas++;
        if (HayCelulaVivaEn(aLaIzquierda, arriba)) vecinas++;
        if (HayCelulaVivaEn(aLaIzquierda, debajo)) vecinas++;

        return vecinas;
    }

    private static bool HaySobrePoblacion(int vecinas)
    {
        return vecinas > 3;
    }

    private static bool HayInfraPoblacion(int vecinas)
    {
        return vecinas < 2;
    }

    private bool HayCelulaVivaEn(int x, int y)
    {
        try
        {
            return _celulas[x, y];
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }

    public bool[,] Universo()
    {
        return _celulas;
    }
}