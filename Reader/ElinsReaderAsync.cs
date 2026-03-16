using ElinsData.Data;

namespace ElinsData.Reader;

public partial class ElinsReader
{
    internal override async Task<ElinsRecord> ReadAsync(BufferStream stream, ElinsRecord data, Filter filter = Filter.All)
    {
        Memory<char> buffer = new Memory<char>(new char[256]);

        int count;
        while ((count = await stream.ReadLineAsync(buffer)) > 0)
        {
            if (count < 2)
                continue;

            switch (buffer.Span.Slice(0, 2))
            {
                case ElinsTags.UserSample when filter.HasFlag(Filter.Metadata) && string.IsNullOrEmpty(data.Name):
                    data.Name = buffer.Slice(2).Trim().ToString();
                    break;

                case ElinsTags.VoltammetryPoint when filter.HasFlag(Filter.Voltammetry):
                    ReadVoltammetryPoint(buffer.Span, data);
                    break;

                case ElinsTags.ImpedancePoint when filter.HasFlag(Filter.Impedance):
                    ReadImpedancePoint(buffer.Span, data);
                    break;

                case ElinsTags.Step when filter.HasFlag(Filter.Impedance | Filter.Voltammetry):
                    data.AppendStep();
                    break;
            }
        }

        return data;
    }
}
