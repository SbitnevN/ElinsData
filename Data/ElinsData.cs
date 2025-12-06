namespace ElinsDataParser.Data;

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

public class ElinsData
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Step> Steps { get; } = new List<Step>();

    public Step LastStep => Steps.Last();
    public IEnumerable<ImpedancePoint> ImpedancePoints => Steps.SelectMany(s => s.ImpedanceData);
    public IEnumerable<VoltammetryPoint> VoltammetryPoints => Steps.SelectMany(s => s.VoltammetryData);
    public IEnumerable<double> Frequencies => ImpedancePoints.Select(i => i.Frequency).Distinct();
}
