using ElinsData.Data;
using ElinsData.Extensions;

namespace ElinsData.Reader;

public partial class TextReader  // ToDo рефак!
{
    internal override async Task<ElinsRecord> ReadAsync(BufferStream stream, ElinsRecord data, Filter filter = Filter.All)
    {
        Memory<char> buffer = new char[256];

        while (await stream.ReadLineAsync(buffer) > 0)
        {
            if (buffer.Span.StartsWith(TextTags.Block) && filter.HasFlag(Filter.Metadata) && string.IsNullOrEmpty(data.Name))
                ReadBlock(data, buffer.Span);
            else
            if (buffer.Span.StartsWith(TextTags.Cycle))
                data.Steps++;
            else
            if (buffer.Span.StartsWith(TextTags.Time) && filter.HasFlag(Filter.Voltammetry))
                await ReadStepAsync(data.VoltammetryPoints, data.Steps, stream, VoltammetryPoint.Create);
            else
            if (buffer.Span.StartsWith(TextTags.Frequency) && filter.HasFlag(Filter.Impedance))
                await ReadStepAsync(data.ImpedancePoints, data.Steps, stream, ImpedancePoint.Create);
        }

        return data;
    }

    private async Task ReadStepAsync<T>(ICollection<T> points, int step, BufferStream stream, Func<ReadOnlySpan<char>, int, T> create) where T : IPoint
    {
        Memory<char> buffer = new char[256];
        while (await stream.ReadAsync(buffer) > 0)
        {
            Span<char> line = buffer.Span.Trim();
            if (line.IsEmpty())
                return;

            if (line.StartsWith("u"))
                return;

            points.Add(create.Invoke(line, step));
        }
    }
}
