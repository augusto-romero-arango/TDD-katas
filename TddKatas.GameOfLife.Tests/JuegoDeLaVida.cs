namespace TddKatas.GameOfLife.Tests;

public class JuegoDeLaVida
{
    private bool[,] _celulas;
    private readonly int _longitudEnX;
    private readonly int _longitudEnY;

    public JuegoDeLaVida(bool[,] celulas)
    {
        _celulas = celulas;
        _longitudEnX = _celulas.GetLength(0);
        _longitudEnY = _celulas.GetLength(1);
    }

    public bool[,] NuevaGeneracion()
    {
        _celulas = CalcularNuevaGeneracion();
        return (bool[,])_celulas.Clone();
    }

    private bool[,] CalcularNuevaGeneracion()
    {
        bool[,] nuevaGeneracion = new bool[_longitudEnX, _longitudEnY];


        for (var x = 0; x < _longitudEnX; x++)
            for (var y = 0; y < _longitudEnY; y++)
                nuevaGeneracion[x, y] = DeterminarSiEstaVivaEnLaProximaGeneracion(x, y);

        return nuevaGeneracion;
    }

    private bool DeterminarSiEstaVivaEnLaProximaGeneracion(int x, int y)
    {
        int vecinasVivas = ContarVecinasVivas(x, y);

        return EstaVivaEnLaSiguienteGeneracion(x, y, vecinasVivas);
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
        return !HayInfraPoblacion(vecinasVivas) 
               && !HaySobrePoblacion(vecinasVivas);
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

    
}