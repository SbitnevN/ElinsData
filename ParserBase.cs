using ElinsDataParser.Data;
using System.Text;

namespace ElinsDataParser;

public abstract class ParserBase : IElinsParser
{
    public ElinsData Parse(string filePath, Filter filter = Filter.All, Name nameFrom = Name.FileName)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using BufferStream streamReader = new BufferStream(filePath, Encoding.GetEncoding(1251));

        ElinsData data = new ElinsData();
        if (nameFrom is Name.FileName)
            data.Name = GetNameFromPath(filePath.AsSpan());

        return Parse(streamReader, data, filter);
    }

    public async Task<ElinsData> ParseAsync(string filePath, Filter filter = Filter.All, Name nameFrom = Name.FileName)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using BufferStream streamReader = new BufferStream(filePath, Encoding.GetEncoding(1251));

        ElinsData data = new ElinsData();
        if (nameFrom is Name.FileName)
            data.Name = GetNameFromPath(filePath.AsSpan());

        return await ParseAsync(streamReader, data, filter);
    }

    internal abstract ElinsData Parse(BufferStream stream, ElinsData data, Filter filter = Filter.All);

    internal abstract Task<ElinsData> ParseAsync(BufferStream stream, ElinsData data, Filter filter = Filter.All);

    private static string GetNameFromPath(ReadOnlySpan<char> path)
    {
        int lastSlash = path.LastIndexOfAny('/', '\\');
        ReadOnlySpan<char> fileName = lastSlash >= 0 ? path[(lastSlash + 1)..] : path;

        int spaceIndex = fileName.IndexOf(' ');
        ReadOnlySpan<char> namePart = spaceIndex >= 0 ? fileName[..spaceIndex] : fileName;

        return namePart.ToString();
    }
}
