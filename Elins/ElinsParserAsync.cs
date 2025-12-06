using ElinsDataParser.Data;

namespace ElinsDataParser.Elins;

public partial class ElinsParser // ToDo рефак?
{
    internal override async Task<ElinsData> ParseAsync(BufferStream stream, ElinsData data, Filter filter = Filter.All)
    {
        Memory<char> buffer = new Memory<char>(new char[256]);

        int count;
        while ((count = await stream.ReadLineAsync(buffer)) > 0)
        {
            if (count < 2)
                continue;

            switch (buffer.Span.Slice(0, 2))
            {
                case Tags.UserSample when filter.HasFlag(Filter.Metadata) && string.IsNullOrEmpty(data.Name):
                    data.Name = buffer.Slice(2).Trim().ToString();
                    break;

                case Tags.VoltammetryCount when filter.HasFlag(Filter.Voltammetry):
                    ParseVoltammetryCount(buffer.Span, data.LastStep);
                    break;

                case Tags.ImpedanceCount when filter.HasFlag(Filter.Impedance):
                    ParseImpedanceCount(buffer.Span, data.LastStep);
                    break;

                case Tags.VoltammetryPoint when filter.HasFlag(Filter.Voltammetry):
                    ParseVoltammetryPoint(buffer.Span, data);
                    break;

                case Tags.ImpedancePoint when filter.HasFlag(Filter.Impedance):
                    ParseImpedancePoint(buffer.Span, data);
                    break;

                case Tags.Step when filter.HasFlag(Filter.Impedance | Filter.Voltammetry):
                    data.Steps.Add(new Step());
                    break;
            }
        }

        return data;
    }
}
