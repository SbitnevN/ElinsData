using ElinsData.Reader;

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

    /// <summary>Ёмкость, Ф/м2</summary>
    public double Capacitance { get; set; }

    /// <summary>Потенциал, В.</summary>
    public IStepPotential Potential { get; set; } = null!;

    public int Step
    {
        get => Potential.Step;
        set => Potential.Step = value;
    }

    public static IImpedancePoint Create(ReadOnlySpan<char> line, ElinsRecord data)
    {
        return new ImpedancePoint
        {
            Frequency = line.ReadDouble(),
            ImpedanceReal = line.ReadDouble(),
            ImpedanceImaginary = line.ReadDouble(),
            Potential = data.StepPotentials.Last()
        };
    }
}
