using ElinsData.Data;

namespace ElinsData.Reader;

public partial class ElinsReader : ReaderBase
{
    internal override ElinsRecord Read(BufferStream stream, ElinsRecord data, Filter filter = Filter.All)
    {
        Span<char> buffer = stackalloc char[256];

        int count;
        while ((count = stream.ReadLine(buffer)) > 0)
        {
            if (count < 2)
                continue;

            switch (buffer.Slice(0, 2))
            {
                case ElinsTags.UserSample when filter.HasFlag(Filter.Metadata) && string.IsNullOrEmpty(data.Name):
                    data.Name = buffer.Slice(2).Trim().ToString();
                    break;

                case ElinsTags.VoltammetryPoint when filter.HasFlag(Filter.Voltammetry):
                    ReadVoltammetryPoint(buffer, data);
                    break;

                case ElinsTags.ImpedancePoint when filter.HasFlag(Filter.Impedance):
                    ReadImpedancePoint(buffer, data);
                    break;

                case ElinsTags.Step when filter.HasFlag(Filter.Impedance) || filter.HasFlag(Filter.Voltammetry):
                    data.Steps++;
                    data.StepPotentials.Add(new StepPotential
                    {
                        Step = data.Steps
                    });
                    break;
            }
        }

        return data;
    }

    private void ReadVoltammetryPoint(ReadOnlySpan<char> buffer, ElinsRecord data)
    {
        buffer.ReadToken();
        IVoltammetryPoint voltammetryPoint = VoltammetryPoint.Create(buffer, data.Steps);
        data.VoltammetryPoints.Add(voltammetryPoint);
    }

    private void ReadImpedancePoint(ReadOnlySpan<char> buffer, ElinsRecord data)
    {
        buffer.ReadToken();
        IImpedancePoint impedancePoint = ImpedancePoint.Create(buffer, data.StepPotentials.Last());
        data.ImpedancePoints.Add(impedancePoint);
    }
}
