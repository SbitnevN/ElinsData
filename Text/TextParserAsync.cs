using ElinsDataParser.Data;
using ElinsDataParser.Extensions;

namespace ElinsDataParser.Text;

public partial class TextParser  // ToDo рефак!
{
    internal override async Task<ElinsData> ParseAsync(BufferStream stream, ElinsData data, Filter filter = Filter.All)
    {
        Memory<char> buffer = new char[256];

        while (await stream.ReadLineAsync(buffer) > 0)
        {
            if (buffer.Span.StartsWith(Tags.Block) && filter.HasFlag(Filter.Metadata) && string.IsNullOrEmpty(data.Name))
                ParseBlock(data, buffer.Span);
            else
            if (buffer.Span.StartsWith(Tags.Cycle))
                data.Steps.Add(new Step());
            else
            if (buffer.Span.StartsWith(Tags.Time) && filter.HasFlag(Filter.Voltammetry))
                await ReadVoltammetryAsync(data.LastStep.VoltammetryData, stream);
            else
            if (buffer.Span.StartsWith(Tags.Frequency) && filter.HasFlag(Filter.Impedance))
                await ReadImpedanceAsync(data.LastStep.ImpedanceData, stream);
        }

        return data;
    }

    private async Task ReadVoltammetryAsync(ICollection<VoltammetryPoint> points, BufferStream stream)
    {
        Memory<char> buffer = new char[256];
        while (await stream.ReadLineAsync(buffer) > 0)
        {
            Memory<char> line = buffer.Trim();
            if (line.Span.IsEmpty())
                return;

            if (line.Span.StartsWith("u"))
                return;

            AddVoltammetry(points, line.Span);
        }
    }

    private void AddVoltammetry(ICollection<VoltammetryPoint> points, Span<char> buffer)
    {
        points.Add(new VoltammetryPoint()
        {
            Time = buffer.ReadDouble(),
            Potential = buffer.ReadDouble(),
            Current = buffer.ReadDouble(),
        });
    }

    private async Task ReadImpedanceAsync(ICollection<ImpedancePoint> points, BufferStream stream)
    {
        Memory<char> buffer = new char[256];
        while (await stream.ReadLineAsync(buffer) > 0)
        {
            Memory<char> line = buffer.Trim();
            if (line.Span.IsEmpty())
                return;

            if (line.Span.StartsWith("u"))
                return;

            AddImpedance(points, line.Span);
        }
    }

    private void AddImpedance(ICollection<ImpedancePoint> points, Span<char> buffer)
    {
        ImpedancePoint impedancePoint = new ImpedancePoint()
        {
            Frequency = buffer.ReadDouble(),
            ImpedanceReal = buffer.ReadDouble(),
            ImpedanceImaginary = buffer.ReadDouble(),
        };

        points.Add(impedancePoint);
    }
}
