using ElinsData.Extensions;

namespace ElinsData.Data;

public class ImpedancePoint : IImpedancePoint
{
    private ImpedancePoint() { }

    /// <summary>Частота измерения, Гц.</summary>
    public double Frequency { get; set; }

    /// <summary>Реальная часть импеданса, Ом.</summary>
    public double ImpedanceReal { get; set; }

    /// <summary>Мнимая часть импеданса, Ом.</summary>
    public double ImpedanceImaginary { get; set; }

    /// <summary>Потенциал, В.</summary>
    public double Potential { get; set; }

    /// <summary>Ёмкость, Ф/м2</summary>
    public double Capacitance { get; set; }

    public int Step { get; set; }

    public static IImpedancePoint Create(ReadOnlySpan<char> line, int step)
    {
        return new ImpedancePoint
        {
            Frequency = line.ReadDouble(),
            ImpedanceReal = line.ReadDouble(),
            ImpedanceImaginary = line.ReadDouble(),
            Step = step
        };
    }
}
