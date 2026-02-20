namespace ElinsData.Data;

public interface IVoltammetryPoint : IPoint
{
    double Current { get; set; }
    double Potential { get; set; }
    double Time { get; set; }
}