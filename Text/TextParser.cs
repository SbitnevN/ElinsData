using ElinsDataParser.Data;
using ElinsDataParser.Extensions;
using System.Runtime.CompilerServices;

namespace ElinsDataParser.Text;

public partial class TextParser : ParserBase  // ToDo рефак!
{
    internal override ElinsData Parse(BufferStream stream, ElinsData data, Filter filter = Filter.All)
    {
        Span<char> buffer = stackalloc char[256];

        while (stream.ReadLine(buffer) > 0)
        {
            if (buffer.StartsWith(Tags.Block) && filter.HasFlag(Filter.Metadata) && string.IsNullOrEmpty(data.Name))
                ParseBlock(data, buffer);
            else
            if (buffer.StartsWith(Tags.Cycle))
                data.Steps.Add(new Step());
            else
            if (buffer.StartsWith(Tags.Time) && filter.HasFlag(Filter.Voltammetry))
                ReadVoltammetry(data.LastStep.VoltammetryData, stream);
            else
            if (buffer.StartsWith(Tags.Frequency) && filter.HasFlag(Filter.Impedance))
                ReadImpedance(data.LastStep.ImpedanceData, stream);
        }

        return data;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ParseBlock(ElinsData data, ReadOnlySpan<char> buffer)
    {
        buffer.ReadToken();
        data.Name = buffer.ReadToken().ToString();
    }

    private void ReadVoltammetry(ICollection<VoltammetryPoint> points, BufferStream stream)
    {
        Span<char> buffer = stackalloc char[256];
        while (stream.ReadLine(buffer) > 0)
        {
            Span<char> line = buffer.Trim();
            if (line.IsEmpty())
                return;

            if (line.StartsWith("u"))
                return;

            points.Add(new VoltammetryPoint()
            {
                Time = line.ReadDouble(),
                Potential = line.ReadDouble(),
                Current = line.ReadDouble(),
            });
        }
    }

    private void ReadImpedance(ICollection<ImpedancePoint> points, BufferStream stream)
    {
        Span<char> buffer = stackalloc char[256];
        while (stream.ReadLine(buffer) > 0)
        {
            Span<char> line = buffer.Trim();
            if (line.IsEmpty())
                return;

            if (line.StartsWith("u"))
                return;

            ImpedancePoint point = new ImpedancePoint()
            {
                Frequency = line.ReadDouble(),
                ImpedanceReal = line.ReadDouble(),
                ImpedanceImaginary = line.ReadDouble()
            };

            points.Add(point);
        }
    }
}
