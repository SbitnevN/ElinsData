using ElinsData.Data;

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
            else if (buffer.Span.StartsWith(TextTags.Cycle))
                data.AppendStep();
            else if (buffer.Span.StartsWith(TextTags.Time) && filter.HasFlag(Filter.Voltammetry))
                await ReadStepAsync(data.VoltammetryPoints, data, stream, VoltammetryPoint.Create);
            else if (buffer.Span.StartsWith(TextTags.Frequency) && filter.HasFlag(Filter.Impedance))
                await ReadStepAsync(data.ImpedancePoints, data, stream, ImpedancePoint.Create);
        }

        return data;
    }

    private async Task ReadStepAsync<T>(ICollection<T> points, ElinsRecord data, BufferStream stream, Func<ReadOnlySpan<char>, ElinsRecord, T> create) where T : IPoint
    {
        Memory<char> buffer = new char[256];
        while (await stream.ReadAsync(buffer) > 0)
        {
            Span<char> line = buffer.Span.Trim();
            if (line.IsEmpty())
                return;

            if (line.StartsWith("u"))
                return;

            points.Add(create.Invoke(line, data));
        }
    }
}
