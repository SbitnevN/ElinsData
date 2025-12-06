using ElinsDataParser.Data;
using ElinsDataParser.Extensions;
using System.Runtime.CompilerServices;

namespace ElinsDataParser.Elins;

public partial class ElinsParser : ParserBase
{
    internal override ElinsData Parse(BufferStream stream, ElinsData data, Filter filter = Filter.All)
    {
        Span<char> buffer = stackalloc char[256];

        int count;
        while ((count = stream.ReadLine(buffer)) > 0)
        {
            if (count < 2)
                continue;

            switch (buffer.Slice(0, 2))
            {
                case Tags.UserSample when filter.HasFlag(Filter.Metadata) && string.IsNullOrEmpty(data.Name):
                    data.Name = buffer.Slice(2).Trim().ToString();
                    break;

                case Tags.VoltammetryCount when filter.HasFlag(Filter.Voltammetry):
                    ParseVoltammetryCount(buffer, data.LastStep);
                    break;

                case Tags.ImpedanceCount when filter.HasFlag(Filter.Impedance):
                    ParseImpedanceCount(buffer, data.LastStep);
                    break;

                case Tags.VoltammetryPoint when filter.HasFlag(Filter.Voltammetry):
                    ParseVoltammetryPoint(buffer, data);
                    break;

                case Tags.ImpedancePoint when filter.HasFlag(Filter.Impedance):
                    ParseImpedancePoint(buffer, data);
                    break;

                case Tags.Step when filter.HasFlag(Filter.Impedance | Filter.Voltammetry):
                    data.Steps.Add(new Step());
                    break;
            }
        }

        return data;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ParseVoltammetryCount(ReadOnlySpan<char> buffer, Step step)
    {
        buffer.ReadToken();
        step.ResizeVoltammetry(buffer.ReadInt());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ParseImpedanceCount(ReadOnlySpan<char> buffer, Step step)
    {
        buffer.ReadToken();
        step.ResizeImpedance(buffer.ReadInt());
    }

    private void ParseVoltammetryPoint(ReadOnlySpan<char> buffer, ElinsData data)
    {
        VoltammetryPoint voltammetryPoint = new VoltammetryPoint();

        buffer.ReadToken();
        voltammetryPoint.Time = buffer.ReadDouble();
        voltammetryPoint.Potential = buffer.ReadDouble();
        voltammetryPoint.Current = buffer.ReadDouble();

        data.LastStep.VoltammetryData.Add(voltammetryPoint);
    }

    private void ParseImpedancePoint(ReadOnlySpan<char> buffer, ElinsData data)
    {
        ImpedancePoint impedancePoint = new ImpedancePoint();

        buffer.ReadToken();
        impedancePoint.Frequency = buffer.ReadDouble();
        impedancePoint.ImpedanceReal = buffer.ReadDouble();
        impedancePoint.ImpedanceImaginary = buffer.ReadDouble();

        data.LastStep.ImpedanceData.Add(impedancePoint);
    }
}
