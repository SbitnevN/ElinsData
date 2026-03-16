namespace ElinsData.Data;

public interface IImpedancePoint : IPoint
{
    double Capacitance { get; set; }
    double Frequency { get; set; }
    double ImpedanceImaginary { get; set; }
    double ImpedanceReal { get; set; }
    IStepPotential Potential { get; set; }
}