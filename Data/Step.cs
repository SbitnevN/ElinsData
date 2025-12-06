using System.Runtime.InteropServices;

namespace ElinsDataParser.Data;

public class Step
{
    private readonly List<VoltammetryPoint> _voltammetryData = [];
    private readonly List<ImpedancePoint> _impedanceData = [];

    public ICollection<VoltammetryPoint> VoltammetryData => _voltammetryData;

    public ICollection<ImpedancePoint> ImpedanceData => _impedanceData;

    internal void ResizeVoltammetry(int size)
    {
        _voltammetryData.Capacity = size;
    }

    internal void ResizeImpedance(int size)
    {
        _impedanceData.Capacity = size;
    }

    internal void FillPotential(double potential)
    {
        Span<ImpedancePoint> span = CollectionsMarshal.AsSpan(_impedanceData);
        for (int i = 0; i < span.Length; i++)
            span[i].Potential = potential;
    }
}
