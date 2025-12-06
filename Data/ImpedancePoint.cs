namespace ElinsDataParser.Data;

public class ImpedancePoint
{
    private double? _capacitance;

    /// <summary>Частота измерения, Гц.</summary>
    public double Frequency { get; set; }

    /// <summary>Реальная часть импеданса, Ом.</summary>
    public double ImpedanceReal { get; set; }

    /// <summary>Мнимая часть импеданса, Ом.</summary>
    public double ImpedanceImaginary { get; set; }

    /// <summary>Потенциал, В.</summary>
    public double Potential { get; set; }

    /// <summary>Ёмкость, Ф/м2</summary>
    public double Capacitance => _capacitance ??= CalculateCapacitance();

    private double CalculateCapacitance()
    {
        return -1.0d / (CalculateAngularFrequency() * ImpedanceImaginary);
    }

    private double CalculateAngularFrequency()
    {
        return 2 * Math.PI * Frequency;
    }
}
