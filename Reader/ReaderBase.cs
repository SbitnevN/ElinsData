using ElinsData.Data;
using System.Text;

namespace ElinsData.Reader;

public abstract class ReaderBase : IElinsReader
{
    public ElinsRecord Read(string filePath, Filter filter = Filter.All, Name nameFrom = Name.FileName)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using BufferStream streamReader = new BufferStream(filePath, Encoding.GetEncoding(1251));

        ElinsRecord data = new ElinsRecord();
        if (nameFrom is Name.FileName)
            data.Name = GetNameFromPath(filePath.AsSpan());

        return Read(streamReader, data, filter);
    }

    public async Task<ElinsRecord> ReadAsync(string filePath, Filter filter = Filter.All, Name nameFrom = Name.FileName)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using BufferStream streamReader = new BufferStream(filePath, Encoding.GetEncoding(1251));

        ElinsRecord data = new ElinsRecord();
        if (nameFrom is Name.FileName)
            data.Name = GetNameFromPath(filePath.AsSpan());

        return await ReadAsync(streamReader, data, filter);
    }

    internal abstract ElinsRecord Read(BufferStream stream, ElinsRecord data, Filter filter = Filter.All);

    internal abstract Task<ElinsRecord> ReadAsync(BufferStream stream, ElinsRecord data, Filter filter = Filter.All);

    private static string GetNameFromPath(ReadOnlySpan<char> path)
    {
        int lastSlash = path.LastIndexOfAny('/', '\\');
        ReadOnlySpan<char> fileName = lastSlash >= 0 ? path[(lastSlash + 1)..] : path;

        int spaceIndex = fileName.IndexOf(' ');
        ReadOnlySpan<char> namePart = spaceIndex >= 0 ? fileName[..spaceIndex] : fileName;

        return namePart.ToString();
    }
}
