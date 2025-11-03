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

        var nuevaGeneracion = gameOfLife.NuevaGeneracion();

        nuevaGeneracion.Should().BeEquivalentTo(new bool[5, 5]);
    }

    [Fact]
    public void UnaCelulaConUnVecino_MuerePorInfrapoblacion()
    {
        var celulas = new bool [5, 5];
        celulas[3, 3] = true;
        celulas[3, 4] = true;
        var gameOfLife = new JuegoDeLaVida(celulas);

        var nuevaGeneracion = gameOfLife.NuevaGeneracion();

        nuevaGeneracion.Should().BeEquivalentTo(new bool[5, 5]);
    }

    [Fact]
    public void UnaCelulaConDosVecinos_Vive()
    {
        var celulas = new bool [5, 5];
        celulas[3, 3] = true;
        celulas[2, 4] = true;
        celulas[4, 2] = true;
        var gameOfLife = new JuegoDeLaVida(celulas);

        var nuevaGeneracion = gameOfLife.NuevaGeneracion();

        var generacionEsperada = new bool[5, 5];
        generacionEsperada[3, 3] = true;
        
        nuevaGeneracion.Should().BeEquivalentTo(generacionEsperada);
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

        var nuevaGeneracion = gameOfLife.NuevaGeneracion();

        var generacionEsperada = new bool[5, 5];
        generacionEsperada[3, 3] = true;
        generacionEsperada[2, 3] = true;
        generacionEsperada[3, 2] = true;
        
        nuevaGeneracion.Should().BeEquivalentTo(generacionEsperada);
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

        var nuevaGeneracion = gameOfLife.NuevaGeneracion();

        var generacionEsperada = new bool[5, 5];
        generacionEsperada[3, 4] = true;
        generacionEsperada[4, 4] = true;
        generacionEsperada[4, 3] = true;
        generacionEsperada[2, 3] = true;
        generacionEsperada[3, 2] = true;

        // [3,3] muere por sobrepoblacion
        // [2,2] muere por infrapoblacion

        nuevaGeneracion.Should().BeEquivalentTo(generacionEsperada);
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

        var nuevaGeneracion = gameOfLife.NuevaGeneracion();

        var generacionEsperada = new bool[5, 5];
        generacionEsperada[3, 4] = true;
        generacionEsperada[2, 3] = true;
        generacionEsperada[3, 2] = true;
        generacionEsperada[4, 3] = true;


        nuevaGeneracion.Should().BeEquivalentTo(generacionEsperada);
    }
}