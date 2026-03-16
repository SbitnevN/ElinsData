using ElinsData.Reader;

namespace ElinsData.Data;

public class VoltammetryPoint : IVoltammetryPoint
{
    private VoltammetryPoint() { }

    /// <summary> Время, С </summary>
    public double Time { get; set; }

    /// <summary> Потенциал, В </summary>
    public double Potential { get; set; }

    /// <summary> Ток, А </summary>
    public double Current { get; set; }

    /// <summary>Шаг №</summary>
    public int Step { get; set; }

    public static IVoltammetryPoint Create(ReadOnlySpan<char> line, ElinsRecord data)
    {
        return new VoltammetryPoint
        {
            Time = line.ReadDouble(),
            Potential = line.ReadDouble(),
            Current = line.ReadDouble(),
            Step = data.Steps
        };
    }
}
