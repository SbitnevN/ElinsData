using ElinsData.Data;
using System.Runtime.CompilerServices;

namespace ElinsData.Reader;

public partial class TextReader : ReaderBase  // ToDo рефак!
{
    internal override ElinsRecord Read(BufferStream stream, ElinsRecord data, Filter filter = Filter.All)
    {
        Span<char> buffer = stackalloc char[256];
        while (stream.ReadLine(buffer) > 0)
        {
            if (buffer.StartsWith(TextTags.Block) && filter.HasFlag(Filter.Metadata) && string.IsNullOrEmpty(data.Name))
                ReadBlock(data, buffer);
            else
                if (buffer.StartsWith(TextTags.Cycle))
                    data.Steps++;
                else
                    if (buffer.StartsWith(TextTags.Time) && filter.HasFlag(Filter.Voltammetry))
                        ReadStep(data.VoltammetryPoints, data, stream, VoltammetryPoint.Create);
                    else
                        if (buffer.StartsWith(TextTags.Frequency) && filter.HasFlag(Filter.Impedance))
                            ReadStep(data.ImpedancePoints, data, stream, ImpedancePoint.Create);

        }
        return data;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ReadBlock(ElinsRecord data, ReadOnlySpan<char> buffer)
    {
        buffer.ReadToken();
        data.Name = buffer.ReadToken().ToString();
    }

    private void ReadStep<T>(ICollection<T> points, ElinsRecord data, BufferStream stream, Func<ReadOnlySpan<char>, ElinsRecord, T> create) where T : IPoint
    {
        Span<char> buffer = stackalloc char[256];
        while (stream.ReadLine(buffer) > 0)
        {
            Span<char> line = buffer.Trim();
            if (line.IsEmpty())
                return;

            if (line.StartsWith("u"))
                return;

            points.Add(create.Invoke(line, data));
        }
    }
}
