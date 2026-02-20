namespace ElinsData.Data;

[Flags]
public enum Filter
{
    Metadata = 1,
    Voltammetry = 2,
    Impedance = 3,
    All = Metadata | Voltammetry | Impedance
}

public enum Name
{
    FileName,
    FileContent
}

public class ElinsRecord
{
    public string Name { get; set; } = string.Empty;
    public int Steps { get; set; } = 0;

    public ICollection<IImpedancePoint> ImpedancePoints { get; } = [];
    public ICollection<IVoltammetryPoint> VoltammetryPoints { get; } = [];
}
